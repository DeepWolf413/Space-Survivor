using System.Collections;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class EnemyShipController : MonoBehaviour
    {
        public enum EShootPattern
        {
            Continuous,
            Burst,
            Single
        }
        
        [SerializeField]
        private EnemyShipMovement movementComponent = null;

        [SerializeField]
        private Shooter shooterComponent = null;

        [SerializeField]
        private Health healthComponent = null;

        [Header("[Shooting Pattern]")]
        [SerializeField]
        private EShootPattern shootPattern = EShootPattern.Single;
        
        [SerializeField]
        private float shootDelay = 2.0f;

        [Header("[Shooting Pattern - Burst]")]
        [SerializeField]
        private int burstAmount = 3;

        private Coroutine shootCoroutine = null;
        
        [Header("[Sprite Variation]")]
        [SerializeField]
        private Sprite[] spriteVariations = new Sprite[0];

        private void OnValidate()
        {
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

            switch (shootPattern)
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
            if (!movementComponent.HasTarget)
            {
                shooterComponent.EndShoot();
                
                if (shootCoroutine != null)
                { StopCoroutine(shootCoroutine); }
            }
        }

        private void SetRndSpriteVariation()
        {
            if (TryGetComponent(out SpriteRenderer spriteRenderer))
            { spriteRenderer.sprite = spriteVariations[Random.Range(0, spriteVariations.Length)]; }
        }
        
        #region Shooting methods

        private IEnumerator ShootBurst()
        {
            yield return new WaitForSeconds(shootDelay);
            shooterComponent.BeginShoot();
            yield return new WaitForSeconds(shooterComponent.WeaponData.UseRate * burstAmount);
            shooterComponent.EndShoot();
            shootCoroutine = StartCoroutine(ShootBurst());
        }
        
        private IEnumerator ShootSingle()
        {
            yield return new WaitForSeconds(shootDelay);
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