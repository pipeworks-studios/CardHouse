using UnityEngine;

namespace CardHouse.Tutorial
{
    public class SceneKeeper : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(this);
        }
    }
}
