using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Seekers/Float/Animated Curve")]
    public class AnimCurveFloatSeekerScriptable : SeekerScriptable<float>
    {
        public float Duration = 2f;
        public AnimationCurve ProgressCurve;

        public override Seeker<float> GetStrategy(params object[] args)
        {
            return new AnimCurveFloatSeeker(Duration, ProgressCurve);
        }
    }
}
