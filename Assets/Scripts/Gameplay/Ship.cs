using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [RequireComponent(typeof(Health), typeof(Shield), typeof(Shooter))]
    [SelectionBase]
    public class Ship : MonoBehaviour
    {
        [SerializeField]
        private Health healthComponent = null;

        [SerializeField]
        private Shield shieldComponent = null;

        #region Unity callbacks

        private void OnValidate()
        {
            if (!healthComponent)
            { healthComponent = GetComponent<Health>(); }
            
            if (!shieldComponent)
            { shieldComponent = GetComponent<Shield>(); }
        }

        #endregion

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
            {
                float damageAmount = shieldComponent.CurrentValue >= amount ? amount : shieldComponent.CurrentValue;
                shieldComponent.ApplyDamage(damageAmount);
            }
        }

        #endregion
    }
}