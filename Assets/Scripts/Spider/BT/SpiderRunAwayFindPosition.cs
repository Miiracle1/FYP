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
            for (int i = 0; i < searchLimit; i++)
            {
                Vector3 random = transform.position + Random.insideUnitSphere * searchRadius;

                random.y = transform.position.y;

                if (NavMesh.SamplePosition(random, out NavMeshHit hit, 2f, NavMesh.AllAreas))
                {
                    escapePoint.Value = hit.position;
                    return TaskStatus.Success;
                }
            }

            return TaskStatus.Failure;
        }
    }
}