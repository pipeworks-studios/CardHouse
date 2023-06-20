using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardHouse.Tutorial
{
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
}