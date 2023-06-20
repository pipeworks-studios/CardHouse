using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(CardGroup))]
    public class BlockAllDrops : Gate<DropParams>
    {
        protected override bool IsUnlockedInternal(DropParams gateParams)
        {
            return false;
        }
    }
}
