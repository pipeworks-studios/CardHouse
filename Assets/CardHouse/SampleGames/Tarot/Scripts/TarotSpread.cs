using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardHouse.SampleGames.Tarot
{
    [Serializable]
    public class TarotSpread
    {
        public string Name;
        [TextArea(1, 15)]
        public string Instructions;
        public List<CardGroup> Slots;

        public void FillNext(Card card)
        {
            foreach (var slot in Slots)
            {
                if (slot.HasRoom())
                {
                    var myAngle = UnityEngine.Random.Range(0f, 360f);
                    var tweak = Vector3.right * Mathf.Cos(myAngle) + Vector3.up * Mathf.Sin(myAngle) + Vector3.back;
                    var tweakCurve = AnimationCurve.EaseInOut(0, 0, 1, 0);
                    tweakCurve.AddKey(0.5f, 1f);

                    var cardSeeker = new TweakVector3Seeker(1f, AnimationCurve.EaseInOut(0, 0, 1, 1), UnityEngine.Random.Range(1f, 1.5f) * tweak, tweakCurve);

                    slot.Mount(card, seekerSets: new SeekerSetList { new SeekerSet { Card = card, Homing = cardSeeker } });
                    break;
                }
            }
        }
    }
}
