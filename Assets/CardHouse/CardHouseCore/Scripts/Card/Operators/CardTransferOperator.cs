using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardHouse
{
    public class CardTransferOperator : Activatable
    {
        public GroupTransition Transition;
        public GroupTargetType GrabFrom = GroupTargetType.Last;
        public GroupTargetType SendTo = GroupTargetType.Last;
        public int NumberToTransfer = 1;
        public float FlipSpeed = 1;

        public SeekerScriptable<Vector3> PopPushHomingOverride;

        public List<TimedEvent> OnSourceDepletedEventChain;

        public bool TryAgainAfterSourceDepleted;

        protected override void OnActivate()
        {
            var cardsToMove = NumberToTransfer > 0 ? Transition.Source.Get(GrabFrom, NumberToTransfer) : Transition.Source.MountedCards.ToList();

            TransferCards(cardsToMove);

            if (NumberToTransfer > cardsToMove.Count)
            {
                StartCoroutine(ExecuteOnSourceDepletedEventChain());
            }
        }

        IEnumerator ExecuteOnSourceDepletedEventChain()
        {
            yield return TimedEvent.ExecuteChain(OnSourceDepletedEventChain);
            if (TryAgainAfterSourceDepleted)
            {
                var cardsToMove = Transition.Source.Get(GrabFrom, NumberToTransfer);
                TransferCards(cardsToMove);
            }
        }

        void TransferCards(List<Card> cards)
        {
            foreach (var card in cards)
            {
                Transition.Destination.Mount(card, SendTo == GroupTargetType.First ? -1 : SendTo == GroupTargetType.Last ? null : UnityEngine.Random.Range(0, Transition.Destination.MountedCards.Count + 1), seekerSets: new SeekerSetList { new SeekerSet { Card = card, Homing = PopPushHomingOverride?.GetStrategy(), FlipSpeed = FlipSpeed } });
            }
        }
    }
}