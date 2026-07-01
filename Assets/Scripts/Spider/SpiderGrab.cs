using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.AI;

public class SpiderGrab : MonoBehaviour
{
    private SpiderAI spider;
    private NavMeshAgent agent;
    private BehaviorTree behaviorTree;
    private SpiderAttachPoint currentAttachPoint;
    private Player player;

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    void Start()
    {
        spider = GetComponent<SpiderAI>();
        agent = GetComponent<NavMeshAgent>();
        behaviorTree = GetComponent<BehaviorTree>();

        player = FindAnyObjectByType<Player>();
    }

    /***************************************************************************************************************************************************************************************/
    // Methods

    public void Grab(Transform hand)
    {
        spider.SetAttached(false);

        transform.SetParent(hand);
        agent.enabled = false;
        spider.SetGrabbed(true);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Release()
    {
        if (currentAttachPoint != null)
        {
            spider.SetGrabbed(false);
            AttachToPoint(currentAttachPoint);
        }
        else
        {
            spider.SetGrabbed(false);
            ReturnToGround();
        }
    }

    private void AttachToPoint(SpiderAttachPoint point)
    {
        transform.SetParent(point.transform);
        spider.SetAttached(true);

        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        agent.enabled = false;
    }

    private void ReturnToGround()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 5f))
        {
            transform.position = hit.point;
        }

        transform.SetParent(null);

        agent.enabled = true;
        agent.Warp(transform.position);
    }

    /***************************************************************************************************************************************************************************************/
    // Triggers

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AttachPoints"))
        {
            if (other.TryGetComponent<SpiderAttachPoint>(out var point))
            {
                currentAttachPoint = point;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("AttachPoints"))
        {
            if (other.TryGetComponent<SpiderAttachPoint>(out var point))
            {
                if (point == currentAttachPoint)
                {
                    currentAttachPoint = null;
                }
            }
        }
    }
}
