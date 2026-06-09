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
            agent.isStopped = false;

            agent.SetDestination(targetPos.Value);

            Debug.Log("sfgsdfsf" + targetPos);
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;

            if (agent.pathPending)
                return TaskStatus.Running;

            Debug.Log("tyytry" + agent.stoppingDistance);
            Debug.Log("remaining" + agent.remainingDistance);

            if (!agent.pathPending && agent.remainingDistance <= 0.2f)
            {
                agent.isStopped = true;
                agent.ResetPath();
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}
