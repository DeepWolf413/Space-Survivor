using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class GameSession : MonoBehaviour
    {
        [SerializeField]
        private WaveGenerationConfig generationConfig = null;

        [SerializeField]
        private Spawner spawner = null;

        private Wave currentWave = null;
        private float gameStartedTime = 0.0f;
        private float gameEndedTime = 0.0f;
        private int pointsForSpawning = 0;
        private int enemiesLeftInWave = 0;
        
        private bool isGameInProgress = false;
        
        /// <summary>
        /// The time stamp of the next asteroid event.
        /// </summary>
        private float nextAsteroidEvent = 0.0f;

        private Coroutine waveSpawningCoroutine;
        private Coroutine asteroidEventCoroutine;

        #region Properties

        public float TimeSinceGameStarted => Time.time - gameStartedTime;

        #endregion

        #region Events

        public event Action GameStarted = delegate { };
        public event Action GameEnded = delegate { }; 

        #endregion

        private void OnValidate()
        {
            if (!spawner)
            { spawner = FindObjectOfType<Spawner>(); }
        }

        private void Awake()
        {
            pointsForSpawning = generationConfig.StartingPoints;
            
            // TODO: Add a countdown, then start the game after.
            StartGame();
        }

        private void OnEnable()
        {
            GameEvents.EnemyShipDestroyed += OnEnemyShipDestroyed;
        }

        private void OnDisable()
        {
            GameEvents.EnemyShipDestroyed -= OnEnemyShipDestroyed;
        }

        private void Update()
        {
            if (!isGameInProgress)
            { return; }

            if (nextAsteroidEvent <= Time.time && asteroidEventCoroutine == null)
            { asteroidEventCoroutine = StartCoroutine(SpawnAsteroidEvent()); }
        }

        private void StartGame()
        {
            Random.InitState(generationConfig.Seed);
            gameStartedTime = Time.time;
            UpdateAsteroidEventTime();
            StartNextWave();
            
            isGameInProgress = true;
            GameStarted?.Invoke();
        }

        private void EndGame()
        {
            StopCoroutine(waveSpawningCoroutine);
            StopCoroutine(asteroidEventCoroutine);
            CancelInvoke(nameof(StartNextWave));
            gameEndedTime = Time.time;

            isGameInProgress = false;
            GameEnded?.Invoke();
        }

        private void StartNextWave()
        {
            currentWave = generationConfig.GenerateWave(currentWave == null ? 1 : currentWave.Number + 1, pointsForSpawning);
            enemiesLeftInWave = currentWave.GetEnemySpawnCount();
            
            waveSpawningCoroutine = StartCoroutine(SpawnWave());
        }

        private void CompleteWave()
        {
            pointsForSpawning = Mathf.Clamp(pointsForSpawning + generationConfig.PointsPerWave, generationConfig.StartingPoints, generationConfig.MaxPoints);
            Invoke(nameof(StartNextWave), 3.5f);
        }

        private IEnumerator SpawnAsteroidEvent()
        {
            GameEvents.SignalAsteroidsEventSpawned();
            int spawnAmount = generationConfig.GetRndAsteroidSpawnCount();
            for (int i = 0; i < spawnAmount; i++)
            {
                yield return new WaitForSeconds(generationConfig.AsteroidSpawnDelay);
                spawner.SpawnAsteroid();
            }

            UpdateAsteroidEventTime();
            asteroidEventCoroutine = null;
        }
        
        private IEnumerator SpawnWave()
        {
            int currentEnemyIndex = 0;
            while (currentEnemyIndex < currentWave.EnemyCount)
            {
                EnemySpawnInfo spawnInfo = currentWave.GetEnemy(currentEnemyIndex);
                for (int i = 0; i < spawnInfo.SpawnAmount; i++)
                {
                    GameObject spawnedEnemy = spawner.Spawn(spawnInfo.Prefab);
                    GameEvents.SignalEnemyShipSpawned(spawnedEnemy);
                    yield return new WaitForSeconds(generationConfig.GetRndEnemySpawnDelay());
                }
                
                currentEnemyIndex++;
            }
        }

        /// <summary>
        /// Sets the value of <see cref="nextAsteroidEvent"/> to a new time based on <see cref="Time.time"/> + <see cref="WaveGenerationConfig.GetRndAsteroidEventSpawnDelay()"/>.
        /// </summary>
        private void UpdateAsteroidEventTime() => nextAsteroidEvent = Time.time + generationConfig.GetRndAsteroidEventSpawnDelay();
        
        #region Event listeners

        private void OnEnemyShipDestroyed()
        {
            enemiesLeftInWave--;
            if (enemiesLeftInWave <= 0)
            { CompleteWave(); }
        }

        #endregion
    }
}