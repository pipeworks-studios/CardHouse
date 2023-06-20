using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Seekers/Vector3/Continuous")]
    public class ContinuousInstantVector3SeekerScriptable : SeekerScriptable<Vector3>
    {
        public override Seeker<Vector3> GetStrategy(params object[] args)
        {
            return new ContinuousInstantVector3Seeker();
        }
    }
}