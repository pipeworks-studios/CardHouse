using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantGrowthScriptable : ScriptableObject
{
    public List<PlantMaturityInfo> Stages;
}

[Serializable]
public class PlantMaturityInfo
{
    public string Name;
    public string Description;
    public Sprite Sprite;
}