using UnityEngine;
using UnityEngine.AI; 
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Move object towards Target.")]
    public class Chase : Action
    {
        public float speed = 0;
        public SharedTransform target;

        private NavMeshAgent agent;

        public override void OnAwake()
        {
            agent = GetComponent<NavMeshAgent>();

            if (agent != null)
                agent.isStopped = false;
        }

        public override TaskStatus OnUpdate()
        {
            if (target.Value == null)
            {
                return TaskStatus.Failure;
            }

            agent.SetDestination(target.Value.position);

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            if (agent != null)
                agent.ResetPath();
        }
    }
}
