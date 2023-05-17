using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    public TMP_Text Label;

    public void Setup(string text, SandboxManager manager)
    {
        Label.text = text;
        GetComponent<Button>().onClick.AddListener(() => manager.GoTo(text));
    }
}
