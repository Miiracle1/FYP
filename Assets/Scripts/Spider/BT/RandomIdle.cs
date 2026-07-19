using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Randomly Plays Idle Animation")]
	public class RandomIdle : Action
	{
		[SerializeField] private float chance = 40f;
		[SerializeField] private bool forcePlay = false;

		private SpiderAI spider;

		public override void OnStart()
		{
			spider = GetComponent<SpiderAI>();

			var randomNum = Random.Range(0,100);

			if (randomNum <= chance && spider.GetMoveBool() == false) // if gacha and not moving
			{
				spider.PlayIdleAnim();
			}
		}

		public override TaskStatus OnUpdate()
		{
			if (forcePlay)
				return TaskStatus.Success;

			if (spider.spottedPlayer == true)
				return TaskStatus.Failure;

			return TaskStatus.Success;
		}
	}
}