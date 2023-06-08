#if UNITY_EDITOR
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
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
            RemoveTutorialScenesFromBuildSettings();
        }
    }

    [MenuItem("CardHouse/Launch Tutorial")]
    static void LaunchTutorial()
    {
        var launchData = GetLaunchData();
        if (launchData == null)
            return;

        launchData.LaunchedTutorial = true;
        launchData.OpenScenes = new List<string>();
        for (var i = 0; i < EditorSceneManager.sceneCount; i++)
        {
            launchData.OpenScenes.Add(EditorSceneManager.GetSceneAt(i).path);
        }
        launchData.ActiveScene = EditorSceneManager.GetActiveScene().path;
        EditorUtility.SetDirty(launchData);
        AssetDatabase.SaveAssetIfDirty(launchData);

        var requiredScenes = new List<string> { "Assets/CardHouse/Tutorial/Overlay/TutorialOverlay.unity" };
        var tutorials = AssetDatabase.LoadAssetAtPath<StringListScriptable>("Assets/CardHouse/Tutorial/TutorialSceneList.asset");
        foreach (var tutorialScene in tutorials.MyList)
        {
            var sceneSubfolder = tutorialScene.Replace("(", "").Replace(")", " -");
            requiredScenes.Add($"Assets/CardHouse/Tutorial/Lessons/{sceneSubfolder}/{tutorialScene}.unity");
        }

        var buildScenes = EditorBuildSettings.scenes.ToList();
        foreach (var requiredScene in requiredScenes)
        {
            if (!buildScenes.Any(x => x.path == requiredScene))
            {
                buildScenes.Add(new EditorBuildSettingsScene(requiredScene, true));
            }
        }

        EditorBuildSettings.scenes = buildScenes.ToArray();

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/CardHouse/Tutorial/Tutorial.unity");

        var gameViewWindowType = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        var selectedSizeIndexProperty = gameViewWindowType.GetProperty("selectedSizeIndex", 
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        var gameViewWindow = EditorWindow.GetWindow(gameViewWindowType);
        selectedSizeIndexProperty.SetValue(gameViewWindow, 1, null);

        EditorApplication.isPlaying = true;
    }

    static void RemoveTutorialScenesFromBuildSettings()
    {
        var scenes = new List<string> { "Assets/CardHouse/Tutorial/Overlay/TutorialOverlay.unity" };
        var tutorials = AssetDatabase.LoadAssetAtPath<StringListScriptable>("Assets/CardHouse/Tutorial/TutorialSceneList.asset");
        foreach (var tutorialScene in tutorials.MyList)
        {
            var sceneSubfolder = tutorialScene.Replace("(", "").Replace(")", " -");
            scenes.Add($"Assets/CardHouse/Tutorial/Lessons/{sceneSubfolder}/{tutorialScene}.unity");
        }

        var buildScenes = EditorBuildSettings.scenes.ToList();
        foreach (var requiredScene in scenes)
        {
            var result = buildScenes.FirstOrDefault(x => x.path == requiredScene);
            if (result != null)
            {
                buildScenes.Remove(result);
            }
        }

        EditorBuildSettings.scenes = buildScenes.ToArray();
    }

    static LaunchDataScriptable GetLaunchData()
    {
        return AssetDatabase.LoadAssetAtPath<LaunchDataScriptable>("Assets/CardHouse/Tutorial/LaunchData.asset");
    }

    [MenuItem("CardHouse/Report a Bug")]
    static void OpenIssuesPage()
    {
        Application.OpenURL("https://github.com/pipeworks-studios/CardHouse/issues");
    }
}
#endif