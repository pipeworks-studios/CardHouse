using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTargetOperator : CardTargetCardOperator
{
    public int Damage;

    protected override void ActOnTarget()
    {
        var health = Target.GetComponent<Health>();
        if (health == null)
            return;

        health.Change(-1 * Damage);
    }
}
