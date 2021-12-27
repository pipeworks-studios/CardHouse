using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HoverDetector : Toggleable
{
    public UnityEvent OnHover;
    public UnityEvent OnUnHover;

    void OnMouseEnter()
    {
        if (!IsActive)
            return;

        OnHover.Invoke();
    }

    void OnMouseExit()
    {
        if (!IsActive)
            return;

        OnUnHover.Invoke();
    }
}
