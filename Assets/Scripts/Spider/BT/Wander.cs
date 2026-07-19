using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Move to Position from shared Vector3 variable. Also stops when timer reach max move time.")]
    public class Wander : Action
    {
        [Header("Settings")]
        [SerializeField] private float rotateSpeed = 360f;
        [SerializeField] private float angleThreshold = 110f;
        [SerializeField] private float maxMoveTime = 3f;
        [SerializeField] private float sprintChance = 35f;
        [SerializeField] private float sprintSpeedRatio = 2f;

        public SharedVector3 targetPos;

        private float timer;
        private float originalSpeed;
        private float originalAnimatorSpeed;
        private bool isSprinting = false;
        private NavMeshAgent agent;
        private SpiderAI spider;
        private Animator animator;

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            spider = GetComponent<SpiderAI>();
            animator = spider.GetAnimatorReference();

            if (spider.IsGrabbed || spider.IsAttached) return;

            agent.isStopped = false;
            agent.speed = spider.GetDefaultAgentSpeed();

            agent.SetDestination(targetPos.Value);

            if (RandomSprintChance() && isSprinting == false)
            {
                isSprinting = true;
                originalSpeed = agent.speed;
                originalAnimatorSpeed = animator.speed;
                agent.speed *= sprintSpeedRatio;
                animator.speed *= sprintSpeedRatio;
            }

            spider.PlayMoveAnim();

            timer = Mathf.RoundToInt(Random.Range(0, maxMoveTime));
            if (timer == 0) timer += 1f; // if somehow timer is 0, fallback to 1 second
        }

        public override TaskStatus OnUpdate()
        {
            timer -= Time.deltaTime;

            if (spider.mode == SpiderMode.Lobby)
            { 
                if (spider.GazeCompleted)
                    return TaskStatus.Success;
            }

            if (timer <= 0f || spider.IsGrabbed || spider.IsAttached)
            {
                return TaskStatus.Success;
            }

            if (agent.pathPending)
                return TaskStatus.Running;

            // Make sure agent almost reach to end of path to continue branch
            if (agent.remainingDistance <= 0.1f)
            {
                return TaskStatus.Success;
            }

            // Get the next point the NavMeshAgent wants to move toward.
            Vector3 direction = agent.steeringTarget - transform.position;
            direction.y = transform.position.y;

            if (direction.sqrMagnitude > 0.001f)
            {
                float angle = Vector3.Angle(transform.forward, direction);

                // Rotate first
                if (angle > angleThreshold)
                {
                    agent.isStopped = true;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);

                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
                }
                else
                {
                    agent.isStopped = false;
                }
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            animator.speed = originalAnimatorSpeed;
            agent.speed = spider.GetDefaultAgentSpeed();
            isSprinting = false;
            spider.StopMoveAnim();

            if (agent.isOnNavMesh)
                agent.isStopped = true;
        }

        private bool RandomSprintChance()
        {
            var chance = Random.Range(0f, 100f);

            return chance <= sprintChance;
        }
    }
}
