using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class ProjectileLauncherModule : WeaponModule
    {
        [SerializeField]
        private PoolData projectilePool = null;

        [SerializeField]
        private Transform[] shootPoints = new Transform[0];

        #region Overridden methods

        public override void OnUse()
        {
            for (int i = 0; i < shootPoints.Length; i++)
            { PoolManager.Spawn(projectilePool, shootPoints[i].position, shootPoints[i].rotation); }
        }

        #endregion
    }
}