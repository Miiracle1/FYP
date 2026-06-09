using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Parent Class of Spider AI
/// </summary>
public partial class SpiderAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    /*****************************************************************************************************************************************/
    //Unity Methods

    private void Awake()
    {
        //agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
    }

    void Start()
    {
        GetHandReference();
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

    private void OnAnimatorMove()
    {
        /* let agent speed match animation
        if (animator.GetBool("Running"))
        { 
            agent.speed = (animator.deltaPosition / Time.deltaTime).magnitude;
        }*/
    }
}
