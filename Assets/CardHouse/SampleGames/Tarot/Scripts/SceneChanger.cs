using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string SceneToSpawn;
    public void Activate()
    {
        SceneManager.LoadScene(SceneToSpawn);
    }
}