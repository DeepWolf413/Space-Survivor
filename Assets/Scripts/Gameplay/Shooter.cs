﻿using UnityEngine;
using Logger = DeepWolf.Logging.Logger;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField]
        private WeaponData startWeapon = null;

        [SerializeField]
        private WeaponBehaviour[] weapons = new WeaponBehaviour[0];

        private WeaponBehaviour currentWeapon = null;

        private bool isShooting = false;
        
        public WeaponData WeaponData => currentWeapon.Data;

        private void OnValidate()
        {
            if (weapons.Length == 0)
            { weapons = GetComponentsInChildren<WeaponBehaviour>(); }
        }

        private void Start()
        {
            if (startWeapon == null)
            { Logger.LogError("No start weapon was specified. Please assign a start weapon."); }
            else
            { ChangeWeapon(startWeapon); }
        }

        public void ChangeWeapon(WeaponData newWeapon)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i].Data != newWeapon)
                { continue; }

                if (currentWeapon != null)
                {
                    currentWeapon.EndUse();
                    currentWeapon.gameObject.SetActive(false);
                }
                
                currentWeapon = weapons[i];
                currentWeapon.gameObject.SetActive(true);

                if (isShooting)
                { currentWeapon.BeginUse(); }
                return;
            }
        }

        public void BeginShoot()
        {
            if (!currentWeapon)
            { return; }
            
            currentWeapon.BeginUse();
            isShooting = true;
        }

        public void EndShoot()
        {
            if (!currentWeapon)
            { return; }
            
            currentWeapon.EndUse();
            isShooting = false;
        }
    }
}