using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class ProjectileLauncherModule : WeaponModule
    {
        [SerializeField]
        private GameObject projectilePrefab = null;

        #region Overridden methods
        
        public override void OnUse()
        {
            //Instantiate();
        }

        #endregion
    }
}