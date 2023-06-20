using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(CurrencyCost))]
    public class CurrencyGate : Gate<NoParams>
    {
        CurrencyCost MyCost;

        void Awake()
        {
            MyCost = GetComponent<CurrencyCost>();
        }

        protected override bool IsUnlockedInternal(NoParams gateParams)
        {
            foreach (var resourceCost in MyCost.Cost)
            {
                var amountPlayerHas = CurrencyRegistry.Instance.GetCurrency(resourceCost.Cost.CurrencyType, PhaseManager.Instance.PlayerIndex);
                if (amountPlayerHas < resourceCost.Cost.Amount)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
