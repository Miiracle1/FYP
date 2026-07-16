using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Find random position in current room spider is currently in")]
	public class RoomFindRandomPos : Action
	{
		private SpiderAI spider;
		public SharedVector3 targetPosition;

        public float radius = 5f;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
        }

        public override TaskStatus OnUpdate()
        {
            Bounds bounds = spider.currentRoom.roomBound.bounds;

            const int maxAttempts = 20;

            for (int i = 0; i < maxAttempts; i++)
            {
                Vector3 randomPosition = new(
                    Random.Range(bounds.min.x, bounds.max.x),
                    transform.position.y,
                    Random.Range(bounds.min.z, bounds.max.z));

                // Too far away? Try another point.
                if (Vector3.Distance(transform.position, randomPosition) > radius)
                    continue;

                if (NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                {
                    targetPosition.Value = hit.position;
                    return TaskStatus.Success;
                }
            }

            return TaskStatus.Failure;
        }
    }
}