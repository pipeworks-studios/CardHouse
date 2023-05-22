using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
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