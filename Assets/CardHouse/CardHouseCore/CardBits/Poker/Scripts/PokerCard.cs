using UnityEngine;

namespace CardHouse
{
    public class PokerCard : CardSetup
    {
        public SpriteRenderer Image;
        public SpriteRenderer BackImage;
        public PokerSuit Suit { get; private set; }
        public int Rank { get; private set; }

        public override void Apply(CardDefinition data)
        {
            if (data is PokerCardDefinition pokerCard)
            {
                Image.sprite = pokerCard.Art;
                if (pokerCard.BackArt != null)
                {
                    BackImage.sprite = pokerCard.BackArt;
                }
                Rank = pokerCard.Rank;
                Suit = pokerCard.Suit;
            }
        }
    }
}
