using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class DeckSetup : MonoBehaviour
    {
        public bool RunOnStart = true;
        public GameObject CardPrefab;
        public DeckDefinition DeckDefinition;
        public CardGroup Deck;
        public List<TimedEvent> OnSetupCompleteEventChain;

        void Start()
        {
            if (RunOnStart)
            {
                DoSetup();
            }
        }

        public void DoSetup()
        {
            StartCoroutine(SetupCoroutine());
        }

        IEnumerator SetupCoroutine()
        {
            var newCardList = new List<Card>();
            foreach (var cardDef in DeckDefinition.CardCollection)
            {
                var newThing = Instantiate(CardPrefab, Deck.transform.position, Deck.transform.rotation);
                newCardList.Add(newThing.GetComponent<Card>());
                var card = newThing.GetComponent<CardSetup>();

                if (card != null)
                {
                    var copyCardDef = cardDef;

                    if (cardDef.BackArt == null && DeckDefinition.CardBackArt != null)
                    {
                        copyCardDef = Instantiate(cardDef);
                        copyCardDef.BackArt = DeckDefinition.CardBackArt;
                    }
                    card.Apply(copyCardDef);
                }
            }

            yield return new WaitForEndOfFrame();

            foreach (var card in newCardList)
            {
                Deck.Mount(card, instaFlip: true);
            }

            Deck.Shuffle();

            StartCoroutine(TimedEvent.ExecuteChain(OnSetupCompleteEventChain));
        }
    }
}
