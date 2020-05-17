using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class EnemyShipMovement : MonoBehaviour
    {
        [SerializeField]
        private float thrusterPower = 10.0f;

        [SerializeField]
        private Vector2 powerRange = new Vector2(10.0f, 20.0f);

        [SerializeField]
        private bool predictTargetVelocity = false;

        [SerializeField]
        private Vector2 offsetDistanceRange = new Vector2(3.0f, 6.0f);

        [SerializeField]
        private float reachThreshold = 0.3f;

        private Vector3 followOffset = Vector3.zero;

        private Rigidbody2D target = null;
        private Rigidbody2D cachedRigidbody = null;

        public bool HasTarget => target;

        private Vector3 Destination => target.transform.position + followOffset;

        private void Awake() => cachedRigidbody = GetComponent<Rigidbody2D>();

        private void Start()
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (!playerObject)
            { return; }
            
            target = playerObject.GetComponent<Rigidbody2D>();
            FindRndOffsetFromTarget();
        }

        private void FixedUpdate()
        {
            if (!HasTarget)
            { return; }

            if (!IsWithinDestinationPos())
            { MoveTowardsTarget(); }
            
            LookTowardsTarget();
        }

        private void MoveTowardsTarget()
        {
            Vector3 direction = Destination - transform.position;
            
            #region Target velocity prediction

            if (predictTargetVelocity)
            {
                Vector3 targetVelocity = target.velocity;
                direction = (Destination + targetVelocity) - transform.position;
            }

            #endregion

            /*float maxDistance = 5.0f;
            float minDistance = reachThreshold;
            float distance = Vector3.SqrMagnitude(Destination - transform.position);*/
            //float power = Mathf.Lerp(powerRange.x, powerRange.y, distance / maxDistance)
            cachedRigidbody.AddForce(direction.normalized * thrusterPower);
        }

        private void LookTowardsTarget() => transform.rotation = Quaternion.LookRotation(transform.forward, target.transform.position - transform.position);

        private bool IsWithinDestinationPos() => Vector3.SqrMagnitude(Destination - transform.position) <= reachThreshold * reachThreshold;
        
        private void FindRndOffsetFromTarget()
        {
            do
            {
                float rndRadius = Random.Range(offsetDistanceRange.x, offsetDistanceRange.y);
                followOffset = Random.insideUnitCircle * rndRadius;
            } while (followOffset.sqrMagnitude < offsetDistanceRange.x * offsetDistanceRange.x);
        }

        private void OnDrawGizmosSelected()
        {
            if (HasTarget)
            { Gizmos.DrawWireSphere(Destination, 1.0f); }
        }
    }
}