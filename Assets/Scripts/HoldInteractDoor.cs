using System;
using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HoldInteractDoor : MonoBehaviour
{
    [SerializeField] private float holdDuration = 2f;

    public SceneEnums scene;
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
        isHolding = true;
        timer = 0;
    }

    /// <summary>
    /// Set static scene enum and load bootstap scene to load specific scene.
    /// By default load lobby
    /// </summary>
    private void OnHoldComplete()
    {
        if (scene == SceneEnums.Lobby)
        {
            LobbyManager.instance.EnterLevel();
            return;
        }

        GameManager.instance.ExitLevel();
    }
}
