using DeepWolf.SpaceSurvivor.Gameplay.PowerupSystem;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class PowerupPickup : Pickup
    {
        [Header("[Powerup Pickup]")]
        [SerializeField]
        private Powerup powerupToActivate = null;

        protected override void OnEnable()
        {
            base.OnEnable();
            spriteRenderer.sprite = powerupToActivate.Sprite;
        }

        public override void Take(GameObject playerObject)
        {
            if (playerObject.TryGetComponent(out PowerupsController powerupsController))
            { powerupsController.ActivatePowerup(powerupToActivate); }
            
            base.Take(playerObject);
        }
    }
}