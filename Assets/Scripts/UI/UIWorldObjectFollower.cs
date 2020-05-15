using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class UIWorldObjectFollower : MonoBehaviour
    {
        [SerializeField]
        private Vector3 offset = Vector3.zero;

        [SerializeField]
        private bool smoothFollow = false;

        [SerializeField]
        private float followSpeed = 10.0f;

        public Transform Target { get; set; }

        private void LateUpdate()
        {
            if (!Target)
            { return; }
            
            FollowTarget();
        }

        private void FollowTarget()
        {
            Vector3 newPosition = Target.position + offset;
            
            if (smoothFollow)
            {
                newPosition = Vector3.Lerp(transform.position, Target.position + offset,
                    followSpeed * Time.deltaTime);
            }

            transform.position = newPosition;
        }
    }
}