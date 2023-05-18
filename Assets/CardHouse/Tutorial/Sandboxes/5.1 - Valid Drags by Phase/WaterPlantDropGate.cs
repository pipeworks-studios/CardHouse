using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardGroup))]
public class WaterPlantDropGate : Gate<DropParams>
{
    public override bool IsUnlocked(DropParams gateParams)
    {
        if (gateParams.DragType == DragAction.Mount) 
            return true;

        var closestCardIndex = gateParams.Target.GetClosestMountedCardIndex(gateParams.Card.transform.position);
        return closestCardIndex != null && (gateParams.Target.MountedCards[(int)closestCardIndex].GetComponent<Plant>()?.CanBeWatered() ?? false);
    }
}
