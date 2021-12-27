using System.Collections.Generic;

public class SolitaireScorePileDropGate : Gate<DropParams>
{
    public override bool IsUnlocked(DropParams gateParams)
    {
        var topCard = gateParams.Target.Get();
        var pokerCard = gateParams.Card.GetComponent<PokerCard>();
        if (topCard == null)
        {
            return pokerCard.Rank == 1; // Ace
        }
        else
        {
            var topPokerCard = topCard.GetComponent<PokerCard>();
            return pokerCard.Suit == topPokerCard.Suit && pokerCard.Rank == topPokerCard.Rank + 1;
        }
    }
}
