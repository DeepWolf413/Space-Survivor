using DeepWolf.SpaceSurvivor.Data;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInputModule inputModule = new PlayerInputModule();

        [Header("[Cached Components]")]
        [SerializeField]
        private PlayerShipMovement movementComponent = null;

        [SerializeField]
        private Ship shipComponent = null;
        
        [SerializeField]
        private Health shipHealthComponent = null;

        #region Unity callbacks

        private void OnValidate()
        {
            if (!shipComponent)
            { shipComponent = GetComponent<Ship>(); }

            if (!shipHealthComponent)
            { shipHealthComponent = GetComponent<Health>(); }
        }

        private void Awake() => inputModule.Initialize(movementComponent, GetComponentInChildren<Shooter>());

        private void Start() => shipComponent.LoadData(GameManager.SaveManager.GetSelectedShip());

        private void OnEnable() => shipHealthComponent.OnDeath += OnShipDead;
        
        private void OnDisable() => shipHealthComponent.OnDeath -= OnShipDead;

        private void Update() => inputModule.ProcessInput();

        #endregion

        #region Event listeners
        
        private void OnShipDead() => GameEvents.SignalPlayerShipDestroyed();
        
        #endregion
    }
}