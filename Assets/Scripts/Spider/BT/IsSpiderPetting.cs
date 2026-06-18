using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Conditional task to check if spider is being pet.")]
	public class IsSpiderPetting : Conditional
	{
		private SpiderAI spider;
		private SpiderPet petDetection;

		public override void OnStart()
		{
			spider = GetComponent<SpiderAI>();
			petDetection = GetComponent<SpiderPet>();
		}

		public override TaskStatus OnUpdate()
		{
			if (petDetection.IsPetting)
				return TaskStatus.Success;

			return TaskStatus.Failure;
		}
	}
}