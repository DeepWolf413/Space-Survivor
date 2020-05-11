using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class EnemyShipController : MonoBehaviour
    {
        [SerializeField]
        private EnemyShipMovement movementComponent = null;

        [SerializeField]
        private Shooter shooterComponent = null;

        [SerializeField]
        private Health healthComponent = null;

        private void OnValidate()
        {
            if (!movementComponent)
            { movementComponent = GetComponent<EnemyShipMovement>(); }

            if (!shooterComponent)
            { shooterComponent = GetComponent<Shooter>(); }

            if (!healthComponent)
            { healthComponent = GetComponent<Health>(); }
        }

        private void OnEnable() => healthComponent.OnDeath += OnDeath;
        
        private void OnDisable() => healthComponent.OnDeath -= OnDeath;

        private void Update()
        {
            if (movementComponent.HasTarget)
            { shooterComponent.BeginShooting(); }
            else
            { shooterComponent.StopShooting(); }
        }
        
        private void OnDeath() => GameEvents.SignalEnemyShipDestroyed();
    }
}