using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShuffleOperator : Activatable
{
    public List<CardGroup> Groups;
    public CardGroup Deck;

    protected override void OnActivate()
    {
        foreach (var slot in Groups)
        {
            foreach (var card in slot.MountedCards.ToList())
            {
                Deck.Mount(card);
            }
        }

        Deck.Shuffle();
    }
}
