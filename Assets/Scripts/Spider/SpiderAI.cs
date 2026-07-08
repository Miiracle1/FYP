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

        if (mode == SpiderMode.Game)
        {
            StartPhase1();
        }
    }

    void Update()
    {
        /* Example of playing animation when agent is moving
        if (agent.velocity.magnitude != 0f)
        {
            animator.SetBool("Running", true);
        }
        else 
        {
            animator.SetBool("Running", false);
        }*/

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
         //let agent speed match animation
        if (animator.GetBool("Move"))
        { 
            agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
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