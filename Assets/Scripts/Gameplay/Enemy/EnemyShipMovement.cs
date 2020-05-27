using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class EnemyShipMovement : ShipMovement
    {
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

        public bool HasTarget => target;

        private Vector3 Destination => target.transform.position + followOffset;

        private void Start()
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (!playerObject)
            { return; }
            
            target = playerObject.GetComponent<Rigidbody2D>();
            FindRndOffsetFromTarget();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (!HasTarget)
            {
                if (moveShip)
                { StopMove(); }

                return;
            }

            if (!IsWithinDestinationPos())
            { MoveTowardsTarget(); }
            else
            {
                if (moveShip)
                { StopMove(); }
            }
            
            LookTowards(target.transform.position - transform.position);
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
            StartMove(direction.x, direction.y);
        }

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