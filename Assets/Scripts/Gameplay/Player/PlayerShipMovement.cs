using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerShipMovement : ShipMovement
    {
        [SerializeField]
        private Vector2 screenPadding = new Vector2(0.1f, 0.1f);

        private Vector3 screenBounds;

        protected override void Awake()
        {
            base.Awake();
            UpdateScreenBounds();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            ClampToScreenBounds();
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