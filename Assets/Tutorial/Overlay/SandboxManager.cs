using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SandboxManager : MonoBehaviour
{
    public GameObject TutorialButtonPrefab;
    public Transform TutorialListRoot;
    public List<string> Tutorials;
    int currentTutorial = -1;
    public Animator SidebarAnimator;
    public TMP_Text TitleText;

    public void Start()
    {
        for (var i = 0; i < TutorialListRoot.childCount; i++) 
        { 
            Destroy(TutorialListRoot.GetChild(i).gameObject);
        }
        for (var i = Tutorials.Count - 1; i >= 0; i--)
        {
            var newButton = Instantiate(TutorialButtonPrefab);
            newButton.GetComponent<TutorialButton>().Setup(Tutorials[i], this);
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
        if (currentTutorial < Tutorials.Count - 1) 
        {
            currentTutorial++;
            SetupCurrentTutorial();
        }
    }

    public void GoTo(string name)
    {
        currentTutorial = Tutorials.IndexOf(name);
        SetupCurrentTutorial();
    }

    void SetupCurrentTutorial()
    {
        TitleText.text = Tutorials[currentTutorial];
        SceneManager.LoadScene(Tutorials[currentTutorial]);
    }

    public void ToggleSidebar()
    {
        SidebarAnimator.SetBool("IsVisible", !SidebarAnimator.GetBool("IsVisible"));
    }
}
