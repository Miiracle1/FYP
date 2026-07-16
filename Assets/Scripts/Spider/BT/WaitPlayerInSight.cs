using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace spider.BT
{
	[TaskCategory("Spider")]
	[TaskDescription("Waits for spider to sees the player")]
	public class WaitPlayerInSight : Action
	{
		[Header("Settings")]
		[SerializeField] private float fov = 100f;
		[SerializeField] private float viewDistance = 4f;
		[SerializeField] private LayerMask visibleLayerMask;
        [SerializeField] private float requiredLookTime = 0.5f;

        [Range(0f, 1f)]
        public float playerLookThreshold = 0.9f;

        private Transform playerHead;

        private float nextCheckTime;
        private const float checkInterval = 0.2f;

        private SpiderAI spider;
		private NavMeshAgent agent;
        private float lookTimer;

        public override void OnStart()
		{
            spider = GetComponent<SpiderAI>();
			agent = GetComponent<NavMeshAgent>();
		}

        public override TaskStatus OnUpdate()
        {
            /*
            playerHead = Camera.main.transform;

            if (Time.time < nextCheckTime)
                return TaskStatus.Running;

            nextCheckTime = Time.time + checkInterval;*/

            if (PlayerCanSeeSpider())
            {
                lookTimer += Time.deltaTime;

                if (lookTimer >= requiredLookTime)
                    return TaskStatus.Success;
            }
            else
            {
                lookTimer = 0f;
            }

            return TaskStatus.Running;
        }

        private bool SpiderCanSeePlayer()
        {
            Vector3 direction = playerHead.position - transform.position;

            float distance = direction.magnitude;

            if (distance > viewDistance)
                return false;

            float angle = Vector3.Angle(transform.forward, direction);

            if (angle > fov * 0.5f)
                return false;

            if (Physics.Raycast(
                    transform.position,
                    direction.normalized,
                    out RaycastHit hit,
                    distance,
                    visibleLayerMask))
            {
                return hit.transform == playerHead;
            }

            return false;
        }

        private bool PlayerLookingAtSpider()
        {
            Vector3 toSpider =
                (transform.position - playerHead.position).normalized;

            float dot = Vector3.Dot(playerHead.forward, toSpider);

            return dot >= playerLookThreshold;
        }

        private bool PlayerCanSeeSpider()
        {
            Vector3 toSpider = transform.position - playerHead.position;

            float distance = toSpider.magnitude;

            if (distance > viewDistance)
                return false;

            float angle = Vector3.Angle(playerHead.forward, toSpider);

            if (angle > fov * 0.5f)
                return false;

            if (Physics.Raycast(
                    playerHead.position,
                    toSpider.normalized,
                    out RaycastHit hit,
                    distance,
                    visibleLayerMask))
            {
                return hit.transform == transform;
            }

            return false;
        }
    }
}