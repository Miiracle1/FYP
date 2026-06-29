using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Check if spider is activated in gameplay mode or not")]
	public class TriggerActivate : Conditional
	{
		private SpiderAI spider;

        public override void OnStart()
        {
			spider = GetComponent<SpiderAI>();
        }

		public override TaskStatus OnUpdate()
		{
			return spider.InGamePhase1 == true ? TaskStatus.Success : TaskStatus.Failure;
		}
	}
}