using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [CreateAssetMenu(menuName = "Game/Gameplay/New wave generation config")]
    public class WaveGenerationConfig : ScriptableObject
    {
        [SerializeField, Tooltip("The seed for generating waves.")]
        private int seed = 4814833;

        [SerializeField]
        private EnemyWaveInfo[] enemies = new EnemyWaveInfo[0];

        [SerializeField, Tooltip("The amount to increase points with after each wave. These points are used to spawn enemies.")]
        private int pointsPerWave = 5;

        [SerializeField, Tooltip("The starting points used for spawning enemies in waves.")]
        private int startingPoints = 5;

        [SerializeField, Tooltip("The maximum amount of points that a wave can have.")]
        private int maxPoints = 25;

        [SerializeField]
        private Vector2 enemySpawnDelayRange = new Vector2(1.2f, 3.0f);

        [FormerlySerializedAs("spawnAsteroidsDelayRange")]
        [SerializeField]
        private Vector2 asteroidsEventSpawnDelayRange = new Vector2(45.0f, 120.0f);

        [SerializeField]
        private float asteroidSpawnDelay = 1.5f;

        [SerializeField]
        private Vector2Int asteroidSpawnCountRange = new Vector2Int(10, 15);

        #region Properties

        /// <summary>
        /// Gets the seed for generating waves.
        /// </summary>
        public int Seed => seed;

        /// <summary>
        /// Gets the amount of points that is added per wave.
        /// </summary>
        public int PointsPerWave => pointsPerWave;

        /// <summary>
        /// Gets the starting points used for spawning enemies in waves.
        /// </summary>
        public int StartingPoints => startingPoints;

        /// <summary>
        /// Gets the maximum amount of points that a wave can have.
        /// </summary>
        public int MaxPoints => maxPoints;

        /// <summary>
        /// Gets the asteroid spawn delay.
        /// </summary>
        public float AsteroidSpawnDelay => asteroidSpawnDelay;

        #endregion

        public float GetRndEnemySpawnDelay() => Random.Range(enemySpawnDelayRange.x, enemySpawnDelayRange.y);
        
        public int GetRndAsteroidSpawnCount() => Random.Range(asteroidSpawnCountRange.x, asteroidSpawnCountRange.y);
        
        public float GetRndAsteroidEventSpawnDelay() => Random.Range(asteroidsEventSpawnDelayRange.x, asteroidsEventSpawnDelayRange.y);

        private EnemyWaveInfo GetEnemyBasedOnPoints(int points, int waveNumber)
        {
            List<EnemyWaveInfo> possibleEnemies = new List<EnemyWaveInfo>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].PointsRequiredToSpawn <= points)
                { possibleEnemies.Add(enemies[i]); }
            }

            if (possibleEnemies.Count > 0)
            {
                return enemies[Random.Range(0, possibleEnemies.Count)];
            }

            Debug.LogError($"Didn't find an enemy for the points ({points}).");
            return null;
        }

        /*private EnemySpawnInfo[] GetEnemiesForWave(int waveNumber, int points)
        {
            List<EnemySpawnInfo> enemiesToSpawn = new List<EnemySpawnInfo>();
            while (points > 0)
            {
                if (enemies[i].IntroduceAtWave == waveNumber)
                {
                    possibleEnemies.Clear();
                    possibleEnemies.Add(enemies[i]);
                    break;
                }
            }
        }*/
        
        /// <summary>
        /// Generates a <see cref="Wave"/> based on the config from <see cref="generationConfig"/>.
        /// </summary>
        /// <param name="number">The number of the <see cref="Wave"/> to generate.</param>
        /// <returns>A <see cref="Wave"/> that was generated based on the <paramref name="number"/>, and the <see cref="generationConfig"/>.</returns>
        public Wave GenerateWave(int number, int pointsAvailable)
        {
            List<EnemySpawnInfo> enemiesToSpawn = new List<EnemySpawnInfo>();

            while (pointsAvailable > 0)
            {
                EnemyWaveInfo enemy = GetEnemyBasedOnPoints(pointsAvailable, number);
                if (enemy == null)
                {
                    Debug.LogError($"No enemy was found with the points available {pointsAvailable}.");
                    break;
                }

                #region Check if enemy is already added
                    
                EnemySpawnInfo spawnInfo = null;
                for (int i = 0; i < enemiesToSpawn.Count; i++)
                {
                    if (enemiesToSpawn[i].Prefab == enemy.Prefab)
                    {
                        spawnInfo = enemiesToSpawn[i];
                        break;
                    }
                }
                    
                #endregion

                // Add to spawn amount or add a new enemy entry based on the previous check.
                if (spawnInfo == null)
                { enemiesToSpawn.Add(new EnemySpawnInfo(enemy.Prefab)); }
                else
                { spawnInfo.AddToSpawnAmount(); }
                pointsAvailable -= enemy.PointsRequiredToSpawn;
            }
            
            return new Wave(number, enemiesToSpawn.ToArray());
        }
    }
}