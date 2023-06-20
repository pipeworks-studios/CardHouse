using System.Collections.Generic;
using UnityEngine.Serialization;

namespace CardHouse
{
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
}
