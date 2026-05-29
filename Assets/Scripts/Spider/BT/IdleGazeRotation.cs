using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine.AI;

namespace Spider.BT
{
    public class IdleGazeRotation : Action
    {
        public float gazeAngle = 30f;
        public float rotationSpeed = 2f;


        private Transform player;
        private Transform head;

        public override void OnStart()
        {
            player = Camera.main.transform;
            /*if (TryGetComponent<SpiderController>(out var spider))
            {
                head = spider.GetHeadSocket();
            }*/
        }

        public override TaskStatus OnUpdate()
        {
            if (player == null)
            { 
                return TaskStatus.Running;
            }

            // Determine what angle player looking cause spider to rotate towards player
            var directionToSpider = (transform.position - player.position).normalized;

            float angle = Vector3.Angle(player.forward, directionToSpider);

            if (angle < gazeAngle)
            {
                return TaskStatus.Running;
            }

            // Rotate spider to player camera position
            Vector3 direction = player.position - transform.position;

            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            { 
                var targetRotation = Quaternion.LookRotation(direction);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            return TaskStatus.Running;
        }
    }
}