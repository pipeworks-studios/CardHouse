using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardCardOperator : MonoBehaviour
{
    public void Activate()
    {
        var card = GetComponentInParent<Card>();
        var discardGroup = GroupRegistry.Instance?.Get(GroupName.Discard, GroupRegistry.Instance.GetOwnerIndex(card.Group));
        if (discardGroup != null)
        {
            discardGroup.Mount(card);
        }
    }
}
