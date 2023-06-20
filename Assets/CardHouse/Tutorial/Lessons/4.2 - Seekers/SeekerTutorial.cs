using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CardHouse.Tutorial
{
    public class SeekerTutorial : MonoBehaviour
    {
        public TMP_Dropdown HomingDropdown;
        public TMP_Dropdown TurningDropdown;
        public TMP_Dropdown ScalingDropdown;

        public List<CardGroup> Stacks;

        public List<StringSeekerKVP> SeekerKVPs;

        public Transform Waypoint;

        public void Transfer(int i)
        {
            var card = Stacks[0].Get();
            if (card == null)
                return;

            Seeker<Vector3> homing = null;
            var homingKey = HomingDropdown.options[HomingDropdown.value].text;
            foreach (var kvp in SeekerKVPs)
            {
                if (kvp.Key == homingKey)
                {
                    homing = ((SeekerScriptable<Vector3>)kvp.Value).GetStrategy(kvp.Key.Contains("Vector3") ? Waypoint.position : null);
                    break;
                }
            }
            Seeker<float> turning = null;
            var turningKey = TurningDropdown.options[TurningDropdown.value].text;
            foreach (var kvp in SeekerKVPs)
            {
                if (kvp.Key == turningKey)
                {
                    turning = ((SeekerScriptable<float>)kvp.Value).GetStrategy(kvp.Key.Contains("Angle") ? Waypoint.eulerAngles.z : null);
                    break;
                }
            }
            Seeker<float> scaling = null;
            var scalingKey = ScalingDropdown.options[ScalingDropdown.value].text;
            foreach (var kvp in SeekerKVPs)
            {
                if (kvp.Key == scalingKey)
                {
                    scaling = ((SeekerScriptable<float>)kvp.Value).GetStrategy(kvp.Key.Contains("Float") ? Waypoint.lossyScale.y : null);
                    break;
                }
            }

            Stacks[i].Mount(card, seekerSets: new SeekerSetList { new SeekerSet { Card = card, Homing = homing, Scaling = scaling, Turning = turning } });
        }
    }

    [Serializable]
    public class StringSeekerKVP
    {
        public string Key;
        public ScriptableObject Value;
    }
}