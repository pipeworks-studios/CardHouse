using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CardHouse.TestScenes
{
    [RequireComponent(typeof(CardGroup))]
    public class WaypointTesterGroup : MonoBehaviour
    {
        public List<Transform> Waypoints;

        public void Test(Card card, SeekerScriptable<Vector3> homing, SeekerScriptable<float> turning, SeekerScriptable<float> scaling)
        {
            GetComponent<CardGroup>().Mount(card, seekerSets:
                new SeekerSetList
                {
                    new SeekerSet
                    {
                        Card = card,
                        Homing = homing.GetStrategy(Waypoints.Select(x => x.position).ToList()),
                        Turning = turning.GetStrategy(Waypoints.Select(x => x.rotation.eulerAngles.z).ToList()),
                        Scaling = scaling.GetStrategy(Waypoints.Select(x => x.lossyScale.x).ToList())
                    }
                });
        }
    }
}
