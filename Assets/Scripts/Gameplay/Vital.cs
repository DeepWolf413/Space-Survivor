using System;
using DeepWolf.SpaceSurvivor.Gameplay.Feedbacks;
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

        private float currentValue = 100.0f;

        #region Properties

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public float MaxValue
        {
            get => maxValue;
            set => maxValue = value;
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

        #endregion
        
        #region Unity callbacks

        protected virtual void OnEnable() => currentValue = startValue;

        #endregion

        #region Public methods

        public virtual void ResetValue() => CurrentValue = MaxValue;

        #endregion
    }
}