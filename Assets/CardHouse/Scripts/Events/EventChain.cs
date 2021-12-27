using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChain : MonoBehaviour
{
    public List<TimedEvent> Events = new List<TimedEvent>();

    public void Activate()
    {
        StartCoroutine(TimedEvent.ExecuteChain(Events));
    }
}
