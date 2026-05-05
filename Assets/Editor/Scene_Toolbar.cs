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
        /*
        if (GUILayout.Button("Test_Game", GUILayout.Width(80)))
        {
            SaveScene();
            EditorSceneManager.OpenScene("Assets/Scenes/Single_Player.unity");
        }

        if (GUILayout.Button("Roles", GUILayout.Width(80)))
        {
            SaveScene();
            EditorSceneManager.OpenScene("Assets/Scenes/Test_Game.unity");
        }

        if (GUILayout.Button("Level_2", GUILayout.Width(80)))
        {
            SaveScene();
            EditorSceneManager.OpenScene("Assets/Scenes/Level_2.unity");
        }*/
    }
    static void OnPlayModeChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            SaveScene();
            EditorSceneManager.OpenScene("Assets/Scenes/Lobby.unity");
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
