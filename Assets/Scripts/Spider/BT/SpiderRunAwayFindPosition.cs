using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Finds a position for spider to run away to.")]
	public class SpiderRunAwayFindPosition : Action
	{
        public SharedVector3 escapePoint;

        [SerializeField] private float searchRadius = 6f;
        [SerializeField] private int searchLimit = 20;

        private SpiderAI spider;
		private NavMeshAgent agent;

		public override void OnStart()
		{
			spider = GetComponent<SpiderAI>();
			agent = GetComponent<NavMeshAgent>();
		}

		public override TaskStatus OnUpdate()
		{
            var bounds = spider.currentRoom.roomBound.bounds;

            const int maxAttempts = 20;

            for (int i = 0; i < maxAttempts; i++)
            {
                Vector3 randomDirection = new(Random.Range(bounds.min.x, bounds.max.x), transform.position.y,
                    Random.Range(bounds.min.z, bounds.max.z));

                if (Vector3.Distance(transform.position, randomDirection) > searchRadius)
                    continue;

                if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                {
                    escapePoint.Value = hit.position;
                    return TaskStatus.Success;
                }
            }

            return TaskStatus.Failure;
        }
    }
}