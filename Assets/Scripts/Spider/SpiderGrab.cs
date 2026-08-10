using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class SpiderGrab : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private InputActionReference rotateAction;
    [SerializeField] private float rotateSpeed = 100f;

    private SnapTurnProvider snapTurnProvider;

    private SpiderAI spider;
    private NavMeshAgent agent;
    private SpiderAttachPoint currentAttachPoint;
    private Player player;
    private Quaternion originalRotation;

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        spider = GetComponent<SpiderAI>();
        agent = GetComponent<NavMeshAgent>();

        player = FindAnyObjectByType<Player>();
        snapTurnProvider = player.GetComponentInChildren<SnapTurnProvider>();
    }

    private void Update()
    {
        if (!spider.IsGrabbed) return;

        Vector2 input = rotateAction.action.ReadValue<Vector2>();

        transform.Rotate(Vector3.up, input.x * rotateSpeed * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.right, -input.y * rotateSpeed * Time.deltaTime, Space.World);
    }

    private void OnEnable()
    {
        rotateAction.action.Enable();
    }

    private void OnDisable()
    {
        rotateAction.action.Disable();
    }

    /***************************************************************************************************************************************************************************************/
    // Methods

    public void Grab(SelectEnterEventArgs args)
    {
        if (spider.mode == SpiderMode.Game)
            if (!spider.InGamePhase2) return;

        if (spider.IsGrabbed) return;

        snapTurnProvider.enabled = false;
        spider.StopAllAnim();
        originalRotation = transform.rotation;
        spider.SetAttached(false);
        player.SetAttachingSpider(false);
        agent.enabled = false;
        spider.SetGrabbed(true);
        player.IsGrabbingSpider = true;

        if (PlayerPrefs.GetString("Tutorial") == "True" && GameProgressTracker.Scene == SceneEnums.Garage)
        {
            StartNarrator.instance.PlaySound(NarratorSounds.garageCaughtSpider);
        }
    }

    public void Release(SelectExitEventArgs args)
    {
        if (spider.mode == SpiderMode.Game)
            if (!spider.InGamePhase2) return;

        snapTurnProvider.enabled = true;

        spider.SetGrabbed(false);
        player.IsGrabbingSpider= false;

        if (currentAttachPoint != null)
        {
            AttachToPoint(currentAttachPoint);
        }
        else
        {
            ReturnToGround();
        }
        spider.StopAllAnim();
    }

    private void AttachToPoint(SpiderAttachPoint point)
    {
        transform.SetParent(point.transform);
        
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.rotation = originalRotation;

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

        transform.rotation = originalRotation;
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
