using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Conditional Task to check if just entered phase 2")]
	public class Phase2Conditional : Conditional
	{
		private SpiderAI spider;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
        }

		public override TaskStatus OnUpdate()
		{
			return spider.justEnteredPhase2 ? TaskStatus.Success : TaskStatus.Failure;
		}
	}
}