using UnityEngine;
using UnityEngine.Serialization;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerShipMovement : MonoBehaviour
    {
        [FormerlySerializedAs("mainThrusterPower")]
        [SerializeField]
        private float thrusterPower = 20.0f;

        [SerializeField]
        private float turnSpeed = 500.0f;

        [SerializeField]
        private Vector2 screenPadding = new Vector2(0.1f, 0.1f);

        private Vector3 moveInput = Vector3.zero;
        private bool moveShip = false;

        private Vector3 screenBounds;
        
        private Rigidbody2D cachedRigidbody = null;
        private Transform cachedTransform = null;

        #region Properties

        /// <summary>
        /// Gets or sets the thruster power.
        /// </summary>
        public float ThrusterPower
        {
            get => thrusterPower;
            set
            {
                if (value < 0)
                { thrusterPower = 0; }
                else
                { thrusterPower = value; }
            }
        }

        #endregion

        private void Awake()
        {
            cachedRigidbody = GetComponent<Rigidbody2D>();
            cachedTransform = transform;
            UpdateScreenBounds();
        }

        private void FixedUpdate()
        {
            if (moveShip)
            { Move(); }
            
            ClampToScreenBounds();
        }

        /// <summary>
        /// Tells the ship to start moving. This will set <see cref="moveShip"/> to <c>true</c> to make it start calling the <see cref="Move"/> method in <see cref="FixedUpdate"/>.
        /// </summary>
        public void StartMove(float xAxis, float yAxis)
        {
            moveInput.Set(xAxis, yAxis, 0.0f);
            moveInput.Normalize();
            moveShip = true;
        }

        /// <summary>
        /// Tells the ship to stop moving. This will set <see cref="moveShip"/> to <c>false</c> to make it stop calling the <see cref="Move"/> method from <see cref="FixedUpdate"/>.
        /// </summary>
        public void StopMove()
        {
            moveShip = false;
            moveInput.Set(0.0f, 0.0f, 0.0f);
        }

        public void LookTowards(Vector3 direction)
        {
            cachedTransform.rotation = Quaternion.RotateTowards(cachedTransform.rotation, Quaternion.LookRotation(cachedTransform.forward, direction), turnSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Moves the ship in the forward direction (local space).
        /// </summary>
        private void Move()
        {
            Vector3 force = new Vector3(moveInput.x, moveInput.y) * ThrusterPower;
            cachedRigidbody.AddForce(force);
        }

        private void ClampToScreenBounds()
        {
            if (cachedTransform.position.x < screenBounds.x && cachedTransform.position.y < screenBounds.y && cachedTransform.position.x > -screenBounds.x && cachedTransform.position.y > -screenBounds.y)
            { return; }
            
            float xClamped = Mathf.Clamp(cachedTransform.position.x, -screenBounds.x, screenBounds.x);
            float yClamped = Mathf.Clamp(cachedTransform.position.y, -screenBounds.y, screenBounds.y);
            cachedTransform.position = new Vector2(xClamped, yClamped);
        }

        private void UpdateScreenBounds()
        {
            Camera cachedCamera = Camera.main;
            screenBounds = cachedCamera.ViewportToWorldPoint(new Vector3(1.0f - screenPadding.x, 1.0f - screenPadding.y));
        }
    }
}