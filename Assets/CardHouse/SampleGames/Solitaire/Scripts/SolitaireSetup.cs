using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardHouse.SampleGames.Solitaire
{
    public class SolitaireSetup : MonoBehaviour
    {
        public SeekerScriptable<Vector3> DealingStrategy;
        public CardGroup Deck;
        public List<CardGroup> Columns;
        public List<CardGroup> AllGroups;
        public EventChain ResetBoardEventChain;

        bool CanDoSetup = true;

        public void TryResetBoard()
        {
            if (CanDoSetup)
            {
                ResetBoardEventChain.Activate();
            }
        }

        public void DealCards()
        {
            StartCoroutine(RiffleDeal());
        }

        IEnumerator RiffleDeal()
        {
            var cardCount = 0;
            for (var i = 0; i < Columns.Count; i++)
            {
                cardCount += i + 1;
            }

            var delayBetweenCards = 2f / (2f * cardCount);
            for (var i = 0; i < Columns.Count; i++)
            {
                var column = Columns[i];
                for (var j = 0; j < i; j++)
                {
                    var card = Deck.Get();
                    column.Mount(card);
                    card.SetFacing(CardFacing.FaceDown);

                    yield return new WaitForSeconds(delayBetweenCards);
                }
                var faceUpCard = Deck.Get();
                column.Mount(faceUpCard, seekerSets: new SeekerSetList { new SeekerSet { Homing = DealingStrategy?.GetStrategy() } });
                faceUpCard.SetFacing(CardFacing.FaceUp);
                faceUpCard.GetComponent<Collider2D>().enabled = true;

                yield return new WaitForSeconds(delayBetweenCards);
            }
        }

        public void PreventReset()
        {
            CanDoSetup = false;
        }

        public void AllowReset()
        {
            CanDoSetup = true;
        }
    }
}
