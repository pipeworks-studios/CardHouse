using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    [RequireComponent(typeof(Card)), RequireComponent(typeof(DragDetector))]
    public class LoyaltyGateCardDrop : Gate<DropParams>
    {
        public Loyalty Loyalty;
        public List<GroupName> Destinations;

        void Awake()
        {
        }

        protected override bool IsUnlockedInternal(DropParams gateParams)
        {
            var groupLoyalty = GroupRegistry.Instance.GetLoyalty(gateParams.Target, PhaseManager.Instance.PlayerIndex);
            return (Loyalty & groupLoyalty) != 0;
        }
    }
}