using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CurrencyRefillOperator : CurrencyOperator
{
    [FormerlySerializedAs("ResourcesToRefill")]
    public List<CurrencyScriptable> CurrenciesToRefill;

    protected override void AdjustCurrencies()
    {
        foreach (var resource in CurrenciesToRefill)
        {
            MyRegistry.Refill(resource.name, PhaseManager.Instance.PlayerIndex);
        }
    }
}
