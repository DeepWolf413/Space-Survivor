using UnityEngine;
using Logger = DeepWolf.Logging.Logger;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class Wave
    {
        private EnemySpawnInfo[] enemiesToSpawn;
        
        public Wave(int number, EnemySpawnInfo[] enemiesToSpawn)
        {
            Number = number;
            this.enemiesToSpawn = enemiesToSpawn;
        }
        
        public int Number { get; }

        /// <summary>
        /// Gets the count of the <see cref="enemiesToSpawn"/> array.
        /// NOTE: This does not mean the actual total spawn count, but only the count of the enemy types that will spawn.
        /// To get the total spawn count, use the method <see cref="GetEnemySpawnCount"/> instead.
        /// </summary>
        public int EnemyCount => enemiesToSpawn.Length;
        
        public EnemySpawnInfo GetEnemy(int index)
        {
            if (index < 0 || index >= EnemyCount)
            {
                Logger.LogError($"The index ({index}) was out of bounds of the 'enemiesToSpawn' array.");
                return null;
            }

            return enemiesToSpawn[index];
        }

        public int GetEnemySpawnCount()
        {
            int count = 0;
            for (int i = 0; i < enemiesToSpawn.Length; i++)
            {
                count += enemiesToSpawn[i].SpawnAmount;
            }

            return count;
        }
    }
}