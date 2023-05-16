using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpawner : MonoBehaviour
{
    public string SceneToSpawn;
    void Start()
    {
        SceneManager.LoadScene(SceneToSpawn, LoadSceneMode.Additive);
    }
}
