using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Seekers/Vector3/Exponential")]
    public class ExponentialVector3SeekerScriptable : SeekerScriptable<Vector3>
    {
        public float XYGain = 8f;
        public float ZGain = 10f;
        public float ArrivalDistance = 0.01f;

        public override Seeker<Vector3> GetStrategy(params object[] args)
        {
            return new ExponentialVector3Seeker(XYGain, ZGain, ArrivalDistance);
        }
    }
}
