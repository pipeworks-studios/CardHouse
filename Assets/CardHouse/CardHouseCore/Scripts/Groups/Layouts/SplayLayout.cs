using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class SplayLayout : CardGroupSettings
    {
        public float MarginalCardOffset = 0.01f;
        public Vector2 ArcCenterOffset = new Vector2(0f, -5f);
        [Range(0f, 0.8f)]
        public float ArcMargin = 0.3f;
        Collider2D MyCollider;

        void Awake()
        {
            MyCollider = GetComponent<Collider2D>();
            if (MyCollider == null)
            {
                Debug.LogWarningFormat("{0}:{1} needs Collider2D on its game object to function.", gameObject.name, "SplayLayout");
            }
        }

        private void Start()
        {
            ArcCenterOffset = transform.position + transform.right * ArcCenterOffset.x + transform.up * ArcCenterOffset.y;
        }

        protected override void ApplySpacing(List<Card> cards, SeekerSetList seekerSets = null)
        {
            var width = transform.lossyScale.x * (1f - ArcMargin);
            var spacing = width / (cards.Count + 1);
            for (var i = 0; i < cards.Count; i++)
            {
                var newPos = transform.position
                             + Vector3.back * (MountedCardAltitude + i * MarginalCardOffset)
                             + transform.right * width * -0.5f
                             + transform.right * (i + 1) * spacing;

                var seekerSet = seekerSets?.GetSeekerSetFor(cards[i]);
                cards[i].Homing.StartSeeking(newPos, seekerSet?.Homing);

                var newAngle = Mathf.Atan2(newPos.y - ArcCenterOffset.y, newPos.x - ArcCenterOffset.x) * Mathf.Rad2Deg - 90;
                cards[i].Turning.StartSeeking(newAngle, seekerSet?.Turning);

                cards[i].Scaling.StartSeeking(UseMyScale ? transform.lossyScale.y : 1, seekerSet?.Scaling);
            }
        }
    }
}
