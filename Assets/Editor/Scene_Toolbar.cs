using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

/// <summary>
/// Utilize toolbar extender package to load scenes easily and auto save scene when press Play.
/// </summary>
[InitializeOnLoad]
public static class SceneToolbar
{
    static SceneToolbar()
    {
        ToolbarExtender.LeftToolbarGUI.Add(DrawToolbar);
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    static void DrawToolbar()
    {
        if (GUILayout.Button("Test", GUILayout.Width(80)))
        {
            SaveScene();
            EditorSceneManager.OpenScene("Assets/Scenes/VR Test.unity");
        }

        if (GUILayout.Button("VR Demo", GUILayout.Width(80)))
        {
            SaveScene();
            EditorSceneManager.OpenScene("Assets/Samples/XR Interaction Toolkit/3.1.3/Starter Assets/DemoScene.unity");
        }
    }
    static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            SaveScene();
            //EditorSceneManager.OpenScene("Assets/Scenes/Lobby.unity");
        }
    }

    static void SaveScene()
    {
#if UNITY_EDITOR
        if (!EditorApplication.isPlaying)
        {
            EditorSceneManager.SaveOpenScenes();
        }
#endif
    }
}
