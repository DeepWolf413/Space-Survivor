using System;
using DeepWolf.SpaceSurvivor.Gameplay.Feedbacks;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    /// <summary>
    /// Represents the <see cref="Health"/> <see cref="Vital"/>.
    /// </summary>
    public class Health : Vital
    {
        [SerializeField]
        private bool usePooling = false;

        [SerializeField]
        private FeedbackPlayer damagedFeedback = null;
        
        [SerializeField]
        private FeedbackPlayer dieFeedback = null;

        #region Properties

        /// <summary>
        /// Gets a <see cref="bool"/> that indicates whether the entity is dead
        /// </summary>
        public bool IsDead => CurrentValue <= 0;
        
        #endregion

        #region Events

        /// <summary>
        /// Occurs when damage has been applied.
        /// <para>
        /// arg1: The new health value;
        /// arg2: The amount of damage that was applied;
        /// </para>
        /// </summary>
        public event Action<float, float> DamageApplied;

        /// <summary>
        /// Occurs when healing has been applied.
        /// <para>
        /// arg1: The new health value;
        /// arg2: The amount of healing that was applied;
        /// </para>
        /// </summary>
        public event Action<float, float> HealApplied;
        
        /// <summary>
        /// Occurs when the entity dies.
        /// </summary>
        public event Action OnDeath;
        
        #endregion
        
        #region Public methods

        public void ApplyDamage(float amount)
        {
            if (IsDead)
            { return; }
            
            float oldHealth = CurrentValue;
            CurrentValue -= amount;
            DamageApplied?.Invoke(CurrentValue, oldHealth - CurrentValue);

            if (IsDead)
            { Die(); }
            else
            {
                if (damagedFeedback)
                { damagedFeedback.Play(); }
            }
        }

        public void Heal(float amount)
        {
            float oldHealth = CurrentValue;
            CurrentValue += amount;
            HealApplied?.Invoke(CurrentValue, CurrentValue - oldHealth);
        }

        #endregion
        
        #region Private methods

        private void Die()
        {
            if (dieFeedback)
            { dieFeedback.Play(); }

            OnDeath?.Invoke();

            if (usePooling)
            { PoolManager.Despawn(gameObject); }
            else
            { Destroy(gameObject); }
        }
        
        #endregion
    }
}