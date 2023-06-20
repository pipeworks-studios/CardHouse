using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse
{
    public class PhaseManager : MonoBehaviour
    {
        public List<Button> AllPhaseDependentButtons;
        public List<Phase> Phases;
        public Phase CurrentPhase => (CurrentPhaseIndex >= 0 && CurrentPhaseIndex < Phases.Count) ? Phases[CurrentPhaseIndex] : null;
        int CurrentPhaseIndex = 0;
        public int PlayerIndex
        {
            get { return CurrentPhase.PlayerIndex; }
        }

        public Action<Phase> OnPhaseChanged;
        public static PhaseManager Instance;

        void Awake()
        {
            Instance = this;
        }

        IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();
            if (CurrentPhase != null)
            {
                StartCoroutine(CurrentPhase.Start());
            }
        }

        public void HardReset()
        {
            StartCoroutine(HardResetRoutine());
        }

        IEnumerator HardResetRoutine()
        {
            CurrentPhaseIndex = 0;
            yield return new WaitForEndOfFrame();
            if (CurrentPhase != null)
            {
                StartCoroutine(CurrentPhase.Start());
                OnPhaseChanged?.Invoke(CurrentPhase);
            }
        }

        public void NextPhase()
        {
            foreach (var button in AllPhaseDependentButtons)
            {
                button.interactable = false;
            }
            StartCoroutine(PhaseTransition());
        }

        IEnumerator PhaseTransition()
        {
            yield return CurrentPhase.End();
            CurrentPhaseIndex = (CurrentPhaseIndex + 1) % Phases.Count;
            yield return CurrentPhase.Start();
            OnPhaseChanged?.Invoke(CurrentPhase);
        }

        public bool IsValidDragStart(CardGroup source, DragAction dragAction)
        {
            if (CurrentPhase == null)
                return true;

            return CurrentPhase.IsValidDragStart(source, dragAction);
        }

        public bool IsValidDrag(CardGroup source, CardGroup destination, DragAction dragAction)
        {
            if (CurrentPhase == null)
                return true;

            return CurrentPhase.IsValidDrag(source, destination, dragAction);
        }

        public bool IsValidClick(ClickDetector blutton)
        {
            if (CurrentPhase == null)
                return true;

            return CurrentPhase.ValidClickTargets.Contains(blutton);
        }

        public void SetCameraPosition(Transform cameraPosition)
        {
            if (cameraPosition != null)
            {
                var homing = Camera.main.GetComponent<Homing>();
                var turning = Camera.main.GetComponent<Turning>();

                if (homing != null && turning != null)
                {
                    homing.StartSeeking(cameraPosition.position + Vector3.forward * (-10 - cameraPosition.position.z));
                    turning.StartSeeking(cameraPosition.rotation.eulerAngles.z);
                }
            }
        }

        void Update()
        {
#if DEBUG
            if (CurrentPhase?.ValidDrags == null)
                return;

            foreach (var validDrag in CurrentPhase.ValidDrags)
            {
                var offset = Vector3.zero;
                var color = Color.green;
                switch (validDrag.DragAction)
                {
                    case DragAction.UseAndDiscard:
                        offset += Vector3.one * 0.1f;
                        color = Color.white;
                        break;
                    case DragAction.UseOnTargetAndDiscard:
                        offset += Vector3.one * 0.2f;
                        color = Color.cyan;
                        break;
                }
                Debug.DrawLine(
                    validDrag.Source.transform.position + offset,
                    validDrag.Destination.transform.position + offset,
                    color);
            }
#endif
        }
    }
}
