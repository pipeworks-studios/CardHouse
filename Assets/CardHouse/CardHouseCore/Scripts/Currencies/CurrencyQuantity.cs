using System;
using UnityEngine.Serialization;

[Serializable]
public class CurrencyQuantity
{
    [FormerlySerializedAs("ResourceType")]
    public CurrencyScriptable CurrencyType;
    public int Amount;
}