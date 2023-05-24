using System;

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
}
