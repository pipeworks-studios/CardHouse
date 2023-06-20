using UnityEngine;

namespace CardHouse.Tutorial
{
    public class ClosestCardHighlighter : MonoBehaviour
    {
        bool IsActive;
        CardGroup MyGroup;

        private void Start()
        {
            MyGroup = GetComponent<CardGroup>();
            CardGroup.OnNewActiveGroup += HandleNewActiveGroup;
        }

        void HandleNewActiveGroup(CardGroup group)
        {
            IsActive = group == MyGroup;
        }

        void Update()
        {

            var dragTarget = Dragging.Instance?.GetTarget();
            if (IsActive && dragTarget != null)
            {

                var closestIndex = MyGroup.GetClosestMountedCardIndex(dragTarget.transform.position);
                if (closestIndex == null)
                    return;

                var diff = MyGroup.MountedCards[(int)closestIndex].transform.position - dragTarget.transform.position;
                var insertPoint = diff.x > 0 ? closestIndex : closestIndex + 1;

                for (var i = 0; i < MyGroup.MountedCards.Count; i++)
                {
                    SetHighlightState(MyGroup.MountedCards[i], i == insertPoint);
                }
            }
            else
            {
                foreach (var card in MyGroup.MountedCards)
                {
                    SetHighlightState(card, true);
                }
            }

        }

        void SetHighlightState(Card card, bool state)
        {
            card.GetComponentInChildren<SpriteColorOperator>().Activate(state ? "Active" : "Dim");
        }
    }
}
