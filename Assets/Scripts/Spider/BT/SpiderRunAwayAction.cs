using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Run away with shared position and stops a while then continue.")]
	public class SpiderRunAwayAction : Action
	{
        public SharedVector3 escapePoint;

        [Header("Movement")]
        [SerializeField] private float escapeSpeed = 6f;
        [SerializeField] private float stoppingDistance = 0.2f;

        [Header("Burst")]
        [SerializeField] private Vector2 runDuration = new(0.6f, 1f);
        [SerializeField] private Vector2 pauseDuration = new(0.15f, 0.4f);

        private NavMeshAgent agent;
        private bool running;
        private float timer;

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();

            agent.speed = escapeSpeed;

            agent.isStopped = false;

            agent.SetDestination(escapePoint.Value);

            running = true;

            timer = Random.Range(runDuration.x, runDuration.y);
        }

        public override TaskStatus OnUpdate()
        {
            if (agent.remainingDistance <= stoppingDistance)
            {
                agent.isStopped = true;

                return TaskStatus.Success;
            }

            timer -= Time.deltaTime;

            if (running)
            {
                if (timer <= 0f)
                {
                    running = false;

                    agent.isStopped = true;

                    timer = Random.Range(pauseDuration.x, pauseDuration.y);
                }
            }
            else
            {
                if (timer <= 0f)
                {
                    running = true;

                    agent.isStopped = false;

                    timer = Random.Range(runDuration.x, runDuration.y);
                }
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            agent.isStopped = false;
        }
    }
}