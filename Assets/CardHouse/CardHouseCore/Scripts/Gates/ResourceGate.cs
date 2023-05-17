using UnityEngine;

[RequireComponent(typeof(ResourceCost))]
public class ResourceGate : Gate<NoParams>
{
    ResourceCost MyCost;

    void Awake()
    {
        MyCost = GetComponent<ResourceCost>();
    }

    public override bool IsUnlocked(NoParams gateParams)
    {
        foreach (var resourceCost in MyCost.Cost)
        {
            var amountPlayerHas = ResourceRegistry.Instance.GetResource(resourceCost.Cost.ResourceType, PhaseManager.Instance.PlayerIndex);
            if (amountPlayerHas < resourceCost.Cost.Amount)
            {
                return false;
            }
        }
        return true;
    }
}
