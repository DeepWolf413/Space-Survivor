using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    /// <summary>
    /// Represents a ship's vitals (health, and shield).
    /// This is responsible for controlling a ship's vitals.
    /// <para>
    /// As an example, if we say the ship has 50 shield and 100 health, then this script makes sure that only the shield is applied damage to
    /// until the shield is depleted, after that the health is the one that will be applied damage to.
    /// You could say it acts as a middle-man for the ship's actual vital components.
    /// </para>
    /// </summary>
    [RequireComponent(typeof(Health), typeof(Shield))]
    public class ShipVitalsController : MonoBehaviour
    {
        [SerializeField]
        private Health healthComponent = null;

        [SerializeField]
        private Shield shieldComponent = null;

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

                // Apply the rest of the damage to the health if there's any leftover.
                if (shieldComponent.CurrentValue < amount)
                { healthComponent.ApplyDamage(amount - shieldComponent.CurrentValue); }
            }
        }
    }
}