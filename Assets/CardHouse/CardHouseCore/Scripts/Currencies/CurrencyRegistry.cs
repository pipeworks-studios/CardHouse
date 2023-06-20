using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CardHouse
{
    public class CurrencyRegistry : MonoBehaviour
    {
        public Text CurrentPlayerLabel;

        [FormerlySerializedAs("ResourceDisplayParent")]
        public Transform CurrencyDisplayParent;
        [FormerlySerializedAs("ResourceDisplayPrefab")]
        public GameObject CurrencyDisplayPrefab;

        [FormerlySerializedAs("PlayerCurrencies")]
        public List<CurrencyWallet> PlayerWallets;

        public static CurrencyRegistry Instance;

        PhaseManager MyPhaseManager;

        Dictionary<CurrencyContainer, CurrencyUI> CurrencyUILookup = new Dictionary<CurrencyContainer, CurrencyUI>();

        public Action<int, CurrencyWallet> OnCurrencyChanged;

        void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            MyPhaseManager = PhaseManager.Instance;
            if (MyPhaseManager == null)
            {
                Debug.LogError("Currency Registry requires a Phase Manager to work");
                return;
            }
            MyPhaseManager.OnPhaseChanged += HandlePhaseChange;

            if (PlayerWallets.Count > 0)
            {
                ShowPlayerWallet(0);
            }
        }

        void OnDestroy()
        {
            if (MyPhaseManager != null)
            {
                MyPhaseManager.OnPhaseChanged -= HandlePhaseChange;
            }
        }

        void HandlePhaseChange(Phase newPhase)
        {
            ShowPlayerWallet(PhaseManager.Instance.PlayerIndex);
        }

        void ShowPlayerWallet(int playerIndex)
        {
            CurrentPlayerLabel.text = string.Format("Player {0}", PhaseManager.Instance.PlayerIndex + 1);

            CurrencyUILookup.Clear();
            for (var i = 0; i < CurrencyDisplayParent.childCount; i++)
            {
                Destroy(CurrencyDisplayParent.GetChild(i).gameObject);
            }
            foreach (var currency in PlayerWallets[playerIndex].Currencies)
            {
                var newRow = Instantiate(CurrencyDisplayPrefab, CurrencyDisplayParent);
                var currencyUI = newRow.GetComponent<CurrencyUI>();
                CurrencyUILookup[currency] = currencyUI;
                currencyUI.Apply(currency);
            }
        }

        public int? GetCurrency(string name, int playerIndex)
        {
            return FindCurrency(name, playerIndex)?.Amount;
        }

        public int? GetCurrency(CurrencyScriptable resourceDef, int playerIndex)
        {
            return FindCurrency(resourceDef.name, playerIndex)?.Amount;
        }

        CurrencyContainer FindCurrency(string name, int playerIndex)
        {
            if (playerIndex < 0 || playerIndex >= PlayerWallets.Count)
                return null;

            return PlayerWallets[playerIndex].FindCurrency(name);
        }

        public void AdjustCurrency(string name, int playerIndex, int amount)
        {
            var currency = FindCurrency(name, playerIndex);
            if (currency != null)
            {
                currency.Adjust(amount);
                CurrencyUILookup[currency].Apply(currency);
                OnCurrencyChanged?.Invoke(playerIndex, PlayerWallets[playerIndex]);
            }
        }

        public void Refill(string name, int playerIndex)
        {
            var currency = FindCurrency(name, playerIndex);
            if (currency != null)
            {
                currency.Amount = currency.HasMax ? currency.Max : currency.Amount > currency.RefillValue ? currency.Amount : currency.RefillValue;
                if (CurrencyUILookup.ContainsKey(currency))
                {
                    CurrencyUILookup[currency].Apply(currency);
                }
                OnCurrencyChanged?.Invoke(playerIndex, PlayerWallets[playerIndex]);
            }
        }
    }
}
