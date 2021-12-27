using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class ResourceWallet
{
    public List<ResourceContainer> Resources;

    public ResourceContainer FindResource(string name)
    {
        foreach (var resource in Resources)
        {
            if (resource.ResourceType.Name == name)
            {
                return resource;
            }
        }

        return null;
    }

    public bool CanAfford(List<ResourceQuantity> Cost)
    {
        foreach (var holder in Cost)
        {
            var myHolder = FindResource(holder.ResourceType.Name);
            if (myHolder == null || myHolder.Amount < holder.Amount)
            {
                return false;
            }
        }

        return true;
    }
}
