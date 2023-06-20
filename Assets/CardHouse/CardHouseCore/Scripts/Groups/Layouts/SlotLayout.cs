using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class SlotLayout : CardGroupSettings
    {
        protected override void ApplySpacing(List<Card> cards, SeekerSetList seekerSets = null)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var card = cards[i];
                var seekerSet = seekerSets?.GetSeekerSetFor(card);
                card.Homing.StartSeeking(transform.position + Vector3.back * MountedCardAltitude, seekerSet?.Homing);
                card.Turning.StartSeeking(transform.rotation.eulerAngles.z, seekerSet?.Turning);
                card.Scaling.StartSeeking(UseMyScale ? transform.lossyScale.y : 1, seekerSet?.Scaling);
            }
        }
    }
}
