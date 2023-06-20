using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardHouse
{
    public class CurrencyCost : MonoBehaviour
    {
        [Serializable]
        public class CostWithLabel
        {
            public CurrencyQuantity Cost;
            public TextMeshPro Label;
        }

        public List<CostWithLabel> Cost;

        void Start()
        {
            foreach (var cost in Cost)
            {
                if (cost.Label != null)
                {
                    cost.Label.text = cost.Cost.Amount.ToString();
                }
            }
        }

        public void Activate()
        {
            foreach (var resource in Cost)
            {
                CurrencyRegistry.Instance.AdjustCurrency(resource.Cost.CurrencyType.Name, PhaseManager.Instance.PlayerIndex, -1 * resource.Cost.Amount);
            }
        }
    }
}
