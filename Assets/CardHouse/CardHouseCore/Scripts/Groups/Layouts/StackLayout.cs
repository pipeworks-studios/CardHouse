using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class StackLayout : CardGroupSettings
    {
        public Vector3 MarginalCardOffset = new Vector3(0.01f, 0.01f, -0.01f);
        public TriggerEnterRelay SecondaryCollider;
        public bool Straighten = true;

        protected override void ApplySpacing(List<Card> cards, SeekerSetList seekerSets = null)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var seekerSet = seekerSets?.GetSeekerSetFor(cards[i]);
                cards[i].Homing.StartSeeking(transform.position + Vector3.back * MountedCardAltitude + MarginalCardOffset * i, seekerSet?.Homing);
                if (Straighten)
                {
                    cards[i].Turning.StartSeeking(transform.rotation.eulerAngles.z, seekerSet?.Turning);
                }
                cards[i].Scaling.StartSeeking(UseMyScale ? transform.lossyScale.y : 1, seekerSet?.Scaling);
            }

            if (SecondaryCollider != null && cards.Count > 0)
            {
                SecondaryCollider.transform.position = transform.position + Vector3.forward + MarginalCardOffset * (cards.Count - 1);
            }
        }
    }
}
