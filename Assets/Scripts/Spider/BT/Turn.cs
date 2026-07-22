using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Turns 180 Degrees")]
    public class Turn : Action
    {
        [SerializeField] private float rotationSpeed = 180f;
        private SpiderAI spider;
        private NavMeshAgent agent;
        private Quaternion targetRotation;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
            agent = GetComponent<NavMeshAgent>();

            targetRotation = transform.rotation * Quaternion.Euler(0f, 180f, 0f);

            spider.ResetIdle();
            spider.PlayMoveAnim();
        }

        public override TaskStatus OnUpdate()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                spider.StopMoveAnim();
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}