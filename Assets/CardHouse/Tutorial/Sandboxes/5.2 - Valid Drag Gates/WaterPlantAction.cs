using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPlantAction : CardTargetCardOperator
{
    protected override void ActOnTarget()
    {
        var plant = Target.GetComponent<Plant>();
        if (plant != null)
        {
            plant.Water();
        }
    }
}
