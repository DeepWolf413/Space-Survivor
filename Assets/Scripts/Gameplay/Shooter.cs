using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField]
        private WeaponBehaviour currentWeapon = null;

        public WeaponData WeaponData => currentWeapon.Data;
        
        public void ChangeWeapon(WeaponBehaviour newWeapon) => currentWeapon = newWeapon;

        public void BeginShoot()
        {
            if (!currentWeapon)
            { return; }
            
            currentWeapon.BeginUse();
        }

        public void EndShoot()
        {
            if (!currentWeapon)
            { return; }
            
            currentWeapon.EndUse();
        }
    }
}