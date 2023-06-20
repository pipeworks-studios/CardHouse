using System.Collections.Generic;
using UnityEngine;

namespace CardHouse.Tutorial
{
    public class LaunchDataScriptable : ScriptableObject
    {
        public bool LaunchedTutorial;
        public string ActiveScene;
        public List<string> OpenScenes;
    }
}
