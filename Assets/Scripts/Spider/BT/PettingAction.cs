using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Start Pet Behavior")]
	public class PettingAction : Action
	{
		private SpiderAI spider;
		private NavMeshAgent agent;
		private Animator animator;
		public override void OnStart()
		{

		}

		public override TaskStatus OnUpdate()
		{
			return TaskStatus.Success;
		}
	}
}