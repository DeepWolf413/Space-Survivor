using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerShipMovement : MonoBehaviour
    {
        [SerializeField]
        private float thrusterPower = 20.0f;

        [SerializeField]
        private float turnSpeed = 30.0f;

        [SerializeField]
        private Vector2 screenPadding = new Vector2(0.1f, 0.1f);

        private bool moveShip = false;

        private Vector3 screenBounds;
        
        private Rigidbody2D cachedRigidbody = null;
        private Transform cachedTransform = null;

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
        public void StartMove() => moveShip = true;
        
        /// <summary>
        /// Tells the ship to stop moving. This will set <see cref="moveShip"/> to <c>false</c> to make it stop calling the <see cref="Move"/> method from <see cref="FixedUpdate"/>.
        /// </summary>
        public void StopMove() => moveShip = false;

        public void LookTowards(Vector3 direction)
        {
            cachedTransform.rotation = Quaternion.RotateTowards(cachedTransform.rotation, Quaternion.LookRotation(cachedTransform.forward, direction), turnSpeed * Time.deltaTime);
        }

        public void Turn(float xAxis)
        {
            cachedTransform.rotation = Quaternion.RotateTowards(cachedTransform.rotation, Quaternion.Euler(0, 0, cachedTransform.eulerAngles.z + xAxis * turnSpeed), turnSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Moves the ship in the forward direction (local space).
        /// </summary>
        private void Move() => cachedRigidbody.AddForce(cachedTransform.up * thrusterPower);

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