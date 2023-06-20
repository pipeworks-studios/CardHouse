using System;
using UnityEngine;

namespace CardHouse
{
    [Serializable]
    public class SeekerScriptableSet
    {
        public SeekerScriptable<Vector3> Homing;
        public SeekerScriptable<float> Turning;
        public SeekerScriptable<float> Scaling;
    }
}
