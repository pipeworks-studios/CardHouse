using UnityEngine;

namespace CardHouse.SampleGames.Solitaire
{
    [RequireComponent(typeof(CardGroup))]
    public class SolitaireColumnChangeHandler : MonoBehaviour
    {
        CardGroup MyGroup;

        void Awake()
        {
            MyGroup = GetComponent<CardGroup>();
        }

        public void Refresh()
        {
            foreach (var card in MyGroup.MountedCards)
            {
                if (card == MyGroup.Get())
                {
                    card.SetFacing(CardFacing.FaceUp);
                }

                card.GetComponent<Collider2D>().enabled = card.Facing == CardFacing.FaceUp;
            }
        }
    }
}
