using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class SpriteImageOperator : SpriteOperator
    {
        [Serializable]
        public class NamedSprite
        {
            public string Name;
            public Sprite Sprite;
        }

        public List<NamedSprite> Sprites;

        protected override void ChangeSprite(string name)
        {
            foreach (var sprite in Sprites)
            {
                if (sprite.Name == name)
                {
                    SpriteTarget.sprite = sprite.Sprite;
                    break;
                }
            }
        }
    }
}
