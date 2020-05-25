using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class EnemySpawnInfo
    {
        public EnemySpawnInfo(PoolData enemyPool, int pointsRequiredToSpawn)
        {
            EnemyPool = enemyPool;
            SpawnAmount = 1;
            PointsRequiredToSpawn = pointsRequiredToSpawn;
        }
        
        public PoolData EnemyPool { get; }
        
        public int SpawnAmount { get; private set; }
        
        /// <summary>
        /// Gets an <see cref="int"/> that determines the amount of points that is required to spawn this enemy.
        /// </summary>
        public int PointsRequiredToSpawn { get; }

        public void AddToSpawnAmount() => SpawnAmount++;
        
        public void RemoveFromSpawnAmount() => SpawnAmount--;
    }
}