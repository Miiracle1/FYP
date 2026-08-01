using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Teleport Agent to random point based on cached phase 2 TP points")]
	public class TeleportPhase2Points : Action
	{
		private SpiderAI spider;
		private NavMeshAgent agent;
		private Transform teleportPoint;

		public override void OnStart()
		{
			spider = GetComponent<SpiderAI>();
			agent = GetComponent<NavMeshAgent>();

			var randomPoint = Random.Range(0, spider.SpiderPhase2Points.Length);
			spider.SetCurrentTPPoint(spider.SpiderPhase2Points[randomPoint]);
			teleportPoint = spider.SpiderPhase2Points[randomPoint].transform;
            agent.Warp(teleportPoint.position);
        }

		public override TaskStatus OnUpdate()
		{
			if (teleportPoint == null) return TaskStatus.Running;

			return TaskStatus.Success;
		}

        public override void OnEnd()
        {
            spider.EnterPhase2();
			spider.StopAllAnim();
        }
	}
}