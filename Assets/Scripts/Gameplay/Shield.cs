using System;
using DeepWolf.SpaceSurvivor.Gameplay.Feedbacks;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    /// <summary>
    /// Represents the <see cref="Shield"/> <see cref="Vital"/>.
    /// </summary>
    public class Shield : Vital
    {
        [SerializeField]
        private FeedbackPlayer damagedFeedback = null;
        
        [SerializeField]
        private FeedbackPlayer depletedFeedback = null;

        #region Properties

        /// <summary>
        /// Gets a <see cref="bool"/> that indicates whether the <see cref="Shield"/> has depleted.
        /// </summary>
        public bool IsDepleted => CurrentValue <= 0;

        #endregion
        
        #region Events

        /// <summary>
        /// Occurs when damage has been applied.
        /// <para>
        /// arg1: The new shield value;
        /// arg2: The amount of damage that was applied;
        /// </para>
        /// </summary>
        public event Action<float, float> DamageApplied;

        /// <summary>
        /// Occurs when the shield has depleted.
        /// </summary>
        public event Action OnDepleted;
        
        #endregion

        public void ApplyDamage(float amount)
        {
            if (IsDepleted)
            { return; }

            float oldValue = CurrentValue;
            CurrentValue -= amount;
            DamageApplied?.Invoke(CurrentValue, oldValue);

            if (IsDepleted)
            {
                if (depletedFeedback)
                { depletedFeedback.Play(); }
                OnDepleted?.Invoke();
            }
            else
            {
                if (damagedFeedback)
                { damagedFeedback.Play(); }
            }
        }
    }
}