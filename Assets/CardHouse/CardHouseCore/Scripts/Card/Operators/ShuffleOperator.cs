using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardHouse
{
    public class ShuffleOperator : Activatable
    {
        [FormerlySerializedAs("Groups")]
        public List<CardGroup> GroupsToShuffleIntoDeck;
        public CardGroup Deck;
        public bool ShouldOverrideUpsideDownChance;
        [Range(0f, 1f)]
        public float UpsideDownChance;

        protected override void OnActivate()
        {
            foreach (var slot in GroupsToShuffleIntoDeck)
            {
                foreach (var card in slot.MountedCards.ToList())
                {
                    Deck.Mount(card);
                }
            }

            Deck.Shuffle(upsideDownChance: ShouldOverrideUpsideDownChance ? UpsideDownChance : -1);
        }
    }
}
