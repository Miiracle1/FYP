using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Stops Spider Chase action. Should restart chase BT")]
    public class StopChase : Action
    {
        private NavMeshAgent agent;

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();


            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}