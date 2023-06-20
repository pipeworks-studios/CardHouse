using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CardHouse
{
    [System.Serializable]
    public class TimedEvent
    {
        public float Duration;
        public UnityEvent Event;

        public IEnumerator ActivateAndDelay()
        {
            Event.Invoke();
            yield return new WaitForSeconds(Duration);
        }

        public static IEnumerator ExecuteChain(List<TimedEvent> events, Action callback = null)
        {
            if (events != null)
            {
                foreach (var e in events)
                {
                    yield return e.ActivateAndDelay();
                }
            }

            callback?.Invoke();
        }
    }
}
