using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using System.Reflection;
using System;
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

        var requiredScenes = new List<string> { "Assets/CardHouse/Tutorial/Overlay/TutorialOverlay.unity" };
        var tutorials = AssetDatabase.LoadAssetAtPath<StringListScriptable>("Assets/CardHouse/Tutorial/TutorialSceneList.asset");
        foreach (var tutorialScene in tutorials.MyList)
        {
            var sceneSubfolder = tutorialScene.Replace("(", "").Replace(")", " -");
            requiredScenes.Add($"Assets/CardHouse/Tutorial/Sandboxes/{sceneSubfolder}/{tutorialScene}.unity");
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

    [MenuItem("CardHouse/Remove Tutorial Scenes from Build Settings")]
    static void RemoveTutorialScenesFromBuildSettings()
    {
        var scenes = new List<string> { "Assets/CardHouse/Tutorial/Overlay/TutorialOverlay.unity" };
        var tutorials = AssetDatabase.LoadAssetAtPath<StringListScriptable>("Assets/CardHouse/Tutorial/TutorialSceneList.asset");
        foreach (var tutorialScene in tutorials.MyList)
        {
            var sceneSubfolder = tutorialScene.Replace("(", "").Replace(")", " -");
            scenes.Add($"Assets/CardHouse/Tutorial/Sandboxes/{sceneSubfolder}/{tutorialScene}.unity");
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
}
#endif