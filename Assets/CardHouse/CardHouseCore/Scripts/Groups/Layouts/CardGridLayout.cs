using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class CardGridLayout : CardGroupSettings
    {
        public int CardsPerRow = 5;
        public float MarginalCardOffset = 0.05f;
        Collider2D MyCollider;
        public bool Straighten = true;

        void Awake()
        {
            MyCollider = GetComponent<Collider2D>();
            if (MyCollider == null)
            {
                Debug.LogWarningFormat("{0}:{1} needs Collider2D on its game object to function.", gameObject.name, "GridLayout");
            }
        }

        protected override void ApplySpacing(List<Card> cards, SeekerSetList seekerSets)
        {
            var width = transform.lossyScale.x;
            var height = transform.lossyScale.y;

            var rowCount = 1 + (cards.Count - 1) / CardsPerRow;
            var colSpacing = height / (rowCount + 1);

            for (var row = 0; row < rowCount; row++)
            {
                var cardsInThisRow = Mathf.Min(CardsPerRow, cards.Count - row * CardsPerRow);
                var rowSpacing = width / (cardsInThisRow + 1);
                for (var col = 0; col < cardsInThisRow; col++)
                {
                    var newPos = transform.position
                                 + transform.right * width * -0.5f
                                 + transform.right * (col + 1) * rowSpacing
                                 + transform.up * height * 0.5f
                                 + transform.up * (row + 1) * colSpacing * -1
                                 + transform.forward * (MountedCardAltitude + MarginalCardOffset * (row * CardsPerRow + col)) * -1;

                    var cardIndex = row * CardsPerRow + col;
                    var card = cards[cardIndex];
                    var seekerSet = seekerSets?.GetSeekerSetFor(card);
                    card.Homing.StartSeeking(newPos, seekerSet?.Homing);
                    if (Straighten)
                    {
                        card.Turning.StartSeeking(transform.rotation.eulerAngles.z, seekerSet?.Turning);
                    }
                    card.Scaling.StartSeeking(UseMyScale ? transform.lossyScale.y : 1, seekerSet?.Scaling);
                }
            }
        }
    }
}
