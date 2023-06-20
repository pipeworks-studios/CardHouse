using TMPro;
using UnityEngine;

namespace CardHouse.Tutorial
{
    public class PhaseLabelUpdater : MonoBehaviour
    {
        public TMP_Text PhaseText;

        public void UpdatePhaseLabel()
        {
            PhaseText.text = PhaseManager.Instance.CurrentPhase.Name;
        }
    }
}
