using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public partial class SpiderAI
{
    [Header("Hand Tracking")]
    [SerializeField] private float handTrackDistance;

    private Transform leftHand;
    private Transform rightHand;
    private Transform nearestHand { get; set; }

    /// <summary>
    /// Get VR Hand Controller Reference, use in Start or Awake.
    /// </summary>
    private void GetHandReference()
    {
        var xrOrigin = FindFirstObjectByType<XROrigin>();

        var camTransform = xrOrigin.Camera.transform;

        leftHand = xrOrigin.transform.Find("Camera Offset/Left Controller");

        rightHand = xrOrigin.transform.Find("Camera Offset/Right Controller");

        Debug.Log("Left hand : " + leftHand);
        Debug.Log("Right hand : " + rightHand);
    }
    
    /// <summary>
    /// Constantly check the hand distance. Put in Update.
    /// </summary>
    private void CheckHandDistance()
    { 
        float leftDistance = Vector3.Distance(transform.position, leftHand.position);
        float rightDistance = Vector3.Distance(transform.position, rightHand.position);

        if (leftDistance < handTrackDistance)
        {
            //left hand near agent
            Debug.Log("Left hand is near agent");
        }

        if (rightDistance < handTrackDistance)
        {
            // right hand near agent
            Debug.Log("Right hand is near agent");
        }
    }

    /// <summary>
    /// Get nearest hand towards agent.
    /// </summary>
    /// <returns></returns>
    public Transform GetNearestHand()
    {
        float leftDistance = Vector3.Distance(transform.position, leftHand.position);
        float rightDistance = Vector3.Distance(transform.position, rightHand.position);

        if (leftDistance < rightDistance)
        {
            nearestHand = leftHand;
        }
        else
        {
            nearestHand = rightHand;
        }

        return nearestHand;
    }
}
