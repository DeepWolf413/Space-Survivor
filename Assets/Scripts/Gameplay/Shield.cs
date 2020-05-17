using System;
using DeepWolf.SpaceSurvivor.Gameplay.Feedbacks;
using DG.Tweening;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    /// <summary>
    /// Represents the <see cref="Shield"/> <see cref="Vital"/>.
    /// </summary>
    public class Shield : Vital
    {
        [SerializeField]
        private GameObject shieldFx = null;
        
        [SerializeField]
        private FeedbackPlayer damagedFeedback = null;
        
        [SerializeField]
        private FeedbackPlayer depletedFeedback = null;
        
        [SerializeField]
        private FeedbackPlayer shieldRestoredFeedback = null;

        private bool isRegenerating = false;
        
        #region Properties

        /// <summary>
        /// Gets a <see cref="bool"/> that indicates whether the <see cref="Shield"/> has depleted.
        /// </summary>
        public bool IsDepleted => CurrentValue <= 0;
        
        public float StartShieldRegenDelay { get; set; }

        public float ShieldRegenRate { get; set; }

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
        public event Action Depleted;
        
        #endregion

        #region Unity callbacks

        private void OnDestroy()
        {
            if (shieldFx)
            { DOTween.Kill(shieldFx.transform); }
        }

        private void Start()
        {
            if (!shieldFx)
            { return; }
            
            if (CurrentValue <= 0)
            { shieldFx.SetActive(false); }
        }

        private void Update()
        {
            if (isRegenerating)
            { Regenerate(); }
        }

        #endregion
        
        public void ApplyDamage(float amount)
        {
            if (IsDepleted)
            { return; }

            float oldValue = CurrentValue;
            CurrentValue -= amount;
            DamageApplied?.Invoke(CurrentValue, oldValue);
            
            RestartRegenerationPreparation();

            if (IsDepleted)
            {
                Depleted?.Invoke();
                
                if (depletedFeedback)
                { depletedFeedback.Play(); }

                if (shieldFx)
                {
                    shieldFx.transform.DOScale(new Vector3(0.0f, 0.0f, 1.0f), 0.45f).onComplete += delegate
                    { shieldFx.SetActive(false); };
                }
            }
            else
            {
                if (damagedFeedback)
                { damagedFeedback.Play(); }
            }
        }

        #region Shield regeneration methods
        
        /// <summary>
        /// Restarts the regeneration preparation by canceling invoke for <see cref="StartRegeneration"/>, and stops regeneration.
        /// </summary>
        public void RestartRegenerationPreparation()
        {
            CancelInvoke(nameof(StartRegeneration));

            if (isRegenerating)
            { StopRegeneration(); }

            if (CurrentValue < MaxValue)
            { PrepareRegeneration(); }
        }

        /// <summary>
        /// Starts invoking a call to <see cref="StartRegeneration"/>, so that it will start regenerating after the <see cref="StartShieldRegenDelay"/>.
        /// </summary>
        private void PrepareRegeneration() => Invoke(nameof(StartRegeneration), StartShieldRegenDelay);

        private void StartRegeneration()
        {
            isRegenerating = true;

            if (!IsDepleted)
            { return; }
            
            if (shieldRestoredFeedback)
            { shieldRestoredFeedback.Play(); }

            if (shieldFx)
            {
                shieldFx.SetActive(true);
                shieldFx.transform.DOScale(new Vector3(1.0f, 1.0f, 1.0f), 0.45f);
            }
        }

        private void Regenerate()
        {
            CurrentValue += ShieldRegenRate * Time.deltaTime;
            if (CurrentValue >= MaxValue)
            { StopRegeneration(); }
        }

        private void StopRegeneration() => isRegenerating = false;

        #endregion
    }
}