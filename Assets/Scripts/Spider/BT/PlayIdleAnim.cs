using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class PlayIdleAnim : Action
{
    private SpiderAI spider;
    private NavMeshAgent agent;

    public override void OnStart()
	{
        spider = GetComponent<SpiderAI>();
        agent = GetComponent<NavMeshAgent>();

        spider.StopAllAnim();
    }

	public override TaskStatus OnUpdate()
	{
		return TaskStatus.Success;
	}
}