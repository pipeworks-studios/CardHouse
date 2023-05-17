using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Card)), RequireComponent(typeof(DragDetector))]
public class LoyaltyGateCardDrop : Gate<DropParams>
{
    public Loyalty Loyalty;
    public List<GroupName> Destinations;

    void Awake()
    {
    }

    public override bool IsUnlocked(DropParams gateParams)
    {
        var groupLoyalty = GroupRegistry.Instance.GetLoyalty(gateParams.Target, PhaseManager.Instance.PlayerIndex);
        return (Loyalty & groupLoyalty) != 0;
    }
}