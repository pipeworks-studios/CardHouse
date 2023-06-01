using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickDetector : Toggleable
{
    public UnityEvent OnPress;
    public UnityEvent OnButtonClicked;

    public GateCollection<NoParams> ClickGates;

    void OnMouseDown()
    {
        if (IsActive && ClickGates.AllUnlocked(null))
        {
            OnPress.Invoke();
        }
    }

    void OnMouseUpAsButton()
    {
        if (IsActive && ClickGates.AllUnlocked(null))
        {
            OnButtonClicked.Invoke();
        }
    }
}
