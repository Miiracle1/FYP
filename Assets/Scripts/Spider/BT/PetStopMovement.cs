using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Stop agent movement")]
	public class PetStopMovement : Action
	{
		private NavMeshAgent agent;

		public override void OnStart()
		{
			agent.isStopped = true;
			agent.ResetPath();
			//If (agent.TryGetComponent<SpiderAI>(out var lobbySpider)) lobbySpider.ResetAnimationState();

		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}