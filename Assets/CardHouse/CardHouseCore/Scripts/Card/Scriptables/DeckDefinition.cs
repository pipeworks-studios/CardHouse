using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Deck Definition")]
    public class DeckDefinition : ScriptableObject
    {
        public Sprite CardBackArt;
        public List<CardDefinition> CardCollection;
    }
}
