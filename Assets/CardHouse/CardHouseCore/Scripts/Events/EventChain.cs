using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CardHouse
{
    public class EventChain : MonoBehaviour
    {
        public List<TimedEvent> Events = new List<TimedEvent>();

        public UnityEvent OnChainFinished;

        public void Activate()
        {
            StartCoroutine(TimedEvent.ExecuteChain(Events, () => OnChainFinished?.Invoke()));
        }
    }
}
