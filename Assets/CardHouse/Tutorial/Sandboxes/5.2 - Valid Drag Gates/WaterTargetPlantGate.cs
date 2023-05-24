using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterTargetPlantGate : Gate<TargetCardParams>
{
    protected override bool IsUnlockedInternal(TargetCardParams gateParams)
    {
        return gateParams.Target.GetComponent<Plant>()?.CanBeWatered() ?? false;
    }
}
