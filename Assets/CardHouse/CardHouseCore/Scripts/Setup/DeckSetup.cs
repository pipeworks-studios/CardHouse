using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeckSetup : MonoBehaviour
{
    public GameObject CardPrefab;
    public DeckDefinition DeckDefinition;
    public CardGroup Deck;
    public List<TimedEvent> OnSetupCompleteEventChain;

    void Start()
    {
        foreach (var cardDef in DeckDefinition.CardCollection)
        {
            var newThing = Instantiate(CardPrefab, Deck.transform.position, Deck.transform.rotation);
            Deck.Mount(newThing.GetComponent<Card>(), instaFlip: true);
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

        Deck.Shuffle();

        StartCoroutine(TimedEvent.ExecuteChain(OnSetupCompleteEventChain));
    }
}
