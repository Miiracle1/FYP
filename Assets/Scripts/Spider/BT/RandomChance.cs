using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Return true if gacha random chance success")]
    public class RandomChance : Conditional
    {
        [Range(0f,100f)]
        float chance = 20f;

        bool success = false;

        public override void OnStart()
        {
            float randomValue = Random.Range(0f, 100f);

            success = randomValue <= chance;
        }

        public override TaskStatus OnUpdate()
        {
            return success ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}