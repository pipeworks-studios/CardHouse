using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    public class SpriteColorOperator : SpriteOperator
    {
        [Serializable]
        public class NamedColor
        {
            public string Name;
            public Color Color;
        }

        public List<NamedColor> Colors;

        protected override void ChangeSprite(string name)
        {
            foreach (var namedColor in Colors)
            {
                if (namedColor.Name == name)
                {
                    SpriteTarget.color = namedColor.Color;
                    break;
                }
            }
        }
    }
}
