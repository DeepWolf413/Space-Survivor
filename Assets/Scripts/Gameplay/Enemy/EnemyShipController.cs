using System.Collections;
using DeepWolf.SpaceSurvivor.Data;
using DeepWolf.SpaceSurvivor.Enums;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class EnemyShipController : MonoBehaviour
    {
        [SerializeField]
        private Ship shipComponent = null;

        [SerializeField]
        private EnemyShipMovement movementComponent = null;

        [SerializeField]
        private Shooter shooterComponent = null;

        [SerializeField]
        private Health healthComponent = null;

        private Coroutine shootCoroutine = null;

        private void OnValidate()
        {
            if (!shipComponent)
            { shipComponent = GetComponent<Ship>(); }
            
            if (!movementComponent)
            { movementComponent = GetComponent<EnemyShipMovement>(); }

            if (!shooterComponent)
            { shooterComponent = GetComponent<Shooter>(); }

            if (!healthComponent)
            { healthComponent = GetComponent<Health>(); }
        }

        private void OnEnable()
        {
            SetRndSpriteVariation();
            healthComponent.OnDeath += OnDeath;

            EnemyShipData shipData = (EnemyShipData) shipComponent.Data;
            switch (shipData.ShootPattern)
            {
                case EShootPattern.Continuous:
                    shooterComponent.BeginShoot();
                    break;
                case EShootPattern.Burst:
                    shootCoroutine = StartCoroutine(ShootBurst());
                    break;
                case EShootPattern.Single:
                    shootCoroutine = StartCoroutine(ShootSingle());
                    break;
            }
        }
        
        private void OnDisable() => healthComponent.OnDeath -= OnDeath;

        private void Update()
        {
            if (movementComponent.HasTarget)
            { return; }
            
            shooterComponent.EndShoot();
            if (shootCoroutine != null)
            { StopCoroutine(shootCoroutine); }
        }

        private void SetRndSpriteVariation()
        {
            if (!TryGetComponent(out SpriteRenderer spriteRenderer))
            { return; }
            
            EnemyShipData shipData = (EnemyShipData)shipComponent.Data;
            spriteRenderer.sprite = shipData.GetRndSpriteVariation();
        }
        
        #region Shooting methods

        private IEnumerator ShootBurst()
        {
            EnemyShipData shipData = (EnemyShipData)shipComponent.Data;
            
            yield return new WaitForSeconds(shipData.ShootDelay);
            shooterComponent.BeginShoot();
            yield return new WaitForSeconds(shooterComponent.WeaponData.UseRate * shipData.BurstAmount);
            shooterComponent.EndShoot();
            shootCoroutine = StartCoroutine(ShootBurst());
        }
        
        private IEnumerator ShootSingle()
        {
            EnemyShipData shipData = (EnemyShipData)shipComponent.Data;
            
            yield return new WaitForSeconds(shipData.ShootDelay);
            shooterComponent.BeginShoot();
            yield return new WaitForSeconds(shooterComponent.WeaponData.UseRate);
            shooterComponent.EndShoot();
            shootCoroutine = StartCoroutine(ShootSingle());
        }

        #endregion

        #region Event listeners

        private void OnDeath()
        {
            GameEvents.SignalEnemyShipDestroyed();

            if (shootCoroutine != null)
            { StopCoroutine(shootCoroutine); }
            
            shooterComponent.EndShoot();
        }

        #endregion
    }
}