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
        public string targetTag;
        public SharedTransform target;

        private Transform possibleTarget;

        public override void OnAwake()
        {
            var target = GameObject.FindGameObjectWithTag(targetTag);
            possibleTarget = target.transform;
        }

        public override void OnStart()
        {
            base.OnStart();
        }

        public override TaskStatus OnUpdate()
        {
            if (InSight(possibleTarget, fieldOfViewAngle))
            {
                target.Value = possibleTarget;
                return TaskStatus.Success;
            }

            return TaskStatus.Failure;
        }

        public bool InSight(Transform targetTransform, float fieldOfViewAngle)
        { 
            Vector3 direction = targetTransform.position - transform.position;

            return Vector3.Angle(direction, transform.forward) < fieldOfViewAngle;
        }
    }
}