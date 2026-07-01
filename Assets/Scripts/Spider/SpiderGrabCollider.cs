using UnityEngine;

/// <summary>
/// Required a Collider component in Spider Grab Collider child game object.
/// Handles collider trigger logic for spider grab.
/// </summary>
public class SpiderGrabCollider : MonoBehaviour
{
    private SpiderGrab spiderGrab;
    private SpiderAttachPoint currentAttachPoint;

    private void Awake()
    {
        spiderGrab = GetComponentInParent<SpiderGrab>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<SpiderAttachPoint>(out var point))
        {
            currentAttachPoint = point;
            spiderGrab.SetPoint(currentAttachPoint);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<SpiderAttachPoint>(out var point))
        {
            if (point == currentAttachPoint)
            {
                currentAttachPoint = null;
                spiderGrab.SetPoint(null);
            }
        }
    }
}
