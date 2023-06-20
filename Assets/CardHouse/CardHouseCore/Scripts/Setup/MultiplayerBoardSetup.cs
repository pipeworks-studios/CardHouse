using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CardHouse
{
    public class MultiplayerBoardSetup : MonoBehaviour
    {
        [Serializable]
        public class GroupTransitionByName
        {
            public int PhaseIndex;
            public GroupName Source;
            public GroupName Destination;
            public DragAction DragAction;
            public PvpMode Mode;
        }

        public enum PvpMode
        {
            PlayerToEnemy,
            EnemyToPlayer
        }

        public bool RunOnStart = true;
        public GameObject PlayerBoard;
        public int PlayerCount = 2;
        public float SpacingMultiplier = 1.0f;

        public List<GroupTransitionByName> PlayerToPlayerInteractions;

        List<GameObject> SpawnedBoards = new List<GameObject>();
        List<GroupSetup> SpawnedGroupSetups = new List<GroupSetup>();
        List<DeckSetup> SpawnedDeckSetups = new List<DeckSetup>();
        List<Phase> SpawnedPhases = new List<Phase>();
        Dictionary<int, List<DragTransition>> PvpDragTransitionsAddedToTemplate = new Dictionary<int, List<DragTransition>>();

        Vector3 Size;
        Vector3 Offset;

        public GameObject[] GetAllBoards()
        {
            var boards = new List<GameObject> { PlayerBoard };
            boards.AddRange(SpawnedBoards);
            return boards.ToArray();
        }

        private void Start()
        {
            if (RunOnStart)
            {
                Setup(false);
            }
        }

        public void Setup(bool callSetupScripts = true)
        {
            if (PlayerCount <= 0)
                return;

            Teardown();

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

            // Analyze template board
            var originalBoardGroups = PlayerBoard.GetComponentsInChildren<CardGroup>();
            var originalBoardButtons = PlayerBoard.GetComponentsInChildren<Button>();
            var originalBoardClickables = PlayerBoard.GetComponentsInChildren<ClickDetector>();
            var originalBoardComponents = PlayerBoard.GetComponentsInChildren<MonoBehaviour>();
            var originalBoardGroupSetups = PlayerBoard.GetComponentsInChildren<GroupSetup>();
            var originalBoardDeckSetups = PlayerBoard.GetComponentsInChildren<DeckSetup>();

            // Find external setup scripts
            var oddGroupSetups = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<GroupSetup>()).Where(x => !originalBoardGroupSetups.Contains(x)).ToArray();
            var oddDeckSetups = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<DeckSetup>()).Where(x => !originalBoardDeckSetups.Contains(x)).ToArray();

            // Find external card groups
            var oddGroups = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<CardGroup>()).Where(x => !originalBoardGroups.Contains(x)).ToArray();

            if (callSetupScripts)
            {
                foreach (var setup in originalBoardGroupSetups)
                {
                    setup.DoSetup();
                }
                foreach (var setup in oddGroupSetups)
                {
                    setup.DoSetup();
                }
                foreach (var setup in originalBoardDeckSetups)
                {
                    setup.DoSetup();
                }
                foreach (var setup in oddDeckSetups)
                {
                    setup.DoSetup();
                }
            }

            // Analyze Group Registry
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

            // Setup boards
            var newPhases = new Dictionary<int, List<Phase>>();

            var marginalAngle = 360f / PlayerCount;
            var distanceToCenter = Size.y * 110f * SpacingMultiplier / marginalAngle;
            var centerOfCircle = PlayerBoard.transform.position + Offset + Vector3.up * distanceToCenter;

            // Scale camera and camera points
            Camera.main.orthographicSize = distanceToCenter + Size.y / 2f;
            foreach (var phase in PhaseManager.Instance.Phases)
            {
                if (phase.CameraPosition == null)
                    continue;

                phase.CameraPosition.localPosition = Offset + Vector3.up * (distanceToCenter);
            }

            for (var i = 1; i < PlayerCount; i++)
            {
                // Duplicate and space
                var newBoard = Instantiate(PlayerBoard.gameObject);
                SpawnedBoards.Add(newBoard);
                newBoard.transform.RotateAround(centerOfCircle, Vector3.forward, marginalAngle * i);

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
                            GroupRegistry.Instance.Groups.Add(new GroupRegistry.NamedGroup { Name = registeredGroup.Key, PlayerIndex = i, Group = correspondingGroup });
                        }
                    }
                }

                // Group Setup
                foreach (var groupSetup in oddGroupSetups)
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
                    SpawnedGroupSetups.Add(newGroupSetup);
                    newGroupSetup.RunOnStart = groupSetup.RunOnStart;
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
                    if (callSetupScripts)
                    {
                        newGroupSetup.DoSetup();
                    }
                }

                // Deck Setup
                foreach (var deckSetup in oddDeckSetups)
                {
                    if (originalBoardGroups.Contains(deckSetup.Deck))
                    {
                        var correspondingGroup = boardGroups.GetComponentForName(deckSetup.Deck.gameObject.name);
                        var newDeckSetup = deckSetup.gameObject.AddComponent<DeckSetup>();
                        SpawnedDeckSetups.Add(newDeckSetup);
                        newDeckSetup.RunOnStart = deckSetup.RunOnStart;
                        newDeckSetup.Deck = correspondingGroup;
                        newDeckSetup.CardPrefab = deckSetup.CardPrefab;
                        newDeckSetup.DeckDefinition = deckSetup.DeckDefinition;
                        newDeckSetup.OnSetupCompleteEventChain = CopyEvents(deckSetup.OnSetupCompleteEventChain, originalBoardComponents, boardComponents);
                        if (callSetupScripts)
                        {
                            newDeckSetup.DoSetup();
                        }
                    }
                }

                // Resource Manager
                if (CurrencyRegistry.Instance != null && CurrencyRegistry.Instance.PlayerWallets.Count > 0)
                {
                    CurrencyRegistry.Instance.PlayerWallets.Add((CurrencyWallet)CurrencyRegistry.Instance.PlayerWallets[0].Clone());
                }

                // Phase Manager
                if (PhaseManager.Instance != null)
                {
                    var p1Phases = PhaseManager.Instance.Phases.Where(x => x.PlayerIndex == 0).Select(x => PhaseManager.Instance.Phases.IndexOf(x)).ToArray();
                    foreach (var phaseIndex in p1Phases)
                    {
                        var phase = PhaseManager.Instance.Phases[phaseIndex];
                        var newPhase = new Phase
                        {
                            Name = phase.Name.Replace("1", (i + 1).ToString()),
                            PlayerIndex = i,
                            CameraPosition = boardTransforms.GetComponentForName(phase.CameraPosition.gameObject.name),
                            CardPresentationPosition = boardTransforms.GetComponentForName(phase.CardPresentationPosition.gameObject.name),
                        };
                        SpawnedPhases.Add(newPhase);

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

                        if (newPhases.ContainsKey(phaseIndex))
                        {
                            newPhases[phaseIndex].Add(newPhase);
                        }
                        else
                        {
                            newPhases[phaseIndex] = new List<Phase> { newPhase };
                        }
                    }
                }
            }

            var phasesByIndex = new Dictionary<int, List<Phase>>();
            var reversedKeys = newPhases.Keys.ToList();
            reversedKeys.Sort();
            reversedKeys.Reverse();
            foreach (var phaseIndex in reversedKeys)
            {
                PhaseManager.Instance.Phases.InsertRange(phaseIndex + 1, newPhases[phaseIndex]);
                phasesByIndex[phaseIndex] = PhaseManager.Instance.Phases.GetRange(phaseIndex, newPhases[phaseIndex].Count + 1);
            }

            // Set up player-to-player interactions
            reversedKeys.Reverse();
            foreach (var pvp in PlayerToPlayerInteractions)
            {
                if (phasesByIndex.ContainsKey(pvp.PhaseIndex))
                {
                    foreach (var phase in phasesByIndex[pvp.PhaseIndex])
                    {
                        for (var i = 0; i < PlayerCount; i++)
                        {
                            if (i == phase.PlayerIndex)
                                continue;

                            var sourceIndex = pvp.Mode == PvpMode.PlayerToEnemy ? phase.PlayerIndex : i;
                            var destinationIndex = pvp.Mode == PvpMode.PlayerToEnemy ? i : phase.PlayerIndex;
                            var newDragTransition = new DragTransition
                            {
                                Source = GroupRegistry.Instance.Get(pvp.Source, sourceIndex),
                                Destination = GroupRegistry.Instance.Get(pvp.Destination, destinationIndex),
                                DragAction = pvp.DragAction
                            };
                            phase.ValidDrags.Add(newDragTransition);

                            if (phase == phasesByIndex[pvp.PhaseIndex][0])
                            {
                                if (PvpDragTransitionsAddedToTemplate.ContainsKey(pvp.PhaseIndex))
                                {
                                    PvpDragTransitionsAddedToTemplate[pvp.PhaseIndex].Add(newDragTransition);
                                }
                                else
                                {
                                    PvpDragTransitionsAddedToTemplate.Add(pvp.PhaseIndex, new List<DragTransition> { newDragTransition });
                                }
                            }
                        }
                    }
                }
            }

            // Move odd groups to center
            foreach (var group in oddGroups)
            {
                group.gameObject.transform.Translate(centerOfCircle - oddGroups[0].transform.position);
            }

            // Scale presentation points
            foreach (var phase in PhaseManager.Instance.Phases)
            {
                if (phase.CardPresentationPosition == null)
                    continue;

                phase.CardPresentationPosition.localScale = Vector3.one * 1.5f * Camera.main.orthographicSize / 4f;
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

        void Teardown()
        {
            foreach (var group in SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<CardGroup>()))
            {
                foreach (var card in group.MountedCards.ToArray())
                {
                    group.MountedCards.Remove(card);
                    Destroy(card.gameObject);
                }
            }

            foreach (var board in SpawnedBoards)
            {
                Destroy(board.gameObject);
            }
            SpawnedBoards.Clear();

            if (GroupRegistry.Instance != null)
            {
                foreach (var group in GroupRegistry.Instance.Groups.ToArray())
                {
                    if (group.PlayerIndex > 0)
                    {
                        GroupRegistry.Instance.Groups.Remove(group);
                    }
                }
            }

            if (CurrencyRegistry.Instance != null && CurrencyRegistry.Instance.PlayerWallets.Count > 0)
            {
                CurrencyRegistry.Instance.PlayerWallets = new List<CurrencyWallet> { CurrencyRegistry.Instance.PlayerWallets[0] };
            }

            foreach (var setup in SpawnedGroupSetups)
            {
                DestroyImmediate(setup);
            }
            SpawnedGroupSetups.Clear();

            foreach (var setup in SpawnedDeckSetups)
            {
                DestroyImmediate(setup);
            }
            SpawnedDeckSetups.Clear();

            if (PhaseManager.Instance != null)
            {
                foreach (var phase in SpawnedPhases)
                {
                    PhaseManager.Instance.Phases.Remove(phase);
                }
                SpawnedPhases.Clear();

                foreach (var phaseIndex in PvpDragTransitionsAddedToTemplate.Keys)
                {
                    foreach (var transition in PvpDragTransitionsAddedToTemplate[phaseIndex])
                    {
                        PhaseManager.Instance.Phases[phaseIndex].ValidDrags.Remove(transition);
                    }
                }
                PvpDragTransitionsAddedToTemplate.Clear();

                PhaseManager.Instance.HardReset();
            }
        }

    }
}