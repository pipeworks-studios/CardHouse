using UnityEngine;

namespace CardHouse
{
    public class ExponentialAngleFloatSeeker : ExponentialFloatSeeker
    {
        public ExponentialAngleFloatSeeker(float gain = 8f, float arrivalDist = 0.01f) : base(gain, arrivalDist)
        {
        }

        public override float Pump(float currentValue, float TimeSinceLastFrame)
        {
            return Mathf.LerpAngle(currentValue, End, Gain * TimeSinceLastFrame);
        }
    }
}
