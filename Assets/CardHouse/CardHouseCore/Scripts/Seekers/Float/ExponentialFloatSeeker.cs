using UnityEngine;

namespace CardHouse
{
    public class ExponentialFloatSeeker : Seeker<float>
    {
        protected float Gain;
        protected float ArrivalDistance;

        public ExponentialFloatSeeker(float gain = 8f, float arrivalDist = 0.01f)
        {
            Gain = gain;
            ArrivalDistance = arrivalDist;
        }

        public override Seeker<float> MakeCopy()
        {
            return new ExponentialFloatSeeker(Gain, ArrivalDistance);
        }

        public override float Pump(float currentValue, float TimeSinceLastFrame)
        {
            return Mathf.Lerp(currentValue, End, Gain * TimeSinceLastFrame);
        }

        public override bool IsDone(float currentValue)
        {
            return Mathf.Abs(currentValue - End) <= ArrivalDistance;
        }
    }
}
