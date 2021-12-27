using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageGroupOperator : CardTargetCardOperator
{
    public int Damage;

    protected override void ActOnTarget()
    {
        foreach (var target in Target.Group.MountedCards.ToArray())
        {
            var health = target.GetComponent<Health>();
            if (health == null)
                return;

            health.Change(-1 * Damage);
        }
    }
}
