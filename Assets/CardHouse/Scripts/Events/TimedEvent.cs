using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class TimedEvent
{
    public float Duration;
    public UnityEvent Event;

    public IEnumerator ActivateAfterDelay()
    {
        Event.Invoke();
        yield return new WaitForSeconds(Duration);
    }

    public static IEnumerator ExecuteChain(List<TimedEvent> events)
    {
        foreach (var e in events)
        {
            yield return e.ActivateAfterDelay();
        }
    }
}
