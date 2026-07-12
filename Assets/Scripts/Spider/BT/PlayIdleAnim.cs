using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Play Idle Animation")]
    public class PlayIdleAnim : Action
    {
        private SpiderAI spider;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();

            spider.StopAllAnim();
            spider.PlayIdleAnim();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }
    }
}