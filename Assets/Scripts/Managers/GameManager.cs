using System;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

/// <summary>
/// Handles main flow logic in levels
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Time")]
    [SerializeField] private float timer = 180f;

    [Header("UI Panels")]
    [SerializeField] private GameObject firstPanel;

    [Header("Locomotion")]
    [SerializeField] private DynamicMoveProvider dynamicMoveProvider;

    public static event Action OnFirstPhaseStarted;
    public static event Action OnSecondPhaseStarted; // Invoke after lights on

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        { 
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (dynamicMoveProvider != null) 
            dynamicMoveProvider.enabled = false;
    }

    private void OnEnable()
    {
        FadeCanvas.OnFinishFadeIn += ShowUI;
    }
    private void OnDisable()
    {
        FadeCanvas.OnFinishFadeIn -= ShowUI;
    }

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    /// <summary>
    /// After finish fade in, start showing UI panels.
    /// And sets game state to playing.
    /// </summary>
    public void ShowUI()
    {
        GameProgressTracker.GameState = GameStateEnums.Playing;
        if (firstPanel != null)
            firstPanel.SetActive(true);
    }

    /// <summary>
    /// Put in UI panel 'Next' button.
    /// Starts level sequence.
    /// </summary>
    public void StartLevel()
    {
        if (firstPanel != null)
            firstPanel.SetActive(false);

        dynamicMoveProvider.enabled = true;
        GameProgressTracker.GameState = GameStateEnums.Playing;
        OnFirstPhaseStarted?.Invoke();
    }

    // Put in first game greenhouse nextScene
    public void WinGame()
    {
        PlayerPrefs.SetString("Got Spider", "True");
    }

    public void ExitLevel()
    {
        FadeCanvas fadeCanvas = FindFirstObjectByType<FadeCanvas>();

        if (fadeCanvas != null)
            fadeCanvas.FadeIn();

        // If first time playing and win current level go next level
        if (GameProgressTracker.GameState == GameStateEnums.Victory && PlayerPrefs.GetString("Tutorial") == "True" && GameProgressTracker.Scene != SceneEnums.Greenhouse)
        { 
            GameProgressTracker.Scene = SceneEnums.Greenhouse;
            SceneLoader.instance.LoadScene("Bootstrap Scene");
            return;
        }

        GameProgressTracker.Scene = SceneEnums.Lobby;

        SceneLoader.instance.LoadScene("Bootstrap Scene");

    }
}