using UnityEngine;

public class DiscardTargetCardOperator : CardTargetCardOperator
{
    public SeekerScriptableSet TargetDiscardSeekers;

    protected override void ActOnTarget()
    {
        var discardGroup = GroupRegistry.Instance?.Get(GroupName.Discard, GroupRegistry.Instance.GetOwnerIndex(Target.Group));
        if (discardGroup != null)
        {
            var seekerSets = new SeekerSetList { new SeekerSet { Card = Target, Homing = TargetDiscardSeekers.Homing.GetStrategy() } };
            var presentationTransform = PhaseManager.Instance?.CurrentPhase?.CardPresentationPosition;
            if (presentationTransform != null)
            {
                seekerSets.Add(new SeekerSet 
                { 
                    Card = MyCard, 
                    Homing = DiscardSeekers.Homing.GetStrategy(presentationTransform.position),
                    Turning = DiscardSeekers.Turning.GetStrategy(CardHouse.Utils.CorrectAngle(presentationTransform.rotation.eulerAngles.z)),
                    Scaling = DiscardSeekers.Scaling.GetStrategy(presentationTransform.lossyScale.x)
                });
            }
            discardGroup.Mount(Target,
                seekerSets: seekerSets,
                seekersForUnmounting: new SeekerSet { Homing = DiscardSeekers.Homing.GetStrategy() }
            );
        }
    }
}
