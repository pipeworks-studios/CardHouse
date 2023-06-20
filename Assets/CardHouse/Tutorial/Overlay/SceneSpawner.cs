using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardHouse.Tutorial
{
    public class SceneSpawner : MonoBehaviour
    {
        public string SceneToSpawn;
        void Start()
        {
            SceneManager.LoadScene(SceneToSpawn, LoadSceneMode.Additive);
        }
    }
}
