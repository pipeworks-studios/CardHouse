using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatureCropDragGate : Gate<NoParams>
{
    Card MyCard;

    private void Awake()
    {
        MyCard = GetComponent<Card>();
    }

    public override bool IsUnlocked(NoParams gateParams)
    {
        if (MyCard.Group != GroupRegistry.Instance.Get(GroupName.Board, null))
            return true;

        return MyCard.GetComponent<Plant>()?.CanBeWatered() != true;
    }
}
