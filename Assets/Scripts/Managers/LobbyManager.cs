using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LobbyManager : MonoBehaviour
{
    [Header("Spider Prefab")]
    [SerializeField] private GameObject lobbySpider;
    [SerializeField] private GameObject spawnArea;

    [Header("Door")]
    [SerializeField] private GameObject greenhouseDoor;
    [SerializeField] private GameObject greenhouseUI;
    [SerializeField] private XRGrabInteractable interactable;

    public static LobbyManager instance;
    private SceneEnums nextScene;

    private bool newGame;
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
            //greenhouseDoor.SetActive(false);
            //greenhouseUI.SetActive(false);

            newGame = true;
            interactable.enabled = false;
        }

        if (PlayerPrefs.GetString("Tutorial") == "True")
        {
            //greenhouseDoor.SetActive(false);
            //greenhouseUI.SetActive(false);

            interactable.enabled = false;
            newGame = true;
            GameProgressTracker.ModeState = Mod.New;
        }

        // Use player pref to track if player caught spider in game
        if (PlayerPrefs.GetString("Got Spider") == "True" && lobbySpider != null && spawnArea != null)
        {
            newGame = false;
            greenhouseDoor.SetActive(true);
            greenhouseUI.SetActive(true);
            Instantiate(lobbySpider, spawnArea.transform.position, Quaternion.identity);

            interactable.enabled = true;
        }

        GameProgressTracker.Scene = SceneEnums.Lobby;
        GameProgressTracker.GameState = GameStateEnums.Lobby;

        SceneLoader.instance.ForceReset();
    }

    private void OnEnable()
    {
        FadeCanvas.OnFinishFadeOut += PlayStartNarrator;
    }

    private void OnDisable()
    {
        FadeCanvas.OnFinishFadeOut -= PlayStartNarrator;
    }

    /***************************************************************************************************************************************************************************************/
    //Methods

    private void PlayStartNarrator()
    {
        if (newGame)
        {
            StartNarrator.instance.PlayNarrator();
            newGame = false;
        }

        if (GameProgressTracker.ModeState == Mod.Done || PlayerPrefs.GetString("Got Spider") == "True" || PlayerPrefs.GetString("Tutorial") == "False" || GameProgressTracker.GameState == GameStateEnums.Victory)
        {
            Debug.Log("Should be playing complete sound");
            StartNarrator.instance.PlaySound(NarratorSounds.finishLobby);
        }
    }

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
