using System;
using DeepWolf.SpaceSurvivor.Gameplay.Feedbacks;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        private float startHealth = 100.0f;

        [SerializeField]
        private float maxHealth = 100.0f;

        [SerializeField]
        private FeedbackPlayer damagedFeedback = null;
        
        [SerializeField]
        private FeedbackPlayer dieFeedback = null;

        private float currentHealth = 100.0f;

        #region Properties

        /// <summary>
        /// Gets or sets the maximum health.
        /// </summary>
        public float MaxHealth
        {
            get => maxHealth;
            set => maxHealth = value;
        }
        
        /// <summary>
        /// Gets or sets(private) the current health.
        /// </summary>
        public float CurrentHealth
        {
            get => currentHealth;
            private set
            {
                if (Mathf.Approximately(value, currentHealth))
                { return; }

                float oldHealth = CurrentHealth;
                
                if (value > MaxHealth)
                { currentHealth = MaxHealth; }
                else if (value < 0)
                { currentHealth = 0; }
                else
                { currentHealth = value; }
                
                HealthChanged?.Invoke(CurrentHealth, oldHealth);
            }
        }

        /// <summary>
        /// Gets a <see cref="bool"/> that indicates whether the entity is dead
        /// </summary>
        public bool IsDead => CurrentHealth <= 0;
        
        #endregion

        #region Events

        /// <summary>
        /// Occurs when the current health has changed.
        /// <para>
        /// arg1: The new health value;
        /// arg2: The old health value;
        /// </para>
        /// </summary>
        public event Action<float, float> HealthChanged;
        
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
        
        #region Unity callbacks

        private void OnEnable()
        {
            currentHealth = startHealth;
        }

        #endregion

        #region Public methods

        public void ApplyDamage(float amount)
        {
            if (IsDead)
            { return; }
            
            float oldHealth = CurrentHealth;
            CurrentHealth -= amount;
            DamageApplied?.Invoke(CurrentHealth, oldHealth - CurrentHealth);

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
            float oldHealth = CurrentHealth;
            CurrentHealth += amount;
            HealApplied?.Invoke(CurrentHealth, CurrentHealth - oldHealth);
        }

        public void ResetHealth() => CurrentHealth = MaxHealth;

        #endregion
        
        #region Private methods

        private void Die()
        {
            if (dieFeedback)
            { dieFeedback.Play(); }

            OnDeath?.Invoke();
            Destroy(gameObject);
        }
        
        #endregion
    }
}