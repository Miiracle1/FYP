using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

/// <summary>
/// Parent Class of Spider AI
/// </summary>
public partial class SpiderAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Rigidbody rb;

    public SpiderMode mode;
    public XRGrabInteractable xRGrabInteractable;

    private float defaultAgentMoveSpeed;
    private float moveAnimationSpeed;
    private float defaultAnimationSpeed;
    public Animator animator;

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        if (mode == SpiderMode.Game)
        {
            CachePhase2Points();
        }
    }

    void Start()
    {
        //GetHandReference();
        moveAnimationSpeed = movingClip.apparentSpeed;
        defaultAgentMoveSpeed = agent.speed;

        if (mode == SpiderMode.Game)
        {
            StartPhase1();
        }

        //defaultAnimationSpeed = animator.speed;

        if (mode == SpiderMode.Game)
            xRGrabInteractable.enabled = false;
    }

    void Update()
    {
        //CheckHandDistance();

        if (mode == SpiderMode.Lobby || InGamePhase2)
        {
            UpdateGaze();
        }

        float currentSpeed = agent.velocity.magnitude;
        animator.SetFloat("Speed", currentSpeed);
        if (currentSpeed >= 0.08f && !specialMove)
            PlayMoveAnim();

        if (currentSpeed < 0.08f)
            StopMove();

        RandomIdle();
    }

    private void OnEnable()
    {
        if (mode == SpiderMode.Game)
        {
            GameManager.OnFirstPhaseStarted += StartPhase1;
        }
    }

    private void OnDisable()
    {
        if (mode == SpiderMode.Game)
        {
            GameManager.OnFirstPhaseStarted -= StartPhase1;
        }
    }

    
    private void OnAnimatorMove()
    {
        if (IsGrabbed || isAttached) return;
        /*
        //let agent speed match animation
        if (animator.GetBool("Move"))
        {
            //agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
            animator.speed = agent.speed / moveAnimationSpeed;
        }
        else
        { 
            animator.speed = defaultAnimationSpeed;
        }*/
    }

    public float GetDefaultAgentSpeed()
    { 
        return defaultAgentMoveSpeed;
    }

    public void EnableGrab()
    { 
        xRGrabInteractable.enabled = true;
    }
}

public enum SpiderMode
{ 
    Lobby,
    Game,
    Phase1,
    Phase2
}