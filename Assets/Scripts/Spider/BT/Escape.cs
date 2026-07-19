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

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
            agent = GetComponent<NavMeshAgent>();

            Vector3 destination = transform.position + (transform.forward) * escapeDistance;

            agent.isStopped = false;
            agent.SetDestination(destination);
            spider.PlayMoveAnim();
        }

        public override TaskStatus OnUpdate()
        {
            if (agent.pathPending)
                return TaskStatus.Running;

            if (agent.remainingDistance <= 0.01f)
            {
                spider.SetPhase2();
                spider.StopMoveAnim();
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}