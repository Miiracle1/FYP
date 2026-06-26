using UnityEngine;

/// <summary>
/// Handles main flow logic in levels
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] private float timer = 180f;

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    void Start()
    {
        GameProgressTracker.LightsOn = false;
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
    /// After finish fade in, start showing UI panels
    /// </summary>
    public void ShowUI()
    { 
        
    }
}