using System;
using DeepWolf.SpaceSurvivor.Gameplay.Feedbacks;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace DeepWolf.SpaceSurvivor
{
    public class Vital : MonoBehaviour
    {
        [SerializeField]
        protected float startValue = 100.0f;

        [SerializeField]
        protected float maxValue = 100.0f;

        [SerializeField]
        protected bool resetValueOnEnable = true;

        private float vitalFactor = 1.0f;
        
        private float damageFactor = 1.0f;
        
        private float currentValue = 100.0f;

        #region Properties

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public float MaxValue
        {
            get => maxValue * vitalFactor;
            set
            {
                if (value < 0)
                { maxValue = 0; }
                else
                { maxValue = value; }

                if (CurrentValue > MaxValue)
                { CurrentValue = MaxValue; }
            }
        }
        
        /// <summary>
        /// Gets or sets(private) the current value.
        /// </summary>
        public float CurrentValue
        {
            get => currentValue;
            set
            {
                if (Mathf.Approximately(value, currentValue))
                { return; }

                float oldHealth = CurrentValue;
                
                if (value > MaxValue)
                { currentValue = MaxValue; }
                else if (value < 0)
                { currentValue = 0; }
                else
                { currentValue = value; }
                
                ValueChanged?.Invoke(CurrentValue, oldHealth);
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the current value has changed.
        /// <para>
        /// arg1: The new value;
        /// arg2: The old value;
        /// </para>
        /// </summary>
        public event Action<float, float> ValueChanged;
        
        /// <summary>
        /// Occurs when damage has been applied.
        /// <para>
        /// arg1: The new vital value;
        /// arg2: The amount of damage that was applied;
        /// </para>
        /// </summary>
        public event Action<float, float> DamageApplied;

        #endregion

        #region Unity callbacks

        protected virtual void OnEnable()
        {
            if (resetValueOnEnable)
            { ResetValue(); }
        }

        #endregion
        
        #region Public methods

        public virtual void SetVitalFactor(float newFactor) => vitalFactor = newFactor;
        
        public virtual void SetDamageFactor(float newFactor) => damageFactor = newFactor;

        public virtual void ApplyDamage(float amount)
        {
            float oldValue = CurrentValue;
            CurrentValue -= amount * damageFactor;
            DamageApplied?.Invoke(CurrentValue, oldValue - CurrentValue);
        }
        
        public virtual void ResetValue() => CurrentValue = MaxValue;

        #endregion
    }
}