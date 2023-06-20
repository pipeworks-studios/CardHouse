using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Seekers/Vector3/Randomized Curve")]
    public class RandomizedCurveVector3SeekerScriptable : TweakVector3SeekerScriptable
    {
        public float TweakMagnitudeMin = 1.5f;
        public float TweakMagnitudeMax = 2f;

        public override Seeker<Vector3> GetStrategy(params object[] args)
        {
            var myAngle = Random.Range(0f, 360f);
            var tweak = Vector3.right * Mathf.Cos(myAngle) + Vector3.up * Mathf.Sin(myAngle);
            return new TweakVector3Seeker(Duration, ProgressCurve, Random.Range(TweakMagnitudeMin, TweakMagnitudeMax) * tweak, TweakMultiplier);
        }
    }
}
