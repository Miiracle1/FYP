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
            var bounds = spider.currentRoom.roomBound.bounds;

            Vector3 randomDirection = new(Random.Range(bounds.min.x, bounds.max.x), 
                transform.position.y, 
                Random.Range(bounds.min.z, bounds.max.z));

            if (NavMesh.SamplePosition(randomDirection, out var hit, radius, NavMesh.AllAreas))
            {
                targetPosition.Value = hit.position;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }
    }
}