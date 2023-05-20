using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PhaseLabelUpdater : MonoBehaviour
{
    public TMP_Text PhaseText;

    public void UpdatePhaseLabel()
    {
        PhaseText.text = PhaseManager.Instance.CurrentPhase.Name;
    }
}
