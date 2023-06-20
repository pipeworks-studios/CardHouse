using System.Collections.Generic;
using UnityEngine;

namespace CardHouse.SampleGames.Solitaire
{
    [RequireComponent(typeof(CardGroup))]
    public class SolitaireDeckClickHandler : MonoBehaviour
    {
        public CardTransferOperator FlipHandler;
        public CardTransferOperator MoveToDeckHandler;
        public ShuffleOperator ShuffleHandler;
        public CardTransferOperator DealCardHandler;
        public List<TimedEvent> ResetEventChain;

        CardGroup MyGroup;

        void Awake()
        {
            MyGroup = GetComponent<CardGroup>();
        }

        public void FlipOrReset()
        {
            if (MyGroup.MountedCards.Count == 0)
            {
                StartCoroutine(TimedEvent.ExecuteChain(ResetEventChain));
            }
            else
            {
                FlipHandler.Activate();
            }
        }
    }
}
