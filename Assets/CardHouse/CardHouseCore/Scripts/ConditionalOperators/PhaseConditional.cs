using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhaseConditional : Activatable
{
    public List<StringUnityActionKvp> Responses;

    protected override void OnActivate()
    {
        foreach (var kvp in Responses)
        {
            if (kvp.Key == PhaseManager.Instance?.CurrentPhase.Name)
            {
                kvp.Value.Invoke();
                break;
            }
        }
    }
}

[Serializable]
public class StringUnityActionKvp
{
    public string Key;
    public UnityEvent Value;
}