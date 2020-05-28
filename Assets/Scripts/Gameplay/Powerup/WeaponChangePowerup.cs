using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.PowerupSystem
{
    [CreateAssetMenu(menuName = "Game/Powerup/New weapon change powerup")]
    public class WeaponChangePowerup : Powerup
    {
        [SerializeField]
        private WeaponData weapon = null;

        private WeaponData previousWeapon = null;
        
        public override void Activate(PowerupsController owner)
        {
            if (!owner.TryGetComponent(out Shooter shooterComponent))
            { return; }
            
            previousWeapon = shooterComponent.WeaponData;
            shooterComponent.ChangeWeapon(weapon);
        }

        public override void Deactivate(PowerupsController owner)
        {
            if (!owner.TryGetComponent(out Shooter shooterComponent))
            { return; }
            
            shooterComponent.ChangeWeapon(previousWeapon);
            previousWeapon = null;
        }
    }
}