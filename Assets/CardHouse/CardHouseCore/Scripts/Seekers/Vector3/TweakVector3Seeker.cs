using UnityEngine;

namespace CardHouse
{
    public class TweakVector3Seeker : AnimCurveVector3Seeker
    {
        public Vector3 Tweak;
        public AnimationCurve TweakMultiplier;

        public TweakVector3Seeker(float duration, AnimationCurve progressCurve, Vector3 tweak, AnimationCurve tweakMultiplier) : base(duration, progressCurve)
        {
            Tweak = tweak;
            TweakMultiplier = tweakMultiplier;
        }

        public override Seeker<Vector3> MakeCopy()
        {
            return new TweakVector3Seeker(Duration, ProgressCurve, Tweak, TweakMultiplier);
        }

        public override Vector3 Pump(Vector3 currentValue, float TimeSinceLastFrame)
        {
            var step = base.Pump(currentValue, TimeSinceLastFrame);
            var normalizedTime = Timer / Duration;
            return step + TweakMultiplier.Evaluate(normalizedTime) * Tweak;
        }
    }
}
