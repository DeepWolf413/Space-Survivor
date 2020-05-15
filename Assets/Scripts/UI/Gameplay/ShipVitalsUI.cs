using DeepWolf.SpaceSurvivor.Gameplay;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class ShipVitalsUI : MonoBehaviour
    {
        [SerializeField]
        private Slider healthBar = null;
        
        [SerializeField]
        private Slider shieldBar = null;

        private Health healthComponent = null;
        
        [SerializeField]
        private CanvasGroup cachedCanvasGroup = null;

        #region Unity callbacks

        private void Awake()
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (!playerObject)
            {
                Debug.LogError("Could not find player object.");
                return;
            }

            if (!playerObject.TryGetComponent(out healthComponent))
            { Debug.LogError("Could not find Health component on player object."); }
            
            /* TODO: Implement shield component.
             if (!playerObject.TryGetComponent(out shieldComponent))
            { Debug.LogError("Could not find Shield component on player object."); }
             */
        }

        private void OnEnable()
        {
            if (healthComponent)
            { healthComponent.HealthChanged += OnHealthChanged; }
        }
        
        private void OnDisable()
        {
            if (healthComponent)
            { healthComponent.HealthChanged -= OnHealthChanged; }
        }

        private void Start()
        {
            RefreshHealth();
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
            
            // Whether the health is below max value.
            bool isBelowMaxValue = healthComponent.CurrentHealth < healthComponent.MaxHealth;
            cachedCanvasGroup.DOFade(isBelowMaxValue ? 1.0f : 0.3f, 0.3f);
        }

        private void RefreshHealth()
        {
            healthBar.value = healthComponent.CurrentHealth / healthComponent.MaxHealth;
            HandleFading();
        }
        

        // TODO: Uncomment when shield is implemented.
        //private void RefreshShield() => shieldLabel.text = shieldComponent.CurrentShield.ToString();
        
        #region Event listeners

        private void OnHealthChanged(float newHealth, float oldHealth)
        {
            RefreshHealth();
            healthBar.transform.DOPunchScale(new Vector3(0.2f, 0.2f), 0.3f);
        }

        #endregion
    }
}