using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

/// <summary>
/// Handles Gaze Interaction
/// </summary>
public partial class SpiderAI
{
    [Header("Gaze Settings")]
    [SerializeField] private float requiredLookTime = 0.5f;
    [SerializeField] private float maxDistance = 4f;
    [Range(0f, 180f)]
    [SerializeField] private float eyeAngleThreshold = 50f;

    private bool isHovered;
    private bool gazeCompleted;
    private float lookTimer;
    private XRGazeInteractor currentInteractor;
    public bool GazeCompleted => gazeCompleted;
    public Transform GazeTarget => currentInteractor != null ? currentInteractor.transform : null;

    /***************************************************************************************************************************************************************************************/

    private void UpdateGaze()
    {
        if (!isHovered || currentInteractor == null)
            return;

        if (Vector3.Distance(currentInteractor.transform.position, headSocket.transform.position) > maxDistance)
        {
            ResetGaze();
            return;
        }

        // Direction from player to spider's eyes
        Vector3 forward = currentInteractor.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 toEye =
            headSocket.transform.position - currentInteractor.transform.position;
        toEye.y = 0;
        toEye.Normalize();

        float angle = Vector3.Angle(forward, toEye);

        Debug.DrawRay(
    currentInteractor.transform.position,
    currentInteractor.transform.forward * 2f,
    Color.blue);

        Debug.DrawRay(
            currentInteractor.transform.position,
            toEye * 2f,
            Color.red);

        if (angle > eyeAngleThreshold)
        {
            lookTimer = 0f;
            return;
        }

        lookTimer += Time.deltaTime;

        if (lookTimer >= requiredLookTime)
        {
            gazeCompleted = true;
            lookTimer = 0f;
            isHovered = false;
        }
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        currentInteractor = args.interactorObject as XRGazeInteractor;

        if (currentInteractor == null)
            return;

        lookTimer = 0f;
        gazeCompleted = false;
        isHovered = true;
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        ResetGaze();
    }

    private void ResetGaze()
    {
        isHovered = false;
        lookTimer = 0f;
        gazeCompleted = false;
        currentInteractor = null;
    }

    public void FinishGaze()
    {
        ResetGaze();
    }
}
