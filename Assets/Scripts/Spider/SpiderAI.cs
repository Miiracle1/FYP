using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Parent Class of Spider AI
/// </summary>
public partial class SpiderAI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Animator animator;

    public SpiderMode mode;

    private float moveAnimationSpeed;
    private float defaultAnimationSpeed;

    /***************************************************************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (mode == SpiderMode.Game)
        {
            CachePhase2Points();
        }
    }

    void Start()
    {
        GetHandReference();
        moveAnimationSpeed = 3.47f;

        if (mode == SpiderMode.Game)
        {
            StartPhase1();
        }

        defaultAnimationSpeed = animator.speed;
    }

    void Update()
    {
        animator.SetBool("Move", agent.velocity.magnitude > 0.1f); // Constantly checking spider velocity to play move animation

        CheckHandDistance();
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
        if (IsGrabbed) return;
        //let agent speed match animation
        if (animator.GetBool("Move"))
        {
            //agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
            animator.speed = agent.speed / moveAnimationSpeed;
        }
        else
        { 
            animator.speed = defaultAnimationSpeed;
        }
    }

    public Animator GetAnimatorReference()
    {
        if (!animator) return null;
        
        return animator;
    }
}

public enum SpiderMode
{ 
    Lobby,
    Game,
    Phase1,
    Phase2
}