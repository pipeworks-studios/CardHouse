using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CurrencyChangeDetector : MonoBehaviour
{
    [FormerlySerializedAs("OnResourceChange")]
    public UnityEvent OnCurrencyChange;
    CurrencyRegistry MyCurrencyRegistry;
    
    void Start()
    {
        MyCurrencyRegistry = CurrencyRegistry.Instance;
        if (MyCurrencyRegistry != null)
        {
            MyCurrencyRegistry.OnCurrencyChanged += HandleCurrencyChanged;
        }
    }

    void OnDestroy()
    {
        if (MyCurrencyRegistry != null)
        {
            MyCurrencyRegistry.OnCurrencyChanged -= HandleCurrencyChanged;
        }
    }

    void HandleCurrencyChanged(int playerIndex, CurrencyWallet newResources)
    {
        OnCurrencyChange.Invoke();
    }
}
