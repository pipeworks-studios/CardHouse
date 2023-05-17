using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TarotSpread
{
    public string Name;
    [TextArea(1, 15)]
    public string Instructions;
    public List<CardGroup> Slots;

    public void FillNext(Card card)
    {
        foreach (var slot in Slots)
        {
            if (slot.HasRoom())
            {
                slot.Mount(card);
                break;
            }
        }
    }
}
