using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class ShipMovement : MonoBehaviour
    {
        [SerializeField]
        protected float thrusterPower = 17.0f;

        protected Vector3 moveInput = Vector3.zero;
        protected bool moveShip = false;
        
        protected float speedFactor = 1.0f;
        
        protected Rigidbody2D cachedRigidbody = null;
        protected Transform cachedTransform = null;

        #region Properties

        /// <summary>
        /// Gets or sets the thruster power.
        /// </summary>
        public float ThrusterPower
        {
            get => thrusterPower * speedFactor;
            set
            {
                if (value < 0)
                { thrusterPower = 0; }
                else
                { thrusterPower = value; }
            }
        }

        #endregion

        protected virtual void Awake()
        {
            cachedRigidbody = GetComponent<Rigidbody2D>();
            cachedTransform = transform;
        }

        protected virtual void FixedUpdate()
        {
            if (moveShip)
            { Move(); }
        }

        /// <summary>
        /// Tells the ship to start moving. This will set <see cref="moveShip"/> to <c>true</c> to make it start calling the <see cref="Move"/> method in <see cref="FixedUpdate"/>.
        /// </summary>
        public virtual void StartMove(float xAxis, float yAxis)
        {
            moveInput.Set(xAxis, yAxis, 0.0f);
            moveInput.Normalize();
            moveShip = true;
        }

        /// <summary>
        /// Tells the ship to stop moving. This will set <see cref="moveShip"/> to <c>false</c> to make it stop calling the <see cref="Move"/> method from <see cref="FixedUpdate"/>.
        /// </summary>
        public virtual void StopMove()
        {
            moveShip = false;
            moveInput.Set(0.0f, 0.0f, 0.0f);
        }

        public virtual void LookTowards(Vector3 direction)
        {
            cachedTransform.rotation = Quaternion.LookRotation(cachedTransform.forward, direction);
            //cachedTransform.rotation = Quaternion.RotateTowards(cachedTransform.rotation, Quaternion.LookRotation(cachedTransform.forward, direction), turnSpeed * Time.deltaTime);
        }
        
        public virtual void SetSpeedFactor(float newFactor) => speedFactor = newFactor;

        /// <summary>
        /// Moves the ship in the forward direction (local space).
        /// </summary>
        protected virtual void Move()
        {
            Vector3 force = new Vector3(moveInput.x, moveInput.y) * ThrusterPower;
            cachedRigidbody.AddForce(force);
        }
    }
}