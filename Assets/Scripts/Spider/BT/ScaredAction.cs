using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Commence scared action")]
	public class ScaredAction : Action
	{
		[SerializeField] private float interactionDistance = 5f;

		public SharedFloat waitingTime;

		private SpiderAI spider;
		private NavMeshAgent agent;

		public override void OnStart()
		{
			spider = GetComponent<SpiderAI>();
			agent = GetComponent<NavMeshAgent>();
		}

		public override TaskStatus OnUpdate()
		{
            Vector3 playerPos = Camera.main.transform.position;

            float sqrDistance = (transform.position - playerPos).sqrMagnitude;

            if (sqrDistance <= interactionDistance * interactionDistance)
            {
				// Player is within range
				Debug.Log("Player is near, commencing second phase");

				spider.StopAllAnim();

				//lobbySpider.PlayScaredAnimation();
				//waitingTime = lobbySpider.GetAnimationLength() //get the length of animation and pass it to wait task
				waitingTime = 4f;

				return TaskStatus.Success;
            }

            return TaskStatus.Running;
		}
	}
}