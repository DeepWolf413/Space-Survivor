using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField]
        private PoolData projectilePool = null;

        [SerializeField]
        private Transform shootPoint = null;

        [SerializeField]
        private float fireRate = 0.1f;

        [SerializeField]
        private AudioClip shootSfx = null;
        
        private bool isShooting = false;
        private float nextShootTime = 0.0f;
        
        public bool IsOnCooldown => nextShootTime > Time.time;

        private void Update()
        {
            if (isShooting && !IsOnCooldown)
            { Shoot(); }
        }

        public void BeginShooting() => isShooting = true;

        public void StopShooting() => isShooting = false;
        
        public void Shoot()
        {
            PoolManager.Spawn(projectilePool, shootPoint.position, shootPoint.rotation);
            nextShootTime = fireRate + Time.time;

            if (shootSfx)
            { GameManager.SoundManager.PlayGlobalSound(shootSfx, ESoundType.Sfx); }
        }
    }
}