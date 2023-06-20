using UnityEngine;

namespace CardHouse
{
    public class ContinuousInstantVector3Seeker : Seeker<Vector3>
    {
        public override Seeker<Vector3> MakeCopy()
        {
            return new ContinuousInstantVector3Seeker();
        }

        public override Vector3 Pump(Vector3 currentValue, float TimeSinceLastFrame)
        {
            return End;
        }

        public override bool IsDone(Vector3 currentValue)
        {
            return false;
        }
    }
}
