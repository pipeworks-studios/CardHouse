using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Card Definition/Poker")]
    public class PokerCardDefinition : CardDefinition
    {
        public int Rank;
        public PokerSuit Suit;
        public Sprite Art;
    }
}
