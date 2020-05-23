using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class EnemySpawnInfo
    {
        public EnemySpawnInfo(GameObject prefab, int pointsRequiredToSpawn)
        {
            Prefab = prefab;
            SpawnAmount = 1;
            PointsRequiredToSpawn = pointsRequiredToSpawn;
        }
        
        public GameObject Prefab { get; }
        
        public int SpawnAmount { get; private set; }
        
        /// <summary>
        /// Gets an <see cref="int"/> that determines the amount of points that is required to spawn this enemy.
        /// </summary>
        public int PointsRequiredToSpawn { get; }

        public void AddToSpawnAmount() => SpawnAmount++;
        
        public void RemoveFromSpawnAmount() => SpawnAmount--;
    }
}