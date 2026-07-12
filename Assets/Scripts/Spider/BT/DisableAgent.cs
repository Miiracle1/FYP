using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Stops agent movement or nav mesh behavior")]
    public class DisableAgent : Action
    {
        private SpiderAI spider;
        private NavMeshAgent agent;

        public bool grabState; //Determine if task is for grab or attach

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
            agent = GetComponent<NavMeshAgent>();

            StopAgent();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.Success;
        }

        private void StopAgent()
        { 
            //agent.isStopped = true;

            if (grabState && spider.IsGrabbed == false)
                spider.SetGrabbed(true);

            if (!grabState && spider.IsAttached == false)
                spider.SetAttached(true);
        }
    }
}