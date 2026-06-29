using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Turns around then escape from player")]
    public class Escape : Action
    {
        [SerializeField] private float rotationSpeed = 180f;
        private SpiderAI spider;
        private NavMeshAgent agent;
        private Animator animator;
        private Quaternion targetRotation;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            targetRotation = transform.rotation * Quaternion.Euler(0f, 180f, 0f);
        }

        public override TaskStatus OnUpdate()
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
    }
}