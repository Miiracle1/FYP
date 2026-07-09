using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoldInteractDoor : MonoBehaviour
{
    [SerializeField] private float holdDuration = 2f;

    public SceneEnums currentScene;
    public SceneEnums nextScene;
    private bool isHolding;
    private float timer;

    public static event Action onHoldComplete;

    public float Progress => timer/holdDuration;
    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Update()
    {
        if (!isHolding)
            return;

        timer += Time.deltaTime;

        if (timer >= holdDuration)
        {
            isHolding = false;
            timer = 0;

            OnHoldComplete();
        }
    }

    /***************************************************************************************************************************************************************************************/
    //Methods

    /// <summary>
    /// Put in select enter events.
    /// Once triggered, start hold interaction.
    /// </summary>
    /// <param name="args"></param>
    public void StartInteraction(SelectEnterEventArgs args)
    {
        isHolding = true;
        timer = 0;
    }

    /// <summary>
    /// Put in select exit events,
    /// Once stop holding, stops interaction.
    /// </summary>
    /// <param name="args"></param>
    public void StopInteraction(SelectExitEventArgs args)
    {
        isHolding = false;
        timer = 0;
    }

    /// <summary>
    /// Set static nextScene enum and load bootstap nextScene to load specific nextScene.
    /// By default load lobby
    /// </summary>
    private void OnHoldComplete()
    {
        if (currentScene == SceneEnums.Lobby)
        {
            LobbyManager.instance.EnterLevel(nextScene);
            return;
        }

        GameManager.instance.ExitLevel();
    }

    public void ForceComplete()
    {
        OnHoldComplete();
    }
}
