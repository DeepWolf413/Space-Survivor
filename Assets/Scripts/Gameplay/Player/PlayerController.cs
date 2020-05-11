using System;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private string moveHorizontalName = "Horizontal";

        [SerializeField]
        private string moveVerticalName = "Vertical";

        [SerializeField]
        private string shootName = "Shoot";

        [SerializeField]
        private PlayerShipMovement movementComponent = null;

        [SerializeField]
        private Shooter shooterComponent = null;

        private Camera cachedCamera = null;

        /// <summary>
        /// Gets or sets a <see cref="bool"/> that determines whether to process input or not.
        /// </summary>
        public bool DetectInput { get; set; } = true;

        private void OnValidate()
        {
            if (!movementComponent)
            { movementComponent = GetComponent<PlayerShipMovement>(); }
            
            if (!shooterComponent)
            { shooterComponent = GetComponent<Shooter>(); }
        }

        private void Awake() => cachedCamera = Camera.main;

        private void Update()
        {
            if (!DetectInput)
            { return; }
            
            ProcessInput();
        }

        private void ProcessInput()
        {
            if (movementComponent)
            { ProcessMovementInput(); }

            if (shooterComponent)
            { ProcessShootInput(); }
        }

        private void ProcessMovementInput()
        {
            if (Input.GetAxisRaw(moveVerticalName) > 0.0f)
            { movementComponent.StartMove(); }
            else
            { movementComponent.StopMove(); }

            movementComponent.LookTowards(GetDirectionToMouse());
            //movementComponent.Turn(Input.GetAxisRaw(moveHorizontalName));
        }

        private void ProcessShootInput()
        {
            if (Input.GetButtonDown(shootName))
            { shooterComponent.BeginShooting(); }
            
            if (Input.GetButtonUp(shootName))
            { shooterComponent.StopShooting(); }
        }

        private Vector3 GetDirectionToMouse() => cachedCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
}