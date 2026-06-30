using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Conditional Task to check if phase 2 already")]
	public class Phase2Conditional : Conditional
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