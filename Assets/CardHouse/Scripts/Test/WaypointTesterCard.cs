using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTesterCard : MonoBehaviour
{
    public SeekerScriptableSet WaypointSeekers;

    public void Test()
    {
        var card = GetComponent<Card>();
        var tester = card?.Group.GetComponent<WaypointTesterGroup>();
        if (tester != null)
        {
            tester.Test(card, WaypointSeekers.Homing, WaypointSeekers.Turning, WaypointSeekers.Scaling);
        }
    }
}
