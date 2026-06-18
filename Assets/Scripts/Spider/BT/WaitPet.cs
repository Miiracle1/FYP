using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Keep checking if still being pet to keep branch alive")]
	public class WaitPet : Action
	{
		private SpiderPet petDetection;

		public override void OnStart()
		{
			petDetection = GetComponent<SpiderPet>();
		}

		public override TaskStatus OnUpdate()
		{
			if (petDetection.IsPetting)
			{
				return TaskStatus.Running;
			}

			return TaskStatus.Success;
		}
	}
}