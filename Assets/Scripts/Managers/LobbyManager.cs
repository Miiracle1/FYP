using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LobbyManager : MonoBehaviour
{
    [Header("Spider Prefab")]
    [SerializeField] private GameObject lobbySpider;
    [SerializeField] private GameObject spawnArea;

    public static LobbyManager instance;
    private SceneEnums nextScene;

    public event Action OnLobbyChanged; // action to notify subs when changed nextScene from lobby

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        Debug.Log("Teasting scene loader instance" + SceneLoader.instance);

        // Use player pref to track if first time playing
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetString("Tutorial", "True");
        }

        // Use player pref to track if player caught spider in game
        if (PlayerPrefs.GetString("Got Spider") == "True" && lobbySpider != null && spawnArea != null)
        {
            Instantiate(lobbySpider, spawnArea.transform.position, Quaternion.identity);
        }

        GameProgressTracker.Scene = SceneEnums.Lobby;
        GameProgressTracker.GameState = GameStateEnums.Lobby;

        SceneLoader.instance.ForceReset();
    }

    /***************************************************************************************************************************************************************************************/
    //Methods

    public void SetGarageScene(SelectEnterEventArgs args)
    {
        nextScene = SceneEnums.Garage;
    }

    public void SetGreenHouseScene(SelectEnterEventArgs args)
    {
        nextScene = SceneEnums.Greenhouse;
    }

    public void EnterLevel(SceneEnums scene)
    { 
        FadeCanvas fadeCanvas = FindFirstObjectByType<FadeCanvas>();

        if (fadeCanvas != null) 
            fadeCanvas.FadeIn();

        if (PlayerPrefs.GetString("Tutorial") == "True")
        {
            //nextScene = SceneEnums.Garage;
        }

        GameProgressTracker.Scene = scene;

        if (scene == SceneEnums.Garage)
        {
            SceneLoader.instance.LoadScene("Garage");
        }
        else if (scene == SceneEnums.Greenhouse)
        {
            SceneLoader.instance.LoadScene("GreenHouse");
        }

        //SceneLoader.instance.LoadScene("Bootstrap Scene");
    }
}
