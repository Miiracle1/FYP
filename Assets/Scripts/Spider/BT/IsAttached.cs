using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Conditional to check if spider is attached")]
	public class IsAttached : Conditional
	{
		private SpiderAI spider;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
        }

		public override TaskStatus OnUpdate()
		{
			return spider.IsAttached ? TaskStatus.Success : TaskStatus.Failure;
		}
	}
}