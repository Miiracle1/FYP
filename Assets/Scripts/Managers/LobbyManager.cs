using System;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;
    private SceneEnums nextScene;

    public event Action OnLobbyChanged; // action to notify subs when changed scene from lobby

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

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

        if (PlayerPrefs.GetString("Tutorial") == "True")
        {
            //nextScene = SceneEnums.Garage;
        }

        GameProgressTracker.Scene = nextScene;
        SceneLoader.instance.LoadScene("Bootstrap Scene");

    }
}
