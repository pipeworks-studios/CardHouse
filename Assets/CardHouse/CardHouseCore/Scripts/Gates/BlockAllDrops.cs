using UnityEngine;

[RequireComponent(typeof(CardGroup))]
public class BlockAllDrops : Gate<DropParams>
{
    CardGroup MyGroup;

    void Awake()
    {
        MyGroup = GetComponent<CardGroup>();
    }

    public override bool IsUnlocked(DropParams gateParams)
    {
        return false;
    }
}
