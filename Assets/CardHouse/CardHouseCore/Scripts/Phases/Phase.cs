using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Serialization;

[Serializable]
public class Phase
{
    public int PlayerIndex;
    public string Name;
    public Transform CameraPosition;
    public Transform CardPresentationPosition;
    public List<Button> ActiveButtons;
    public List<ClickDetector> ValidClickTargets;
    public List<DragTransition> ValidDrags;
    public List<TimedEvent> OnPhaseStartEventChain;
    public List<TimedEvent> OnPhaseEndEventChain;

    public IEnumerator Start()
    {
        if (CameraPosition != null)
        {
            var homing = Camera.main.GetComponent<Homing>();
            var turning = Camera.main.GetComponent<Turning>();

            if (homing != null && turning != null)
            {
                homing.StartSeeking(CameraPosition.position);
                turning.StartSeeking(CameraPosition.rotation.eulerAngles.z);
            }
        }
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
