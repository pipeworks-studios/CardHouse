using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace CardHouse
{
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
}
