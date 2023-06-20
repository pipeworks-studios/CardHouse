using UnityEngine;

namespace CardHouse
{
    public class AnimCurveFloatSeeker : Seeker<float>
    {
        public float Duration;
        protected float Timer;
        public AnimationCurve ProgressCurve;

        public AnimCurveFloatSeeker(float duration, AnimationCurve progressCurve)
        {
            Duration = duration;
            ProgressCurve = progressCurve;
            Timer = 0f;
        }

        public override Seeker<float> MakeCopy()
        {
            return new AnimCurveFloatSeeker(Duration, ProgressCurve);
        }

        public override float Pump(float currentValue, float TimeSinceLastFrame)
        {
            Timer += TimeSinceLastFrame;
            var normalizedTime = Timer / Duration;
            return Start + (End - Start) * ProgressCurve.Evaluate(normalizedTime);
        }

        public override bool IsDone(float currentValue)
        {
            return Timer >= Duration;
        }


    }
}
