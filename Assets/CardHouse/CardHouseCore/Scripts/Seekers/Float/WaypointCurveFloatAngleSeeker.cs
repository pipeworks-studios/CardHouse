using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardHouse
{
    public class WaypointCurveFloatAngleSeeker : AnimCurveFloatSeeker
    {
        List<float> Waypoints;

        public WaypointCurveFloatAngleSeeker(float duration, AnimationCurve progressCurve, List<float> waypoints) : base(duration, progressCurve)
        {
            Start = CardHouse.Utils.CorrectAngle(Start);
            End = CardHouse.Utils.CorrectAngle(End);
            Waypoints = waypoints.ToList();
            for (var i = 0; i < waypoints.Count; i++)
            {
                waypoints[i] = CardHouse.Utils.CorrectAngle(waypoints[i]);
            }
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
            return Mathf.LerpAngle(lastWaypoint, dest, (progress % (1f / (Waypoints.Count + 1))) * (Waypoints.Count + 1));
        }

        public override bool IsDone(float currentValue)
        {
            return Timer >= Duration;
        }
    }
}
