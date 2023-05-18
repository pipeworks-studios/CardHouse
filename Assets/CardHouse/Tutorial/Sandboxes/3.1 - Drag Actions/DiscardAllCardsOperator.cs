using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DiscardAllCardsOperator : MonoBehaviour
{
    public SeekerScriptableSet DiscardSeekers;
    public SeekerScriptableSet TargetDiscardSeekers;

    public void Activate()
    {
        var boardGroups = GroupRegistry.Instance?.Groups.Where(x => x.Name == GroupName.Board).Select(x => x.Group);
        if (boardGroups != null)
        {
            var seekerSets = new SeekerSetList();

            var presentationTransform = PhaseManager.Instance?.CurrentPhase?.CardPresentationPosition;
            if (presentationTransform != null)
            {
                seekerSets.Add(new SeekerSet
                {
                    Card = GetComponent<Card>(),
                    Homing = DiscardSeekers.Homing.GetStrategy(presentationTransform.position),
                    Turning = DiscardSeekers.Turning.GetStrategy(CardHouse.Utils.CorrectAngle(presentationTransform.rotation.eulerAngles.z)),
                    Scaling = DiscardSeekers.Scaling.GetStrategy(presentationTransform.lossyScale.x)
                });
            }

            foreach (var boardGroup in boardGroups)
            {
                foreach (var target in boardGroup.MountedCards.ToArray())
                {
                    seekerSets.Add(new SeekerSet { Card = target, Homing = TargetDiscardSeekers.Homing?.GetStrategy() });
                }

                var discardGroup = GroupRegistry.Instance?.Get(GroupName.Discard, GroupRegistry.Instance?.GetOwnerIndex(boardGroup));

                foreach (var target in boardGroup.MountedCards.ToArray())
                {
                    discardGroup.Mount(target,
                        seekerSets: seekerSets,
                        seekersForUnmounting: new SeekerSet { Homing = DiscardSeekers.Homing?.GetStrategy() }
                    );
                }
            }
        }
    }
}