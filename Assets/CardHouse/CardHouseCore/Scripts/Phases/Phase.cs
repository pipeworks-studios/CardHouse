using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse
{
    [Serializable]
    public class Phase
    {
        public string Name;
        public int PlayerIndex;
        public Transform CameraPosition;
        public Transform CardPresentationPosition;
        public List<Button> ActiveButtons;
        public List<ClickDetector> ValidClickTargets;
        public List<DragTransition> ValidDrags;
        public List<TimedEvent> OnPhaseStartEventChain;
        public List<TimedEvent> OnPhaseEndEventChain;

        public IEnumerator Start()
        {
            PhaseManager.Instance?.SetCameraPosition(CameraPosition);
            yield return TimedEvent.ExecuteChain(OnPhaseStartEventChain);
            foreach (var button in ActiveButtons)
            {
                button.interactable = true;
            }
        }

        public IEnumerator End()
        {
            yield return TimedEvent.ExecuteChain(OnPhaseEndEventChain);
        }

        public bool IsValidDragStart(CardGroup source, DragAction dragAction)
        {
            return ValidDrags.Any(x => x.Source == source && x.DragAction == dragAction);
        }

        public bool IsValidDrag(CardGroup source, CardGroup destination, DragAction dragAction)
        {
            return ValidDrags.Any(x => x.Source == source && x.Destination == destination && x.DragAction == dragAction);
        }
    }
}
