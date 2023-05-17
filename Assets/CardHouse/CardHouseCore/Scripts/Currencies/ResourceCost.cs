using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceCost : MonoBehaviour
{
    [Serializable]
    public class CostWithLabel
    {
        public ResourceQuantity Cost;
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
            ResourceRegistry.Instance.AdjustResource(resource.Cost.ResourceType.Name, PhaseManager.Instance.PlayerIndex, -1 * resource.Cost.Amount);
        }
    }
}
