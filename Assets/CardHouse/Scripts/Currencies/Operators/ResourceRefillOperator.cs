using System.Collections.Generic;
using UnityEngine;

public class ResourceRefillOperator : ResourceOperator
{
    public List<ResourceScriptable> ResourcesToRefill;

    protected override void AdjustResources()
    {
        foreach (var resource in ResourcesToRefill)
        {
            MyRegistry.Refill(resource.name, PhaseManager.Instance.PlayerIndex);
        }
    }
}
