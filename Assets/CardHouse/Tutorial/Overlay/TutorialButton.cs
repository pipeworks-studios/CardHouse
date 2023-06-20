using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CardHouse.Tutorial
{
    public class TutorialButton : MonoBehaviour
    {
        public TMP_Text Label;

        public void Setup(string text, SandboxManager manager)
        {
            Label.text = text;
            GetComponent<Button>().onClick.AddListener(() => manager.GoTo(text));
        }
    }
}
