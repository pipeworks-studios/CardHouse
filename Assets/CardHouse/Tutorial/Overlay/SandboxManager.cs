using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardHouse.Tutorial
{
    public class SandboxManager : MonoBehaviour
    {
        public GameObject TutorialButtonPrefab;
        public Transform TutorialListRoot;
        public StringListScriptable Tutorials;
        int currentTutorial = -1;
        public Animator SidebarAnimator;
        public TMP_Text TitleText;
        public GameObject NextButton;
        public GameObject PreviousButton;
        public GameObject ResetButton;

        public static MultiBoardTutorial MultiBoardTutorial;

        public void Start()
        {
            for (var i = 0; i < TutorialListRoot.childCount; i++)
            {
                Destroy(TutorialListRoot.GetChild(i).gameObject);
            }
            for (var i = Tutorials.MyList.Count - 1; i >= 0; i--)
            {
                var newButton = Instantiate(TutorialButtonPrefab);
                newButton.GetComponent<TutorialButton>().Setup(Tutorials.MyList[i], this);
                newButton.transform.SetParent(TutorialListRoot.transform, false);
            }

            TitleText.text = "";
            PreviousButton.SetActive(false);
            ResetButton.SetActive(false);
        }

        public void Reset()
        {
            SetupCurrentTutorial();
        }

        public void GoToNext()
        {
            if (MultiBoardTutorial != null && MultiBoardTutorial.InstructionIndex < MultiBoardTutorial.Instructions.Count - 1)
            {
                MultiBoardTutorial.InstructionsForward();
            }
            else
            {
                if (currentTutorial < Tutorials.MyList.Count - 1)
                {
                    currentTutorial++;
                    SetupCurrentTutorial();
                }
            }
        }

        public void GoToPrevious()
        {
            if (MultiBoardTutorial != null && MultiBoardTutorial.InstructionIndex > 0)
            {
                MultiBoardTutorial.InstructionsBackward();
            }
            else
            {
                if (currentTutorial > 0)
                {
                    currentTutorial--;
                    SetupCurrentTutorial();
                }
            }
        }

        public void GoTo(string name)
        {
            currentTutorial = Tutorials.MyList.IndexOf(name);
            SetupCurrentTutorial();
        }

        void SetupCurrentTutorial()
        {
            PreviousButton.SetActive(currentTutorial > 0);
            NextButton.SetActive(currentTutorial < Tutorials.MyList.Count - 1);
            ResetButton.SetActive(currentTutorial >= 0);
            TitleText.text = Tutorials.MyList[currentTutorial];
            SceneManager.LoadScene(Tutorials.MyList[currentTutorial]);
        }

        public void ToggleSidebar()
        {
            SidebarAnimator.SetBool("IsVisible", !SidebarAnimator.GetBool("IsVisible"));
        }
    }
}