using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoadAttribute]
public static class LaunchTutorialOption
{

    static LaunchTutorialOption()
    {
        EditorApplication.playModeStateChanged += ReloadScenes;
    }

    static void ReloadScenes(PlayModeStateChange change)
    {
        if (change != PlayModeStateChange.EnteredEditMode)
            return;

        var launchData = GetLaunchData();
        if (launchData == null)
            return;

        if (launchData.LaunchedTutorial) 
        {
            launchData.LaunchedTutorial = false;
            EditorUtility.SetDirty(launchData);
            AssetDatabase.SaveAssetIfDirty(launchData);

            bool first = true;
            foreach (var scenePath in launchData.OpenScenes)
            {
                if (first)
                {
                    EditorSceneManager.OpenScene(scenePath);
                    first = false;
                }
                else 
                {
                    EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
               }
            }
            EditorSceneManager.SetActiveScene(EditorSceneManager.GetSceneByPath(launchData.ActiveScene));
        }
    }

    [MenuItem("CardHouse/Launch Tutorial")]
    static void LaunchTutorial()
    {
        var launchData = GetLaunchData();
        if (launchData == null)
            return;

        launchData.LaunchedTutorial = true;
        launchData.OpenScenes = EditorSceneManager.GetAllScenes().Select(x => x.path).ToList();
        launchData.ActiveScene = EditorSceneManager.GetActiveScene().path;
        EditorUtility.SetDirty(launchData);
        AssetDatabase.SaveAssetIfDirty(launchData);

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Tutorial/Tutorial.unity");
        EditorApplication.isPlaying = true;
    }

    static LaunchDataScriptable GetLaunchData()
    {
        return AssetDatabase.LoadAssetAtPath<LaunchDataScriptable>("Assets/Tutorial/LaunchData.asset");
    }
}
#endif