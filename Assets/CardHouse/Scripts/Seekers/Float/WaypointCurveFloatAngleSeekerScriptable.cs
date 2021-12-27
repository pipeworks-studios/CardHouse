using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardHouse/Seekers/Float/Waypoint Curve (angle)")]
public class WaypointCurveFloatAngleSeekerScriptable : SeekerScriptable<float>
{
    public float Duration = 2f;
    public AnimationCurve ProgressCurve;

    public override Seeker<float> GetStrategy(params object[] args)
    {
        var waypoints = new List<float>();
        foreach (var arg in args)
        {
            if (arg is float floatArg)
            {
                waypoints.Add(floatArg);
            }
            else if (arg is IEnumerable<float> floatEnumerable)
            {
                waypoints.AddRange(floatEnumerable);
            }
        }
        return new WaypointCurveFloatAngleSeeker(Duration, ProgressCurve, waypoints);
    }
}
