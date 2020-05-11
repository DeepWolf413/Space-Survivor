using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class EnemyShipMovement : MonoBehaviour
    {
        [SerializeField]
        private float thrusterPower = 10.0f;

        [SerializeField]
        private bool predictTargetVelocity = false;

        private Rigidbody2D target = null;

        private Rigidbody2D cachedRigidbody = null;

        public bool HasTarget => target;

        private void Awake() => cachedRigidbody = GetComponent<Rigidbody2D>();

        private void Start()
        { target = GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>(); }

        private void FixedUpdate()
        {
            if (!HasTarget)
            { return; }
            
            MoveTowardsTarget();
            LookTowardsMovingDirection();
        }

        private void MoveTowardsTarget()
        {
            Vector3 direction = target.transform.position - transform.position;
            
            if (predictTargetVelocity)
            {
                Vector3 targetVelocity = target.velocity;
                direction = (target.transform.position + targetVelocity) - transform.position;
            }

            cachedRigidbody.AddForce(direction.normalized * thrusterPower);
        }

        private void LookTowardsMovingDirection() => transform.rotation = Quaternion.LookRotation(transform.forward, cachedRigidbody.velocity);
    }
}