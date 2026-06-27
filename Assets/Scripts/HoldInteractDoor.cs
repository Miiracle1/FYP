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

    public void StartInteraction(SelectEnterEventArgs args)
    {
        isHolding = true;
        timer = 0;
    }

    public void StopInteraction(SelectExitEventArgs args)
    {
        isHolding = true;
        timer = 0;
    }

    private void OnHoldComplete()
    {
        if (scene == SceneEnums.Lobby)
        {
            LobbyManager.instance.EnterLevel(); 
        }

        if (scene == SceneEnums.Garage)
        { 
            
        }
    }
}
