using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Seekers/Vector3/Animated Curve")]
    public class AnimCurveVector3SeekerScriptable : SeekerScriptable<Vector3>
    {
        public float Duration = 2f;
        public AnimationCurve ProgressCurve;

        public override Seeker<Vector3> GetStrategy(params object[] args)
        {
            return new AnimCurveVector3Seeker(Duration, ProgressCurve);
        }
    }
}
