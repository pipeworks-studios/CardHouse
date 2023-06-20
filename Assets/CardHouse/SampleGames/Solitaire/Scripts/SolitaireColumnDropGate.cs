using System.Collections.Generic;

namespace CardHouse.SampleGames.Solitaire
{
    public class SolitaireColumnDropGate : Gate<DropParams>
    {
        protected override bool IsUnlockedInternal(DropParams gateParams)
        {
            var topCard = gateParams.Target.Get();
            var pokerCard = gateParams.Card.GetComponent<PokerCard>();
            if (topCard == null)
            {
                return pokerCard.Rank == 13; // King
            }
            else
            {
                var topPokerCard = topCard.GetComponent<PokerCard>();
                return !IsColorMatch(pokerCard, topPokerCard) && pokerCard.Rank == topPokerCard.Rank - 1;
            }
        }

        bool IsColorMatch(PokerCard a, PokerCard b)
        {
            var redSuits = new List<PokerSuit> { PokerSuit.Hearts, PokerSuit.Diamonds };
            return redSuits.Contains(a.Suit) == redSuits.Contains(b.Suit);
        }
    }
}
