using UnityEngine;

namespace CardHouse
{
    public class ExponentialVector3Seeker : Seeker<Vector3>
    {
        float XYGain = 8f;
        float ZGain = 3f; // Want to home Z faster than X and Y so that cards don't slide through each other as much
        float ArrivalDistance;

        public ExponentialVector3Seeker(float xyGain = 8f, float zGain = 10f, float arrivalDist = 0.01f)
        {
            XYGain = xyGain;
            ZGain = zGain;
            ArrivalDistance = arrivalDist;
        }

        public override Seeker<Vector3> MakeCopy()
        {
            return new ExponentialVector3Seeker(XYGain, ZGain, ArrivalDistance);
        }

        public override Vector3 Pump(Vector3 currentValue, float TimeSinceLastFrame)
        {
            return currentValue + (Vector3.right * (End.x - currentValue.x) + Vector3.up * (End.y - currentValue.y)) * XYGain * TimeSinceLastFrame + Vector3.forward * (End.z - currentValue.z) * ZGain * TimeSinceLastFrame;
        }

        public override bool IsDone(Vector3 currentValue)
        {
            return (currentValue - End).magnitude <= ArrivalDistance;
        }
    }
}
