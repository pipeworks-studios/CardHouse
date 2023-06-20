using UnityEngine;

namespace CardHouse
{
    public class AnimCurveVector3Seeker : Seeker<Vector3>
    {
        public float Duration;
        protected float Timer;
        public AnimationCurve ProgressCurve;

        public AnimCurveVector3Seeker(float duration, AnimationCurve progressCurve)
        {
            Duration = duration;
            ProgressCurve = progressCurve;
            Timer = 0f;
        }

        public override Seeker<Vector3> MakeCopy()
        {
            return new AnimCurveVector3Seeker(Duration, ProgressCurve);
        }

        public override Vector3 Pump(Vector3 currentValue, float TimeSinceLastFrame)
        {
            Timer += TimeSinceLastFrame;
            var normalizedTime = Timer / Duration;
            return Start + (End - Start) * ProgressCurve.Evaluate(normalizedTime);
        }

        public override bool IsDone(Vector3 currentValue)
        {
            return Timer >= Duration;
        }
    }
}
