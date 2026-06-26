using System;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    private SceneEnums nextScene;

    public event Action OnLobbyChanged; // action to notify subs when changed scene from lobby

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Start()
    {
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetString("Tutorial", "True");
        }
    }

    /***************************************************************************************************************************************************************************************/
    //Methods

    public void SetGarageScene()
    {
        nextScene = SceneEnums.Garage;
    }

    public void SetGreenHouseScene()
    {
        nextScene = SceneEnums.Greenhouse;
    }

    public void EnterLevel()
    { 
        FadeCanvas fadeCanvas = FindFirstObjectByType<FadeCanvas>();

        if (fadeCanvas != null) 
            fadeCanvas.FadeIn();

        GameProgressTracker.Scene = nextScene;
        SceneLoader.instance.LoadScene("Bootstrap Scene");

    }
}
