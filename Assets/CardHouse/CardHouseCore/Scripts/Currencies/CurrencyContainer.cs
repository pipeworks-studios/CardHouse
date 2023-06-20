using System;

namespace CardHouse
{
    [Serializable]
    public class CurrencyContainer : CurrencyQuantity
    {
        public bool HasMax;
        public int Max;
        public bool HasMin = true;
        public int Min;
        public int RefillValue;

        public void Adjust(int amount)
        {
            Amount += amount;
            if (HasMin && Amount < Min)
            {
                Amount = Min;
            }
            if (HasMax && Amount > Max)
            {
                Amount = Max;
            }
        }

        public override object Clone()
        {
            return new CurrencyContainer { CurrencyType = CurrencyType, Amount = Amount, HasMax = HasMax, Max = Max, HasMin = HasMin, Min = Min, RefillValue = RefillValue };
        }
    }
}
