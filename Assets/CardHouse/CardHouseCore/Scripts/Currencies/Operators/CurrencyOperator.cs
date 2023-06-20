using UnityEngine;

namespace CardHouse
{
    public abstract class CurrencyOperator : MonoBehaviour
    {
        protected CurrencyRegistry MyRegistry;

        void Start()
        {
            MyRegistry = CurrencyRegistry.Instance;
            if (MyRegistry == null)
            {
                Debug.LogWarningFormat("{0}: Missing SpriteRenderer for Sprite Response to operate on", name);
            }
        }

        public void Activate()
        {
            if (MyRegistry == null)
                return;

            AdjustCurrencies();
        }

        protected abstract void AdjustCurrencies();
    }
}
