using System.Collections.Generic;
using UnityEngine;

public class StackLayout : CardGroupSettings
{
    public Vector3 MarginalCardOffset = new Vector3(0.01f, 0.01f, -0.01f);

    protected override void ApplySpacing(List<Card> cards, SeekerSetList seekerSets = null)
    {
        for (var i = 0; i < cards.Count; i++)
        {
            var seekerSet = seekerSets?.GetSeekerSetFor(cards[i]);
            cards[i].Homing.StartSeeking(transform.position + Vector3.back * MountedCardAltitude + MarginalCardOffset * i, seekerSet?.Homing);
            cards[i].Turning.StartSeeking(transform.rotation.eulerAngles.z, seekerSet?.Turning);
            cards[i].Scaling.StartSeeking(1, seekerSet?.Scaling);
        }
    }
}
