using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SpiderGrab : MonoBehaviour
{
    private SpiderAI spider;
    private NavMeshAgent agent;
    private SpiderAttachPoint currentAttachPoint;
    private Player player;
    private XRGrabInteractable grabInteractable;

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        spider = GetComponent<SpiderAI>();
        agent = GetComponent<NavMeshAgent>();

        player = FindAnyObjectByType<Player>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(Grab);
        grabInteractable.selectExited.AddListener(Release);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(Grab);
        grabInteractable.selectExited.RemoveListener(Release);
    }

    /***************************************************************************************************************************************************************************************/
    // Methods

    public void Grab(SelectEnterEventArgs args)
    {
        if (spider.IsGrabbed)
            return;

        spider.SetAttached(false);
        player.SetAttachingSpider(false);
        agent.enabled = false;
        spider.SetGrabbed(true);
    }

    public void Release(SelectExitEventArgs args)
    {
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
