using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using BehaviorDesigner.Runtime;

namespace Spider.BT
{
    [TaskCategory("Spider")]
    [TaskDescription("Set target when target enter sight.")]
    public class WithinSight : Conditional
    {
        public float fieldOfViewAngle;
        public float viewDistance;
        public string targetTag;
        public SharedTransform target;
        public LayerMask visibleLayerMask;

        private Transform possibleTarget;
        private Transform eyeSocket;

        private float nextCheckTime;
        private float checkInterval = 0.2f;
        private bool playerInSightBefore;

        /******************************************************************************************************/
        public override void OnAwake()
        {
            var foundTarget = GameObject.FindGameObjectWithTag(targetTag);

            if (foundTarget != null)
            {
                possibleTarget = foundTarget.transform;
            }

            /*
            if (TryGetComponent<SpiderController>(out var spider))
            {
                eyeSocket = spider.GetEyeSocketTransform();
            }*/

            //var target = GameObject.FindGameObjectWithTag(targetTag);
            //possibleTarget = target.transform;
        }

        public override void OnStart()
        {
            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (possibleTarget == null)
                return TaskStatus.Failure;

            // Interval check
            if (Time.time < nextCheckTime)
                return TaskStatus.Failure;

            nextCheckTime = Time.time + checkInterval;

            playerInSightBefore = InSight(possibleTarget, fieldOfViewAngle);

            if (playerInSightBefore)
            {
                target.Value = possibleTarget;
            }

            return playerInSightBefore ? TaskStatus.Success : TaskStatus.Failure;
        }

        public bool InSight(Transform targetTransform, float fieldOfViewAngle)
        {
            //Vector3 direction = targetTransform.position - eyeSocket.position;

            Vector3 direction = targetTransform.position - transform.position;

            float distance = direction.magnitude;

            // Distance Check
            if (distance > viewDistance)
                return false;

            // Angle Check
            float angle = Vector3.Angle(transform.forward, direction);

            if (angle > fieldOfViewAngle * 0.5f)
                return false;

            // Raycast check
            Ray ray = new(transform.position, direction.normalized);

            if (Physics.Raycast(ray, out var hit, viewDistance, visibleLayerMask))
            {
                if (hit.transform == targetTransform)
                    return true;
            }

            return false;
        }
    }
}