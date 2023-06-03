using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCardOperator : MonoBehaviour
{
    public void Activate()
    {
        var card = GetComponentInParent<Card>();
        var discardGroup = card.GetDiscardGroup();
        if (discardGroup != null)
        {
            discardGroup.Mount(card);
        }
    }
}
