using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [CreateAssetMenu(menuName = "Game/Gameplay/New wave generation config")]
    public class WaveGenerationConfig : ScriptableObject
    {
        [SerializeField, Tooltip("The seed for everything other than wave generation.")]
        private int globalSeed = 4814833;
        
        [SerializeField, Tooltip("The seed for generating waves.")]
        private int waveGenerationSeed = 4814833;
        
        [SerializeField]
        private EnemyWaveInfo[] enemies = new EnemyWaveInfo[0];

        [SerializeField, Tooltip("The amount to increase points with after each wave. These points are used to spawn enemies.")]
        private int pointsPerWave = 5;

        [SerializeField, Tooltip("The starting points used for spawning enemies in waves.")]
        private int startingPoints = 5;

        [SerializeField, Tooltip("The maximum amount of points that a wave can have.")]
        private int maxPoints = 25;

        [FormerlySerializedAs("waveSpawnDelay")]
        [SerializeField]
        private float nextWaveStartDelay = 10.0f;

        [SerializeField]
        private Vector2 enemySpawnDelayRange = new Vector2(1.2f, 3.0f);

        [FormerlySerializedAs("spawnAsteroidsDelayRange")]
        [SerializeField]
        private Vector2 asteroidsEventSpawnDelayRange = new Vector2(45.0f, 120.0f);

        [SerializeField]
        private float asteroidSpawnDelay = 1.5f;

        [SerializeField]
        private Vector2Int asteroidSpawnCountRange = new Vector2Int(10, 15);

        [Header("[Factors]")]
        [SerializeField]
        private float enemyHealthFactor = 1.0f;

        [SerializeField]
        private float enemySpeedFactor = 1.0f;

        [SerializeField]
        private float enemyDamageFactor = 1.0f;

        [SerializeField]
        private float rewardFactor = 1.0f;


        #region Properties

        /// <summary>
        /// Gets the seed for generating everything else other than generating waves.
        /// </summary>
        public int GlobalSeed => globalSeed;

        /// <summary>
        /// Gets the seed for generating waves.
        /// </summary>
        public int WaveGenerationSeed => waveGenerationSeed;

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
        /// Gets the wave spawn delay.
        /// </summary>
        public float NextWaveStartDelay => nextWaveStartDelay;

        /// <summary>
        /// Gets the asteroid spawn delay.
        /// </summary>
        public float AsteroidSpawnDelay => asteroidSpawnDelay;

        /// <summary>
        /// Gets the factor for enemy health.
        /// </summary>
        public float EnemyHealthFactor => enemyHealthFactor;

        /// <summary>
        /// Gets the factor for enemy speed.
        /// </summary>
        public float EnemySpeedFactor => enemySpeedFactor;

        /// <summary>
        /// Gets the factor for enemy damage.
        /// </summary>
        public float EnemyDamageFactor => enemyDamageFactor;

        /// <summary>
        /// Gets the reward factor.
        /// </summary>
        public float RewardFactor => rewardFactor;

        #endregion

        public float GetRndEnemySpawnDelay() => Random.Range(enemySpawnDelayRange.x, enemySpawnDelayRange.y);
        
        public int GetRndAsteroidSpawnCount() => Random.Range(asteroidSpawnCountRange.x, asteroidSpawnCountRange.y);
        
        public float GetRndAsteroidEventSpawnDelay() => Random.Range(asteroidsEventSpawnDelayRange.x, asteroidsEventSpawnDelayRange.y);

        private EnemyWaveInfo GetEnemyBasedOnPoints(int points, int waveNumber)
        {
            List<EnemyWaveInfo> possibleEnemies = new List<EnemyWaveInfo>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].PointsRequiredToSpawn <= points && enemies[i].IntroduceAtWave < waveNumber)
                { possibleEnemies.Add(enemies[i]); }
            }

            return possibleEnemies.Count > 0 ? enemies[GameSession.WaveGenerationRng.Next(0, possibleEnemies.Count)] : null;
        }
        
        private EnemySpawnInfo[] GetEnemiesToIntroduce(int waveNumber)
        {
            List<EnemySpawnInfo> enemiesToSpawn = new List<EnemySpawnInfo>();
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].IntroduceAtWave == waveNumber)
                {
                    EnemySpawnInfo spawnInfo = new EnemySpawnInfo(enemies[i].EnemyPool, enemies[i].PointsRequiredToSpawn);
                    enemiesToSpawn.Add(spawnInfo);
                    
                    for (int j = 1; j < enemies[i].IntroductionSpawnAmount; j++)
                    { spawnInfo.AddToSpawnAmount(); }
                }
            }

            return enemiesToSpawn.ToArray();
        }
        
        /// <summary>
        /// Generates a <see cref="Wave"/> based on the config from <see cref="generationConfig"/>.
        /// </summary>
        /// <param name="number">The number of the <see cref="Wave"/> to generate.</param>
        /// <returns>A <see cref="Wave"/> that was generated based on the <paramref name="number"/>, and the <see cref="generationConfig"/>.</returns>
        public Wave GenerateWave(int number, int pointsAvailable)
        {
            List<EnemySpawnInfo> enemiesToSpawn = new List<EnemySpawnInfo>();

            #region Add introduction enemies to the spawn queue

            EnemySpawnInfo[] enemiesToIntroduce = GetEnemiesToIntroduce(number);
            if (enemiesToIntroduce.Length > 0)
            {
                enemiesToSpawn.AddRange(enemiesToIntroduce);
                
                for (int i = 0; i < enemiesToIntroduce.Length; i++)
                { pointsAvailable -= enemiesToIntroduce[i].PointsRequiredToSpawn; }
            }

            #endregion

            while (pointsAvailable > 0)
            {
                EnemyWaveInfo enemy = GetEnemyBasedOnPoints(pointsAvailable, number);
                if (enemy == null)
                { break; }

                #region Check if enemy is already added
                    
                EnemySpawnInfo spawnInfo = null;
                for (int i = 0; i < enemiesToSpawn.Count; i++)
                {
                    if (enemiesToSpawn[i].EnemyPool == enemy.EnemyPool)
                    {
                        spawnInfo = enemiesToSpawn[i];
                        break;
                    }
                }
                    
                #endregion

                // Add to spawn amount or add a new enemy entry based on the previous check.
                if (spawnInfo == null)
                { enemiesToSpawn.Add(new EnemySpawnInfo(enemy.EnemyPool, enemy.PointsRequiredToSpawn)); }
                else
                { spawnInfo.AddToSpawnAmount(); }
                pointsAvailable -= enemy.PointsRequiredToSpawn;
            }
            
            return new Wave(number, enemiesToSpawn.ToArray());
        }
    }
}