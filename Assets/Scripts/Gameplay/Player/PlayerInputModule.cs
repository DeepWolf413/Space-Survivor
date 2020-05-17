using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [System.Serializable]
    public class PlayerInputModule
    {
        [SerializeField]
        private string moveHorizontalName = "Horizontal";

        [SerializeField]
        private string moveVerticalName = "Vertical";
        
        [SerializeField]
        private string shootName = "Shoot";
        
        private PlayerShipMovement movementComponent = null;
        private Shooter shooterComponent = null;
        private Camera cachedCamera = null;

        /// <summary>
        /// Gets or sets a <see cref="bool"/> that determines whether to process input or not.
        /// </summary>
        public bool DetectInput { get; set; } = true;
        
        public void Initialize(PlayerShipMovement movementComponent, Shooter shooterComponent)
        {
            this.movementComponent = movementComponent;
            this.shooterComponent = shooterComponent;
            cachedCamera = Camera.main;
        }

        public void ProcessInput()
        {
            if (!DetectInput)
            { return; }
            
            if (movementComponent)
            { ProcessMovementInput(); }

            if (shooterComponent)
            { ProcessShootInput(); }
        }

        private void ProcessMovementInput()
        {
            float xAxis = Input.GetAxisRaw(moveHorizontalName);
            float yAxis = Input.GetAxisRaw(moveVerticalName);
            
            if (!Mathf.Approximately(xAxis, 0.0f) || !Mathf.Approximately(yAxis, 0.0f))
            { movementComponent.StartMove(xAxis, yAxis); }
            else
            { movementComponent.StopMove(); }

            movementComponent.LookTowards(GetDirectionToMouse());
        }

        private void ProcessShootInput()
        {
            if (Input.GetButtonDown(shootName))
            { shooterComponent.BeginShooting(); }
            
            if (Input.GetButtonUp(shootName))
            { shooterComponent.StopShooting(); }
        }

        private Vector3 GetDirectionToMouse() => cachedCamera.ScreenToWorldPoint(Input.mousePosition) - movementComponent.transform.position;
    }
}