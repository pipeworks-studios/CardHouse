using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSpriteOperator : MonoBehaviour
{
    public List<SpriteOperator> SpriteOperators;

    public void Activate(string name, Object voter)
    {
        foreach (var handler in SpriteOperators)
        {
            handler?.Activate(name, voter);
        }
    }
}
