using System;
using UnityEngine.Serialization;

namespace CardHouse
{
    [Serializable]
    public class CurrencyQuantity : ICloneable
    {
        [FormerlySerializedAs("ResourceType")]
        public CurrencyScriptable CurrencyType;
        public int Amount;

        public virtual object Clone()
        {
            return new CurrencyQuantity { CurrencyType = CurrencyType, Amount = Amount };
        }
    }
}