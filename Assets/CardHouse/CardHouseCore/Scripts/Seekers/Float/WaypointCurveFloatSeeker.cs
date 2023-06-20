using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardHouse
{
    public class WaypointCurveFloatSeeker : AnimCurveFloatSeeker
    {
        List<float> Waypoints;

        public WaypointCurveFloatSeeker(float duration, AnimationCurve progressCurve, List<float> waypoints) : base(duration, progressCurve)
        {
            Waypoints = waypoints.ToList();
        }

        public override Seeker<float> MakeCopy()
        {
            return new WaypointCurveFloatSeeker(Duration, ProgressCurve, Waypoints);
        }

        public override float Pump(float currentValue, float TimeSinceLastFrame)
        {
            Timer += TimeSinceLastFrame;
            var normalizedTime = Timer / Duration;
            var progress = Mathf.Min(ProgressCurve.Evaluate(normalizedTime), 0.9999f);
            var destIndex = Mathf.FloorToInt(progress * (Waypoints.Count + 1));
            var dest = destIndex >= Waypoints.Count ? End : Waypoints[destIndex];
            var lastWaypoint = destIndex == 0 ? Start : Waypoints[destIndex - 1];
            return lastWaypoint + (dest - lastWaypoint) * (progress % (1f / (Waypoints.Count + 1))) * (Waypoints.Count + 1);
        }

        public override bool IsDone(float currentValue)
        {
            return Timer >= Duration;
        }
    }
}
