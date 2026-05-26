using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Move to Position from shared Vector3 variable")]
    public class Wander : Action
    {
        public SharedVector3 targetPos;

        private NavMeshAgent agent;

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();

            if (agent.isStopped)
                agent.isStopped = false;
            agent.SetDestination(targetPos.Value);
        }

        public override TaskStatus OnUpdate()
        {
            if (agent.pathPending)
                return TaskStatus.Running;

            if (agent.remainingDistance <= agent.stoppingDistance)
                return TaskStatus.Success;

            return TaskStatus.Running;
        }
    }
}
