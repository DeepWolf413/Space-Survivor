using DeepWolf.SpaceSurvivor.Gameplay;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Logger = DeepWolf.Logging.Logger;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class ShipVitalsUI : MonoBehaviour
    {
        [SerializeField]
        private Slider healthBar = null;
        
        [SerializeField]
        private Slider shieldBar = null;

        private Health healthComponent = null;
        
        private Shield shieldComponent = null;
        
        [SerializeField]
        private CanvasGroup cachedCanvasGroup = null;

        #region Unity callbacks

        private void Awake()
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (!playerObject)
            {
                Logger.LogError("Could not find player object.");
                return;
            }

            if (!playerObject.TryGetComponent(out healthComponent))
            { Logger.LogError("Could not find Health component on player object."); }
            
            if (!playerObject.TryGetComponent(out shieldComponent))
            { Logger.LogError("Could not find Shield component on player object."); }
        }

        private void OnEnable()
        {
            if (healthComponent)
            { healthComponent.ValueChanged += OnHealthChanged; }

            if (shieldComponent)
            {
                shieldComponent.ValueChanged += OnShieldChanged;
            }
        }

        private void OnDisable()
        {
            if (healthComponent)
            { healthComponent.ValueChanged -= OnHealthChanged; }
        }

        private void Start()
        {
            RefreshHealth();
            RefreshShield();
            
            if (TryGetComponent(out UIWorldObjectFollower objectFollowerComponent))
            { objectFollowerComponent.Target = GameObject.FindWithTag("Player").transform; }
        }

        #endregion

        /// <summary>
        /// Slightly fades the vitals when the health and shield is at max value. Fade to solid when either health or shield is missing.
        /// </summary>
        private void HandleFading()
        {
            DOTween.Complete(cachedCanvasGroup);
            
            // Whether the health or shield is below max value.
            bool isBelowMaxValue = healthComponent.CurrentValue < healthComponent.MaxValue || shieldComponent.CurrentValue < shieldComponent.MaxValue;
            cachedCanvasGroup.DOFade(isBelowMaxValue ? 1.0f : 0.3f, 0.3f);
        }

        private void RefreshHealth()
        {
            healthBar.value = healthComponent.CurrentValue / healthComponent.MaxValue;
            HandleFading();
        }
        

        private void RefreshShield()
        {
            shieldBar.value = shieldComponent.CurrentValue / shieldComponent.MaxValue;
            HandleFading();
        }
        
        #region Event listeners

        private void OnHealthChanged(float current, float old)
        {
            RefreshHealth();

            DOTween.Complete(healthBar.transform);
            healthBar.transform.localScale = Vector3.one;
            healthBar.transform.DOPunchScale(new Vector3(0.2f, 0.2f), 0.3f);
        }
        
        private void OnShieldChanged(float current, float old)
        {
            RefreshShield();
            
            DOTween.Complete(shieldBar.transform);
            shieldBar.transform.localScale = Vector3.one;
            shieldBar.transform.DOPunchScale(new Vector3(0.2f, 0.2f), 0.3f);
        }

        #endregion
    }
}