using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Rotates towards player if player look at agent")]
    public class IdleGazeRotation : Action
    {
        [SerializeField] private float requiredGazeTime = 0.6f;
        [SerializeField] private float rotationThreshold = 5f;

        [SerializeField]
        [Range(0f, 1f)]
        private float gazeThreshold = 0.95f;
        [SerializeField] public float rotationSpeed = 360f;

        private SpiderAI spider;
        private Transform player;
        private NavMeshAgent agent;
        private Transform head;
        private float gazeTimer;
        private Quaternion targetRotation;
        private bool targetRotationSet;

        public override void OnStart()
        {
            spider = GetComponent<SpiderAI>();
            agent = GetComponent<NavMeshAgent>();

            head = spider.GetHeadSocket().transform;
            player = Camera.main.transform;

            gazeTimer = 0f;
            targetRotationSet = false;
        }

        public override TaskStatus OnUpdate()
        {
            if (player == null) return TaskStatus.Failure;

            // Determine what angle player looking cause lobbySpider to rotate towards player
            Vector3 playerForward = player.forward;
            playerForward.y = 0f;
            playerForward.Normalize();

            Vector3 toSpider = head.position - player.position;
            toSpider.y = 0f;
            toSpider.Normalize();

            float dot = Vector3.Dot(playerForward, toSpider);

            if (dot < gazeThreshold)
            {
                gazeTimer = 0f;
                return TaskStatus.Failure;
            }

            // Player looking at spider
            gazeTimer += Time.deltaTime;

            // If gaze enough time
            if (gazeTimer < requiredGazeTime)
            {
                return TaskStatus.Running;
            }

            // Pick a random turn direction once
            if (!targetRotationSet)
            {
                int random = Random.Range(0, 3);
                var randomAngle = random switch
                {
                    0 => -90f, // Left
                    1 => 90f,  // Right
                    _ => 180f, // Back
                };
                targetRotation = Quaternion.Euler(0f, transform.eulerAngles.y + randomAngle, 0f);

                targetRotationSet = true;
            }

            // Rotate towards chosen direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) <= rotationThreshold)
            {
                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            gazeTimer = 0f;
            targetRotationSet = false;
            agent.speed = spider.GetDefaultAgentSpeed();
        }
    }
}