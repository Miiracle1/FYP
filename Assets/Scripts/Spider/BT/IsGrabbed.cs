using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Conditional task to check if spider is grabbed.")]
	public class IsGrabbed : Conditional
	{
		private SpiderAI spider;

        public override void OnStart()
        {
			spider = GetComponent<SpiderAI>();
        }

		public override TaskStatus OnUpdate()
		{
			return spider.IsGrabbed ? TaskStatus.Success : TaskStatus.Failure;
		}
	}
}