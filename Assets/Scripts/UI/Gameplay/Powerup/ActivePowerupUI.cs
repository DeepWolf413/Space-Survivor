using System;
using DeepWolf.SpaceSurvivor.Gameplay.PowerupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class ActivePowerupUI : MonoBehaviour
    {
        [SerializeField]
        private Slider timer = null;

        [SerializeField]
        private Image icon = null;

        private ActivePowerup representedPowerup;

        public void Show(ActivePowerup powerup)
        {
            gameObject.SetActive(true);
            representedPowerup = powerup;
            icon.sprite = powerup.Powerup.Sprite;

            InvokeRepeating(nameof(RefreshTimer), 0.1f, 0.3f);
            Invoke(nameof(Hide), powerup.TimeLeft);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            CancelInvoke(nameof(RefreshTimer));
        }

        public bool IsRepresentingPowerup(ActivePowerup powerup) => representedPowerup.Powerup == powerup.Powerup;

        private void RefreshTimer()
        {
            float powerupDuration = representedPowerup.Powerup.Duration;
            timer.value = Mathf.Lerp(1.0f, 0.0f, representedPowerup.TimeLeft / powerupDuration);
        }
    }
}