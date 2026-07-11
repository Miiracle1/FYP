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
        [SerializeField] private float rotateSpeed = 360f;
        [SerializeField] private float angleThreshold = 110f;
        [SerializeField] private float maxMoveTime = 3f;

        public SharedVector3 targetPos;

        private float timer;
        private NavMeshAgent agent;
        private SpiderAI spider;

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            spider = GetComponent<SpiderAI>();
            agent.isStopped = false;

            agent.SetDestination(targetPos.Value);

            spider.PlayMoveAnim();

            timer = Mathf.RoundToInt(Random.Range(0, maxMoveTime));
            if (timer == 0) timer += 1f; // if somehow timer is 0, fallback to 1 second
        }

        public override TaskStatus OnUpdate()
        {
            timer -= Time.deltaTime;

            if (timer <= 0f)
            {
                return TaskStatus.Success;
            }

            if (agent.pathPending)
                return TaskStatus.Running;

            // Make sure agent almost reach to end of path to continue branch
            if (agent.remainingDistance <= 0.1f)
            {
                //agent.isStopped = true;
                //agent.ResetPath();
                return TaskStatus.Success;
            }

            // Get the next point the NavMeshAgent wants to move toward.
            Vector3 direction = agent.steeringTarget - transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.001f)
            {
                float angle = Vector3.Angle(transform.forward, direction);

                // Rotate first
                if (angle > angleThreshold)
                {
                    agent.isStopped = true;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);

                    transform.rotation = Quaternion.RotateTowards(
                        transform.rotation,
                        targetRotation,
                        rotateSpeed * Time.deltaTime);
                }
                else
                {
                    agent.isStopped = false;
                }
            }


            return TaskStatus.Running;


            if (!agent.pathPending && agent.remainingDistance <= 0.2f)
            {
                agent.isStopped = true;
                agent.ResetPath();
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }
        public override void OnEnd()
        {
            spider.StopMoveAnim();
            agent.isStopped = true;
        }
    }
}
