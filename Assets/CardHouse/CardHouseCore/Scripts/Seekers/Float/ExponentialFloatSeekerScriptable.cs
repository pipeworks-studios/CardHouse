using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Seekers/Float/Exponential")]
    public class ExponentialFloatSeekerScriptable : SeekerScriptable<float>
    {
        public float Gain = 8f;
        public float ArrivalDistance = 0.01f;

        public override Seeker<float> GetStrategy(params object[] args)
        {
            return new ExponentialFloatSeeker(Gain, ArrivalDistance);
        }
    }
}
