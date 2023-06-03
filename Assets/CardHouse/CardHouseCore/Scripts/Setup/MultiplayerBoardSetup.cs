using CardHouse;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerBoardSetup : MonoBehaviour
{
    public class GroupTransitionByName
    {
        public GroupName Source;
        public GroupName Destination;
        public DragAction DragAction;
    }

    public GameObject PlayerBoard;
    public int PlayerCount = 2;
    public float SpacingMultiplier = 1.0f;

    public List<GroupTransitionByName> PlayerToPlayerInteractions;

    Vector3 Size;
    Vector3 Offset;

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        if (PlayerCount <= 0)
            return;

        // Find bounds
        var bottom = 0f;
        var top = 0f;
        var left = 0f;
        var right = 0f;
        foreach (var cardGroup in PlayerBoard.GetComponentsInChildren<CardGroup>())
        {
            left = Mathf.Min(left, cardGroup.transform.position.x - cardGroup.transform.lossyScale.x / 2f);
            right = Mathf.Max(right, cardGroup.transform.position.x + cardGroup.transform.lossyScale.x / 2f);
            bottom = Mathf.Min(bottom, cardGroup.transform.position.y - cardGroup.transform.lossyScale.y / 2f);
            top = Mathf.Max(top, cardGroup.transform.position.y + cardGroup.transform.lossyScale.y / 2f);
        }

        Size = new Vector3(right - left, top - bottom, 0);
        Offset = new Vector3(left + Size.x / 2f - PlayerBoard.transform.position.x, bottom + Size.y / 2f - PlayerBoard.transform.position.y, 0);

        // Analyze group registry
        var originalBoardTransforms = PlayerBoard.GetComponentsInChildren<Transform>();
        var originalBoardGroups = PlayerBoard.GetComponentsInChildren<CardGroup>();
        var originalBoardButtons = PlayerBoard.GetComponentsInChildren<Button>();
        var originalBoardClickables = PlayerBoard.GetComponentsInChildren<ClickDetector>();
        var originalBoardComponents = PlayerBoard.GetComponentsInChildren<MonoBehaviour>();

        var registeredGroups = new Dictionary<GroupName, CardGroup>();
        if (GroupRegistry.Instance != null)
        {
            foreach (var group in GroupRegistry.Instance.Groups)
            {
                if (group.PlayerIndex == 0)
                {
                    registeredGroups.Add(group.Name, group.Group);
                }
            }
        }

        // Find general group setup scripts
        var excludedGroupSetupScripts = PlayerBoard.GetComponentsInChildren<GroupSetup>();
        var oddGroupSetupScripts = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<GroupSetup>()).Where(x => !excludedGroupSetupScripts.Contains(x)).ToArray();

        var excludedDeckSetupScripts = PlayerBoard.GetComponentsInChildren<DeckSetup>();
        var oddDeckSetupScripts = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<DeckSetup>()).Where(x => !excludedDeckSetupScripts.Contains(x)).ToArray();

        // Setup boards
        var marginalAngle = 360f / PlayerCount;
        for (var i = 1; i < PlayerCount; i++)
        {
            // Duplicate and space
            var newBoard = Instantiate(PlayerBoard);
            newBoard.transform.RotateAround(PlayerBoard.transform.position + Offset + Vector3.up * Size.y * 110f * SpacingMultiplier / marginalAngle, Vector3.forward, marginalAngle * i);

            var boardGroups = newBoard.GetComponentsInChildren<CardGroup>();
            var boardTransforms = newBoard.GetComponentsInChildren<Transform>();
            var boardButtons = newBoard.GetComponentsInChildren<Button>();
            var boardClickables = newBoard.GetComponentsInChildren<ClickDetector>();
            var boardComponents = newBoard.GetComponentsInChildren<MonoBehaviour>();

            // Group Registry
            if (GroupRegistry.Instance != null)
            {
                foreach (var registeredGroup in registeredGroups)
                {
                    var correspondingGroup = boardGroups.GetComponentForName(registeredGroup.Value.gameObject.name);
                    if (correspondingGroup != null)
                    {
                        GroupRegistry.Instance.Groups.Add(new GroupRegistry.NamedGroup { Name = registeredGroup.Key, PlayerIndex = i, Group = correspondingGroup});
                    }
                }
            }

            // Group Setup
            foreach (var groupSetup in oddGroupSetupScripts)
            {
                var groupSetupEntriesToAdd = new List<GroupSetup.GroupPopulationData>();

                foreach (var target in groupSetup.GroupPopulationList)
                {
                    if (originalBoardGroups.Contains(target.Group))
                    {
                        var correspondingGroup = boardGroups.GetComponentForName(target.Group.gameObject.name);
                        groupSetupEntriesToAdd.Add(new GroupSetup.GroupPopulationData { Group = correspondingGroup, CardPrefab = target.CardPrefab, CardCount = target.CardCount });
                    }
                }

                var newGroupSetup = groupSetup.gameObject.AddComponent<GroupSetup>();
                newGroupSetup.GroupPopulationList = groupSetupEntriesToAdd;
                newGroupSetup.GroupsToShuffle = new List<CardGroup>();

                foreach (var group in groupSetup.GroupsToShuffle.ToArray())
                {
                    if (originalBoardGroups.Contains(group))
                    {
                        var correspondingGroup = boardGroups.GetComponentForName(group.gameObject.name);
                        newGroupSetup.GroupsToShuffle.Add(correspondingGroup);
                    }
                }

                newGroupSetup.OnSetupCompleteEventChain = CopyEvents(groupSetup.OnSetupCompleteEventChain, originalBoardComponents, boardComponents);
            }

            // Deck Setup
            foreach (var deckSetup in oddDeckSetupScripts)
            {
                if (originalBoardGroups.Contains(deckSetup.Deck))
                {
                    var correspondingGroup = boardGroups.GetComponentForName(deckSetup.Deck.gameObject.name);
                    var newDeckSetup = deckSetup.gameObject.AddComponent<DeckSetup>();
                    newDeckSetup.Deck = correspondingGroup;
                    newDeckSetup.CardPrefab = deckSetup.CardPrefab;
                    newDeckSetup.DeckDefinition = deckSetup.DeckDefinition;
                    newDeckSetup.OnSetupCompleteEventChain = CopyEvents(deckSetup.OnSetupCompleteEventChain, originalBoardComponents, boardComponents);
                }
            }

            // Resource Manager
            if (CurrencyRegistry.Instance != null)
            {
                CurrencyRegistry.Instance.PlayerWallets.Add((CurrencyWallet)CurrencyRegistry.Instance.PlayerWallets[0].Clone());
            }

            // Phase Manager
            if (PhaseManager.Instance != null)
            {
                var p1Phases = PhaseManager.Instance.Phases.Where(x => x.PlayerIndex == 0).ToArray();
                foreach (var phase in p1Phases)
                {
                    var newPhase = new Phase
                    {
                        Name = $"Player {i + 1}",
                        PlayerIndex = i,
                        CameraPosition = boardTransforms.GetComponentForName(phase.CameraPosition.gameObject.name),
                        CardPresentationPosition = boardTransforms.GetComponentForName(phase.CardPresentationPosition.gameObject.name),
                    };

                    newPhase.ActiveButtons = phase.ActiveButtons.ToList();
                    for (var j = 0; j < newPhase.ActiveButtons.Count; j++)
                    {
                        if (originalBoardButtons.Contains(newPhase.ActiveButtons[j]))
                        {
                            newPhase.ActiveButtons[j] = boardButtons.GetComponentForName(newPhase.ActiveButtons[j].gameObject.name);
                        }
                    }

                    newPhase.ValidClickTargets = phase.ValidClickTargets.ToList();
                    for (var j = 0; j < newPhase.ValidClickTargets.Count; j++)
                    {
                        if (originalBoardClickables.Contains(newPhase.ValidClickTargets[j]))
                        {
                            newPhase.ValidClickTargets[j] = boardClickables.GetComponentForName(newPhase.ValidClickTargets[j].gameObject.name);
                        }
                    }

                    //  Valid Drags
                    newPhase.ValidDrags = phase.ValidDrags.Select(x => new DragTransition { Source = x.Source, Destination = x.Destination, DragAction = x.DragAction }).ToList();
                    foreach (var drag in newPhase.ValidDrags)
                    {
                        if (originalBoardGroups.Contains(drag.Source))
                        {
                            drag.Source = boardGroups.GetComponentForName(drag.Source.gameObject.name);
                        }

                        if (originalBoardGroups.Contains(drag.Destination))
                        {
                            drag.Destination = boardGroups.GetComponentForName(drag.Destination.gameObject.name);
                        }
                    }

                    //  Beginning/End of phase events
                    newPhase.OnPhaseStartEventChain = CopyEvents(phase.OnPhaseStartEventChain, originalBoardComponents, boardComponents);
                    newPhase.OnPhaseEndEventChain = CopyEvents(phase.OnPhaseEndEventChain, originalBoardComponents, boardComponents);


                    PhaseManager.Instance.Phases.Add(newPhase);
                }
            }

        }

    }

    List<TimedEvent> CopyEvents(IEnumerable<TimedEvent> source, IEnumerable<MonoBehaviour> sourceComponents, IEnumerable<MonoBehaviour> destinationComponents)
    {
        var output = new List<TimedEvent>();
        foreach (var timedEvent in source)
        {
            var newTimedEvent = new TimedEvent { Duration = timedEvent.Duration, Event = new UnityEngine.Events.UnityEvent() };
            for (var j = 0; j < timedEvent.Event.GetPersistentEventCount(); j++)
            {
                var target = (MonoBehaviour)timedEvent.Event.GetPersistentTarget(j);
                if (sourceComponents.Contains(target))
                {
                    target = destinationComponents.GetComponentForName(target.gameObject.name, target.GetType());
                }

                var methodName = timedEvent.Event.GetPersistentMethodName(j);
                newTimedEvent.Event.AddListener(new UnityEngine.Events.UnityAction(() => target.Invoke(methodName, 0f)));
            }

            output.Add(newTimedEvent);
        }
        return output;
    }

    void Update()
    {
        //Debug.DrawLine(PlayerBoard.transform.position + Offset, PlayerBoard.transform.position + Offset + Size / 2f, Color.red);
        //Debug.DrawLine(PlayerBoard.transform.position - Offset + Vector3.right * -1f, PlayerBoard.transform.position - Offset + Vector3.right * 1f, Color.red);
    }

}