using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class CurrencyWallet
{
    public List<CurrencyContainer> Currencies;

    public CurrencyContainer FindCurrency(string name)
    {
        foreach (var resource in Currencies)
        {
            if (resource.CurrencyType.Name == name)
            {
                return resource;
            }
        }

        return null;
    }

    public bool CanAfford(List<CurrencyQuantity> Cost)
    {
        foreach (var holder in Cost)
        {
            var myHolder = FindCurrency(holder.CurrencyType.Name);
            if (myHolder == null || myHolder.Amount < holder.Amount)
            {
                return false;
            }
        }

        return true;
    }
}
