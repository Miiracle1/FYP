using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportActivation : MonoBehaviour
{
    public XRRayInteractor teleportInteractor;
    public InputActionProperty teleportActivatorAction;

    void Start()
    {
        teleportInteractor.gameObject.SetActive(false);

        teleportActivatorAction.action.performed += Activation;
    }

    private void Activation(InputAction.CallbackContext obj)
    {
        teleportInteractor.gameObject.SetActive(!teleportInteractor.gameObject.activeSelf);
    }

    private void OnDisable()
    {
        teleportActivatorAction.action.performed -= Activation;
    }
}

