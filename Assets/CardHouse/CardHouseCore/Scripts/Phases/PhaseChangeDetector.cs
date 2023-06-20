using UnityEngine;
using UnityEngine.Events;

namespace CardHouse
{
    public class PhaseChangeDetector : MonoBehaviour
    {
        public UnityEvent OnPhaseChange;

        PhaseManager MyPhaseManager;

        void Start()
        {
            MyPhaseManager = PhaseManager.Instance;
            if (MyPhaseManager != null)
            {
                MyPhaseManager.OnPhaseChanged += HandlePhaseChanged;
            }
        }

        void OnDestroy()
        {
            if (MyPhaseManager != null)
            {
                MyPhaseManager.OnPhaseChanged -= HandlePhaseChanged;
            }
        }

        void HandlePhaseChanged(Phase phase)
        {
            OnPhaseChange.Invoke();
        }
    }
}
