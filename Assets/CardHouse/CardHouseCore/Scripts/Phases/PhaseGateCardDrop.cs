using UnityEngine;

[RequireComponent(typeof(CardGroup))]
public class PhaseGateCardDrop : Gate<DropParams>
{
    CardGroup MyGroup;

    void Awake()
    {
        MyGroup = GetComponent<CardGroup>();
    }

    public override bool IsUnlocked(DropParams gateParams)
    {
        return PhaseManager.Instance?.IsValidDrag(gateParams.Source, MyGroup, gateParams.DragType) ?? true;
    }
}
