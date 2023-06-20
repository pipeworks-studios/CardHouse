using UnityEngine;

namespace CardHouse
{
    public class DiscardCardOperator : MonoBehaviour
    {
        public void Activate()
        {
            var card = GetComponentInParent<Card>();
            var discardGroup = card.GetDiscardGroup();
            if (discardGroup != null)
            {
                discardGroup.Mount(card);
            }
        }
    }
}
