using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;

/// <summary>
/// Handles main flow logic in levels
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject firstPanel;

    [Header("Locomotion")]
    [SerializeField] private DynamicMoveProvider dynamicMoveProvider;

    [Header("Indicator")]
    [SerializeField] private GameObject indicator;
    [SerializeField] private float indicatorTimer = 10f;
    [SerializeField] private float heightOffset = 2f;

    private Phase2Points[] Phase2Points;
    private Coroutine indicatorCoroutine;
    public static event Action OnFirstPhaseStarted;

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

    void Start()
    {
        if (dynamicMoveProvider != null) 
            dynamicMoveProvider.enabled = false;

        Phase2Points = FindObjectsByType<Phase2Points>(FindObjectsSortMode.None);

        SceneLoader.instance.ForceReset();
    }

    private void OnEnable()
    {
        FadeCanvas.OnFinishFadeIn += ShowUI;
        SpiderAI.OnPhase2Start += ShowIndicator;
    }
    private void OnDisable()
    {
        FadeCanvas.OnFinishFadeIn -= ShowUI;
        SpiderAI.OnPhase2Start -= ShowIndicator;
    }

    /***************************************************************************************************************************************************************************************/
    //Game Flow Methods

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
        StopIndicator();
        FadeCanvas fadeCanvas = FindFirstObjectByType<FadeCanvas>();

        if (fadeCanvas != null)
            fadeCanvas.FadeIn();

        Debug.Log("Current Game Progress =" + GameProgressTracker.GameState);
        Debug.Log("Current Player Pref = " + PlayerPrefs.GetString("Tutorial"));

        // If first time playing and win current level go next level
        if (GameProgressTracker.GameState == GameStateEnums.Victory && PlayerPrefs.GetString("Tutorial") == "True" && GameProgressTracker.Scene != SceneEnums.Greenhouse)
        { 
            GameProgressTracker.Scene = SceneEnums.Greenhouse;
            SceneLoader.instance.LoadScene("GreenHouse");
            return;
        }

        GameProgressTracker.Scene = SceneEnums.Lobby;

        SceneLoader.instance.LoadScene("Lobby");
    }

    /***************************************************************************************************************************************************************************************/
    //Indicators

    private void ShowIndicator()
    {
        if (indicatorCoroutine != null)
            StopCoroutine(indicatorCoroutine);

        indicatorCoroutine = StartCoroutine(IndicatorRoutine());
    }

    private IEnumerator IndicatorRoutine()
    {
        while (true)
        {
            SpawnIndicator();
            yield return new WaitForSeconds(indicatorTimer);
        }
    }

    private void SpawnIndicator()
    {
        if (Phase2Points == null)
        {
            Debug.LogWarning("Phase 2 points not in game manager!");
            return;
        }

        foreach (var point in Phase2Points)
        {
            Instantiate(indicator, point.transform.position + new Vector3(0,heightOffset,0), Quaternion.identity, transform);
        }
    }

    private void StopIndicator()
    {
        if (indicatorCoroutine != null)
        {
            StopCoroutine(indicatorCoroutine);
            indicatorCoroutine = null;
        }
    }
}