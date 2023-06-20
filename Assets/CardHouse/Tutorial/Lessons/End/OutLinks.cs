using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CardHouse.Tutorial
{
    public class OutLinks : MonoBehaviour, IPointerClickHandler
    {
        public TMP_Text Text;

        public void OnPointerClick(PointerEventData eventData)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(Text, Input.mousePosition, null);
            var linkId = Text.textInfo.linkInfo[linkIndex].GetLinkID();

            var url = linkId switch
            {
                "GitHubIssues" => "https://github.com/pipeworks-studios/CardHouse/issues",
                "Pipeworks" => "https://www.pipeworks.com/",
                _ => ""
            };

            if (url != "")
            {
                Application.OpenURL(url);
            }
        }
    }
}
