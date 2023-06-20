using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardHouse
{
    public class WaypointCurveVector3Seeker : AnimCurveVector3Seeker
    {
        List<Vector3> Waypoints;

        public WaypointCurveVector3Seeker(float duration, AnimationCurve progressCurve, List<Vector3> waypoints) : base(duration, progressCurve)
        {
            Waypoints = waypoints.ToList();
        }

        public override Seeker<Vector3> MakeCopy()
        {
            return new WaypointCurveVector3Seeker(Duration, ProgressCurve, Waypoints);
        }

        public override Vector3 Pump(Vector3 currentValue, float TimeSinceLastFrame)
        {
            Timer += TimeSinceLastFrame;
            var normalizedTime = Timer / Duration;
            var progress = Mathf.Min(ProgressCurve.Evaluate(normalizedTime), 0.9999f);
            var destIndex = Mathf.FloorToInt(progress * (Waypoints.Count + 1));
            var dest = destIndex >= Waypoints.Count ? End : Waypoints[destIndex];
            var lastWaypoint = destIndex == 0 ? Start : Waypoints[destIndex - 1];
            return lastWaypoint + (dest - lastWaypoint) * (progress % (1f / (Waypoints.Count + 1))) * (Waypoints.Count + 1);
        }

        public override bool IsDone(Vector3 currentValue)
        {
            return Timer >= Duration;
        }
    }
}
