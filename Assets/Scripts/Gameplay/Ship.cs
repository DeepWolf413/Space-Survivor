using DeepWolf.SpaceSurvivor.Data;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [DefaultExecutionOrder(-5)]
    [RequireComponent(typeof(Health), typeof(Shield), typeof(Shooter))]
    [SelectionBase]
    public class Ship : MonoBehaviour
    {
        [SerializeField]
        private ShipData defaultData = null;
        
        [SerializeField]
        private Health healthComponent = null;

        [SerializeField]
        private Shield shieldComponent = null;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;

        private ShipData data = null;
        
        #region Properties

        /// <summary>
        /// Gets the <see cref="ShipData"/>.
        /// </summary>
        public ShipData Data => data;

        #endregion
        
        #region Unity callbacks

        private void OnValidate()
        {
            if (!healthComponent)
            { healthComponent = GetComponent<Health>(); }
            
            if (!shieldComponent)
            { shieldComponent = GetComponent<Shield>(); }
            
            if (!spriteRenderer)
            { spriteRenderer = GetComponent<SpriteRenderer>(); }
        }

        private void Awake() => LoadData(defaultData);

        private void OnEnable()
        {
            healthComponent.ResetValue();
            shieldComponent.ResetValue();
        }

        #endregion

        /// <summary>
        /// Loads the data from the specified <paramref name="shipData"/>. This is not required, only if you want ship data to be loaded from a scriptable object.
        /// </summary>
        public void LoadData(ShipData shipData)
        {
            if (!shipData)
            { return; }
            
            data = shipData;
            spriteRenderer.sprite = shipData.Sprite;

            if (gameObject.CompareTag("Enemy"))
            { healthComponent.SetVitalFactor(GameManager.Instance.SelectedDifficulty.EnemyHealthFactor); }

            if (gameObject.CompareTag("Player"))
            { healthComponent.SetDamageFactor(GameManager.Instance.SelectedDifficulty.EnemyDamageFactor); }
            
            healthComponent.MaxValue = shipData.Health;
            healthComponent.ResetValue();
            
            if (gameObject.CompareTag("Player"))
            { shieldComponent.SetDamageFactor(GameManager.Instance.SelectedDifficulty.EnemyDamageFactor); }
            
            shieldComponent.MaxValue = shipData.Shield;
            shieldComponent.ResetValue();

            if (TryGetComponent(out ShipMovement shipMovement))
            { shipMovement.ThrusterPower = shipData.Speed; }

            shieldComponent.ShieldRegenRate = shipData.ShieldRegenRate;
            shieldComponent.StartShieldRegenDelay = shipData.StartShieldRegenDelay;
        }
        
        #region Damage methods

        /// <summary>
        /// Applies damage to the <see cref="Shield"/> if it is not depleted,
        /// otherwise it applies damage to the <see cref="Health"/> if the damage overflowed the amount of <see cref="Shield"/> the ship has.
        /// If the ship's <see cref="Health"/> is already depleted, then the <see cref="Health"/> is being damaged by the full amount.
        /// </summary>
        /// <param name="amount">The amount of damage to apply.</param>
        public void ApplyDamage(float amount)
        {
            if (shieldComponent.IsDepleted)
            {
                healthComponent.ApplyDamage(amount);
                shieldComponent.RestartRegenerationPreparation();
            }
            else
            { shieldComponent.ApplyDamage(amount); }
        }

        #endregion
    }
}