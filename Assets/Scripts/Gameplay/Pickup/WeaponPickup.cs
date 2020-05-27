using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class WeaponPickup : Pickup
    {
        [Header("[Weapon Pickup]")]
        [SerializeField]
        private WeaponData weaponToGive = null;

        public override void Take(GameObject playerObject)
        {
            if (playerObject.TryGetComponent(out Shooter shooterComponent))
            {
                shooterComponent.ChangeWeapon(weaponToGive);
            }
            
            base.Take(playerObject);
        }
    }
}