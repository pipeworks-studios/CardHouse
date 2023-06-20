using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Currency")]
    public class CurrencyScriptable : ScriptableObject
    {
        public string Name;
        public Sprite Sprite;
    }
}
