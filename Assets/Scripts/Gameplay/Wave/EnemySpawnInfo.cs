using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class EnemySpawnInfo
    {
        public EnemySpawnInfo(GameObject prefab)
        {
            Prefab = prefab;
            SpawnAmount = 1;
        }
        
        public GameObject Prefab { get; }
        
        public int SpawnAmount { get; private set; }

        public void AddToSpawnAmount() => SpawnAmount++;
        
        public void RemoveFromSpawnAmount() => SpawnAmount--;
    }
}