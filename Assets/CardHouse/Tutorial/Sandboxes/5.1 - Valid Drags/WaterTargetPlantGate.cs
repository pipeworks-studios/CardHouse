using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaterTargetPlantGate : Gate<TargetCardParams>
{
    public override bool IsUnlocked(TargetCardParams gateParams)
    {
        return gateParams.Target.GetComponent<Plant>()?.CanBeWatered() ?? false;
    }
}
