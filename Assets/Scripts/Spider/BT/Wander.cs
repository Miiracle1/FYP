using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Move to Position from shared Vector3 variable")]
    public class Wander : Action
    {
        public SharedVector3 targetPos;

        private NavMeshAgent agent;
        private SpiderAI spider;

        public override void OnStart()
        {
            agent = GetComponent<NavMeshAgent>();
            spider = GetComponent<SpiderAI>();
            agent.isStopped = false;

            agent.SetDestination(targetPos.Value);

            if (spider != null ) 
                spider.PlayMoveAnim();

            Debug.Log("sfgsdfsf" + targetPos);
        }

        public override TaskStatus OnUpdate()
        {
            if (agent.pathPending)
                return TaskStatus.Running;

            // Make sure agent almost reach to end of path to continue branch
            if (agent.remainingDistance <= 0.1f)
            {
                //agent.isStopped = true;
                //agent.ResetPath();
                return TaskStatus.Success;
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
        }
    }
}
