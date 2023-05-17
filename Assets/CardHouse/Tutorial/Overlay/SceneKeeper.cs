using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneKeeper : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
