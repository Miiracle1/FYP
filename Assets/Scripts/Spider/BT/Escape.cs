using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Escape from player")]
    public class Escape : Action
    {
        [SerializeField] private float escapeDistance = 5f;

        private SpiderAI spider;
        private NavMeshAgent agent;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
            agent = GetComponent<NavMeshAgent>();

            if (GameProgressTracker.Scene == SceneEnums.Greenhouse)
                escapeDistance -= 1f;

            Vector3 destination = transform.position + (transform.forward) * escapeDistance;

            
            agent.isStopped = false;
            agent.SetDestination(destination);
            agent.speed *= 2f;
            spider.PlayMoveAnim();
        }

        public override TaskStatus OnUpdate()
        {
            if (agent.pathPending)
                return TaskStatus.Running;

            var state = spider.animancer.States.Current;
            state.Speed = 2f;

            if (agent.remainingDistance <= 0.01f)
            {
                state.Speed = 1f;
                spider.SetPhase2();
                return TaskStatus.Success;
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