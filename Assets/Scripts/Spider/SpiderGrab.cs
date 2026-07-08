using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;

public class SpiderGrab : MonoBehaviour
{
    private SpiderAI spider;
    private NavMeshAgent agent;
    private SpiderAttachPoint currentAttachPoint;
    private Player player;

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        spider = GetComponent<SpiderAI>();
        agent = GetComponent<NavMeshAgent>();

        player = FindAnyObjectByType<Player>();
    }

    /***************************************************************************************************************************************************************************************/
    // Methods

    public void Grab(SelectEnterEventArgs args)
    {
        if (!spider.InGamePhase2) return;

        if (spider.IsGrabbed) return;

        spider.SetAttached(false);
        player.SetAttachingSpider(false);
        agent.enabled = false;
        spider.SetGrabbed(true);
    }

    public void Release(SelectExitEventArgs args)
    {
        if (!spider.InGamePhase2) return;

        spider.SetGrabbed(false);

        if (currentAttachPoint != null)
        {
            AttachToPoint(currentAttachPoint);
        }
        else
        {
            ReturnToGround();
        }
    }

    private void AttachToPoint(SpiderAttachPoint point)
    {
        transform.SetParent(point.transform);
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        spider.SetAttached(true);
        player.SetAttachingSpider(true);
        agent.enabled = false;
        currentAttachPoint = null;
    }

    private void ReturnToGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f))
        {
            transform.position = hit.point;
        }

        spider.SetAttached(false);
        player.SetAttachingSpider(false);
        currentAttachPoint = null;

        agent.enabled = true;
        agent.Warp(transform.position);
    }

    /***************************************************************************************************************************************************************************************/
    // Triggers

    public void SetPoint(SpiderAttachPoint point)
    {
        currentAttachPoint = point;
    }
}
