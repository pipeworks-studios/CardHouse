using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ClickDetector))]
public class PhaseGateClick : Gate<NoParams>
{
    ClickDetector MyClickDetector;
    private void Awake()
    {
        MyClickDetector = GetComponent<ClickDetector>();
    }
    protected override bool IsUnlockedInternal(NoParams gateParams)
    {
        return PhaseManager.Instance.IsValidClick(MyClickDetector);
    }
}
