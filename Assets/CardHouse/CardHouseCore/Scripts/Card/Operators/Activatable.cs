using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activatable : MonoBehaviour
{
    public void Activate()
    {
        OnActivate();
    }

    protected virtual void OnActivate() { }
}
