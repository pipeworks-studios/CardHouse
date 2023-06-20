using System.Collections.Generic;
using UnityEngine;

namespace CardHouse
{
    [CreateAssetMenu(menuName = "CardHouse/Seekers/Vector3/Waypoint Curve")]
    public class WaypointCurveVector3SeekerScriptable : SeekerScriptable<Vector3>
    {
        public float Duration = 2f;
        public AnimationCurve ProgressCurve;

        public override Seeker<Vector3> GetStrategy(params object[] args)
        {
            var waypoints = new List<Vector3>();
            foreach (var arg in args)
            {
                if (arg is Vector3 v3)
                {
                    waypoints.Add(v3);
                }
                else if (arg is IEnumerable<Vector3> v3Enumerable)
                {
                    waypoints.AddRange(v3Enumerable);
                }
            }
            return new WaypointCurveVector3Seeker(Duration, ProgressCurve, waypoints);
        }
    }
}
