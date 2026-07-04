using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Check if is phase 2, not just entered")]
	public class Phase2Check : Conditional
	{
		private SpiderAI spider;

        public override void OnStart()
        {
			spider = GetComponent<SpiderAI>();
        }

		public override TaskStatus OnUpdate()
		{
			return spider.InGamePhase2 ? TaskStatus.Success : TaskStatus.Failure;
		}
	}
}