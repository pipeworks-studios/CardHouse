using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardHouse/Card Definition/Poker")]
public class PokerCardDefinition : CardDefinition
{
    public int Rank;
    public PokerSuit Suit;
    public Sprite Art;
}
