using UnityEngine;

[RequireComponent(typeof(CardGroup))]
public class BlockAllDrops : Gate<DropParams>
{
    public override bool IsUnlocked(DropParams gateParams)
    {
        return false;
    }
}
