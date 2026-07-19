using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using TMPro;
using UnityEngine;
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
        [SerializeField] private float radius = 4f;

        private SpiderAI spider;
		private NavMeshAgent agent;

		public override void OnStart()
		{
			spider = GetComponent<SpiderAI>();
			agent = GetComponent<NavMeshAgent>();
		}

		public override TaskStatus OnUpdate()
		{
            if (GameProgressTracker.Scene == SceneEnums.Garage)
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
            }

            if (GameProgressTracker.Scene == SceneEnums.Greenhouse)
            {
                Vector3 randomDirection =
                Random.insideUnitSphere * radius;

                randomDirection += transform.position;

                if (NavMesh.SamplePosition(
                    randomDirection,
                    out NavMeshHit hit,
                    radius,
                    NavMesh.AllAreas))
                {
                    escapePoint.Value = hit.position;
                    return TaskStatus.Success;
                }
            }
            return TaskStatus.Failure;
        }
    }
}