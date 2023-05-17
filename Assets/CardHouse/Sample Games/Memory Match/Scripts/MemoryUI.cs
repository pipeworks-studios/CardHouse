using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryUI : MonoBehaviour
{
    public Text TimerText;
    public Text MatchText;

    public void UpdateMatches(int matches)
    {
        MatchText.text = string.Format("Matches: {0}", matches);
    }

    public void UpdateTimer(float timer)
    {
        TimerText.text = string.Format("Time: {0:F0}", timer);
    }
}
