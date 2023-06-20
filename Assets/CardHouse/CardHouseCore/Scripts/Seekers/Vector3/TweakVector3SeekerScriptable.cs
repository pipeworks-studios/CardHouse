using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Seekers/Vector3/Tweak")]
    public class TweakVector3SeekerScriptable : AnimCurveVector3SeekerScriptable
    {
        public Vector3 Tweak;
        public AnimationCurve TweakMultiplier;

        public override Seeker<Vector3> GetStrategy(params object[] args)
        {
            return new TweakVector3Seeker(Duration, ProgressCurve, Tweak, TweakMultiplier);
        }
    }
}