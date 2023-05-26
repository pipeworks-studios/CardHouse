using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ShuffleOperator : Activatable
{
    [FormerlySerializedAs("Groups")]
    public List<CardGroup> GroupsToShuffleIntoDeck;
    public CardGroup Deck;

    protected override void OnActivate()
    {
        foreach (var slot in GroupsToShuffleIntoDeck)
        {
            foreach (var card in slot.MountedCards.ToList())
            {
                Deck.Mount(card);
            }
        }

        Deck.Shuffle();
    }
}
