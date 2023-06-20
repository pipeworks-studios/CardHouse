using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Card Definition/Tarot")]
    public class TarotCardDefinition : CardDefinition
    {
        public ArcanaData Data;
        public Sprite Art;
    }
}
