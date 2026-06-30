using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Escape from player")]
    public class Escape : Action
    {
        [SerializeField] private float escapeDistance = 5f;

        private SpiderAI spider;
        private NavMeshAgent agent;
        private Animator animator;
        private Quaternion targetRotation;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            Vector3 destination = transform.position + transform.forward * escapeDistance;

            agent.isStopped = false;
            agent.SetDestination(destination);
        }

        public override TaskStatus OnUpdate()
        {
            if (agent.pathPending)
                return TaskStatus.Running;

            if (agent.remainingDistance <= 0.01f)
            {
                spider.SetPhase2();
                return TaskStatus.Success;
            }

            return TaskStatus.Running;

            // Dont know this approach work or not
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f)
                {
                    //animator.SetBool("Walk", false);
                    return TaskStatus.Success;
                }
            }

            return TaskStatus.Running;
        }
    }
}