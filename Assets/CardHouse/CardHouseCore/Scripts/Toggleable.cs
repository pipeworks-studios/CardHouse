using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggleable : MonoBehaviour
{
    public bool IsActive = true;

    public void SetIsActive(bool newValue)
    {
        IsActive = newValue;
    }
}
