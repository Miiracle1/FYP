using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LobbyManager : MonoBehaviour
{
    [Header("Spider Prefab")]
    [SerializeField] private GameObject lobbySpider;
    [SerializeField] private GameObject spawnArea;

    [Header("Door")]
    [SerializeField] private GameObject greenhouseDoor;
    [SerializeField] private GameObject greenhouseUI;

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
        // Use player pref to track if first time playing
        if (!PlayerPrefs.HasKey("Tutorial"))
        {
            PlayerPrefs.SetString("Tutorial", "True");
            greenhouseDoor.SetActive(false);
            greenhouseUI.SetActive(false);
            Debug.Log("Set Player pref tutorial to true");
        }

        if (PlayerPrefs.GetString("Tutorial") == "True")
        {
            greenhouseDoor.SetActive(false);
            greenhouseUI.SetActive(false);
        }

        Debug.Log("Player pref tutorial is " + PlayerPrefs.GetString("Tutorial"));
        Debug.Log("player pref got spider is " + PlayerPrefs.GetString("Got Spider"));

        // Use player pref to track if player caught spider in game
        if (PlayerPrefs.GetString("Got Spider") == "True" && lobbySpider != null && spawnArea != null)
        {
            Debug.Log("Should spawn spider");
            greenhouseDoor.SetActive(true);
            greenhouseUI.SetActive(true);
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
            scene = SceneEnums.Garage;
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
