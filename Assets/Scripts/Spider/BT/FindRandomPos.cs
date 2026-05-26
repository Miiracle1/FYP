using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Get Random Position near Agent.")]
    public class FindRandomPos : Action
    {
        public SharedVector3 targetPosition;
        public float radius = 5f;

        public override TaskStatus OnUpdate()
        {
            Vector3 randomDirection =
                Random.insideUnitSphere * radius;

            randomDirection += transform.position;

            NavMeshHit hit;

            if (NavMesh.SamplePosition(
                randomDirection,
                out hit,
                radius,
                NavMesh.AllAreas))
            {
                targetPosition.Value = hit.position;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}