using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardHouse/Deck Definition")]
public class DeckDefinition : ScriptableObject
{
    public Sprite CardBackArt;
    public List<CardDefinition> CardCollection;
}
