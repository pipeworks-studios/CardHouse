using System.Collections.Generic;
using UnityEngine.Serialization;

namespace CardHouse
{
    public class IncrementCurrencyOperator : CurrencyOperator
    {
        [FormerlySerializedAs("ResourcesToChange")]
        public List<CurrencyQuantity> CurrenciesToChange;

        protected override void AdjustCurrencies()
        {
            foreach (var resource in CurrenciesToChange)
            {
                MyRegistry.AdjustCurrency(resource.CurrencyType.Name, PhaseManager.Instance.PlayerIndex, resource.Amount);
            }
        }
    }
}
