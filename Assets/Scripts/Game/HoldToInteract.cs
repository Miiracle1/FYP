using UnityEngine;
using UnityEngine.InputSystem;

public class HoldToInteract : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference leftTriggerAction;
    [SerializeField] private InputActionReference rightTriggerAction;

    [Header("Settings")]
    [SerializeField] private float holdDuration = 2f;

    private bool controllerInside;
    private float timer;
    private bool interacted;
    private HoldInteractDoor interaction;

    private void OnEnable()
    {
        leftTriggerAction.action.Enable();
        rightTriggerAction.action.Enable();
    }

    private void OnDisable()
    {
        leftTriggerAction.action.Disable();
        rightTriggerAction.action.Disable();
    }

    private void Start()
    {
        interaction = GetComponent<HoldInteractDoor>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Right_Controller") || other.CompareTag("Left_Controller"))
        {
            controllerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Right_Controller") || other.CompareTag("Left_Controller"))
        {
            controllerInside = false;
            timer = 0f;
            interacted = false;
        }
    }

    private void Update()
    {
        if (!controllerInside)
            return;

        if (leftTriggerAction.action.IsPressed() || rightTriggerAction.action.IsPressed())
        {
            timer += Time.deltaTime;

            if (!interacted && timer >= holdDuration)
            {
                interacted = true;
                timer = 0f;

                DoStuff();
            }
        }
        else
        {
            timer = 0f;
        }
    }

    private void DoStuff()
    {
        interaction.ForceComplete();
    }
}
