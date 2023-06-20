using System.Collections.Generic;
using UnityEngine;

namespace CardHouse.Tutorial
{
    public class PresentationPointTutorial : MonoBehaviour
    {
        public int PlayerIndex;

        public Turning ParentTurning;
        public Scaling ParentScaling;
        public Turning ButtonParentTurning;
        public Scaling ButtonParentScaling;

        public float RotationMin;
        public float RotationMax;
        public int RotationSteps;

        public float ScaleMin;
        public float ScaleMax;
        public int ScaleSteps;

        List<float> Rotations = new List<float>();
        int RotationI;
        List<float> Scales = new List<float>();
        int ScaleI;

        private void Start()
        {
            for (var r = RotationMin; r <= RotationMax; r += (RotationMax - RotationMin) / RotationSteps)
            {
                Rotations.Add(r);
            }
            RotationI = Mathf.CeilToInt(RotationSteps / 2f);

            for (var s = ScaleMin; s <= ScaleMax; s += (ScaleMax - ScaleMin) / ScaleSteps)
            {
                Scales.Add(s);
            }
            ScaleI = Scales.IndexOf(1f);
        }

        public void Rotate(int shift)
        {
            RotationI = Mathf.Clamp(RotationI + shift, 0, Rotations.Count - 1);
            ParentTurning.StartSeeking(Rotations[RotationI], useLocalSpace: true);
            ButtonParentTurning.StartSeeking(-Rotations[RotationI], useLocalSpace: true);
        }

        public void Scale(int shift)
        {
            ScaleI = Mathf.Clamp(ScaleI + shift, 0, Scales.Count - 1);
            ParentScaling.StartSeeking(Scales[ScaleI]);
            ButtonParentScaling.StartSeeking(1f / Scales[ScaleI], useLocalSpace: true);
        }

        public void UpdateCameraPosition()
        {
            if (PlayerIndex != PhaseManager.Instance?.CurrentPhase.PlayerIndex)
                return;

            PhaseManager.Instance?.SetCameraPosition(transform);
        }
    }
}
