using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Escape to spide forward direction after player gaze.")]
    public class GazeEscape : Action
    {
        [SerializeField] private float escapeDistance = 2f;
        [SerializeField] private float escapeSpeedRatio = 2f;
        [SerializeField] private float stuckTime = 1f;
        [SerializeField] private float movementThreshold = 0.02f;

        [SerializeField] private Vector2 runTimeRange = new Vector2(0.3f, 0.8f);
        [SerializeField] private Vector2 pauseTimeRange = new Vector2(0.1f, 0.3f);

        private SpiderAI spider;
        private NavMeshAgent agent;
        private float stuckTimer;
        private bool isPaused;
        private float stateTimer;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
            agent = GetComponent<NavMeshAgent>();

            stuckTimer = 0f;
            isPaused = false;
            stateTimer = 0.5f;

            if (spider.IsGrabbed || spider.IsAttached) return;

            agent.isStopped = false;
            agent.speed = spider.GetDefaultAgentSpeed() * escapeSpeedRatio;

            var targetPosition = transform.position + transform.forward * escapeDistance;

            if (NavMesh.SamplePosition(targetPosition, out var hit, 0.5f, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
            }
            else
                agent.SetDestination(targetPosition);

            //spider.PlayMoveAnim();
        }

        public override TaskStatus OnUpdate()
        {
            if (spider.IsGrabbed || spider.IsAttached) return TaskStatus.Success;

            stateTimer -= Time.deltaTime;

            if (stateTimer <= 0f)
            {
                isPaused = !isPaused;

                if (isPaused)
                {
                    agent.isStopped = false;
                    //spider.PlayMoveAnim();
                    stateTimer = Random.Range(runTimeRange.x, runTimeRange.y);
                }
                else
                {
                    agent.isStopped = true;
                    stateTimer = Random.Range(pauseTimeRange.x, pauseTimeRange.y);
                }
            }

            if (agent.pathPending)
                return TaskStatus.Running;

            if (agent.remainingDistance < 0.1f)
            {
                return TaskStatus.Success;
            }

            // Stop task if spider not moving like stuck
            if (agent.velocity.sqrMagnitude < movementThreshold * movementThreshold && !isPaused)
            {
                stuckTimer += Time.deltaTime;

                if (stuckTimer >= 1f)
                {
                    return TaskStatus.Success;
                }
            }
            else
            {
                stuckTimer = 0f;
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            agent.speed = spider.GetDefaultAgentSpeed();
            if (agent.isOnNavMesh)
                agent.isStopped = true;
        }
    }
}