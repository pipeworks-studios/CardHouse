using UnityEngine;

namespace CardHouse
{
    public class TarotCard : CardSetup
    {
        public enum Arcana
        {
            Minor,
            Major
        }

        public SpriteRenderer Image;

        public ArcanaData ArcanaData { get; private set; }
        public Arcana ArcanaType { get { return ArcanaData?.Arcana == null ? Arcana.Minor : Arcana.Major; } }

        public override void Apply(CardDefinition cardDef)
        {
            if (cardDef is TarotCardDefinition tarotCard)
            {
                ArcanaData = tarotCard.Data;
                Image.sprite = tarotCard.Art;
            }
        }
    }
}
