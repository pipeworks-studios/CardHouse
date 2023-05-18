using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandboxManager : MonoBehaviour
{
    public GameObject TutorialButtonPrefab;
    public Transform TutorialListRoot;
    public StringListScriptable Tutorials;
    int currentTutorial = -1;
    public Animator SidebarAnimator;
    public TMP_Text TitleText;

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
    }

    public void Reset()
    {
        SetupCurrentTutorial();
    }

    public void GoToNext()
    {
        if (currentTutorial < Tutorials.MyList.Count - 1) 
        {
            currentTutorial++;
            SetupCurrentTutorial();
        }
    }

    public void GoToPrevious()
    {
        if (currentTutorial > 0)
        {
            currentTutorial--;
            SetupCurrentTutorial();
        }
    }

    public void GoTo(string name)
    {
        currentTutorial = Tutorials.MyList.IndexOf(name);
        SetupCurrentTutorial();
    }

    void SetupCurrentTutorial()
    {
        TitleText.text = Tutorials.MyList[currentTutorial];
        SceneManager.LoadScene(Tutorials.MyList[currentTutorial]);
    }

    public void ToggleSidebar()
    {
        SidebarAnimator.SetBool("IsVisible", !SidebarAnimator.GetBool("IsVisible"));
    }
}
