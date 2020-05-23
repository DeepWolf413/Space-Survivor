using System;
using System.Collections;
using DeepWolf.SpaceSurvivor.Managers;
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

        private bool isGameInProgress = false;

        /// <summary>
        /// The time stamp of the next asteroid event.
        /// </summary>
        private float nextAsteroidEvent = 0.0f;

        private Coroutine waveSpawningCoroutine;
        private Coroutine asteroidEventCoroutine;

        #region Properties

        public float TimeSinceGameStarted => Time.time - gameStartedTime;

        public float EndTime => gameEndedTime - gameStartedTime;

        public bool HasNewBestTime { get; private set; }
        
        /// <summary>
        /// Gets or sets(private) the space credits reward.
        /// </summary>
        public int SpaceCreditsCounter { get; private set; }

        #endregion

        #region Events

        public event Action GameStarted = delegate { };

        /// <summary>
        /// Occurs when the game has ended.
        /// </summary>
        public event Action GameEnded = delegate { };

        public event Action<int> SpaceCreditsCounterChanged = delegate { };

        #endregion

        #region Unity callbacks

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

        private void OnEnable() => GameEvents.PlayerShipDestroyed += OnPlayerShipDestroyed;

        private void OnDisable()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }
            
            GameEvents.PlayerShipDestroyed -= OnPlayerShipDestroyed;
        }

        private void Start() => ReferenceManager.Register(this);

        private void Update()
        {
            if (!isGameInProgress)
            { return; }

            if (nextAsteroidEvent <= Time.time && asteroidEventCoroutine == null)
            { asteroidEventCoroutine = StartCoroutine(SpawnAsteroidEvent()); }
        }

        #endregion

        public void AddSpaceCreditsReward(int amount)
        {
            SpaceCreditsCounter += amount;
            SpaceCreditsCounterChanged?.Invoke(SpaceCreditsCounter);
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
            if (waveSpawningCoroutine != null)
            { StopCoroutine(waveSpawningCoroutine); }

            if (asteroidEventCoroutine != null)
            { StopCoroutine(asteroidEventCoroutine); }

            CancelInvoke(nameof(StartNextWave));
            gameEndedTime = Time.time;

            GameManager.SaveManager.SaveState.AddSpaceCredits(SpaceCreditsCounter);
            HasNewBestTime = GameManager.SaveManager.SaveState.SetBestTime(EndTime);
            isGameInProgress = false;
            GameManager.SaveManager.SaveGame();
            GameEnded?.Invoke();
        }

        private void StartNextWave()
        {
            currentWave = generationConfig.GenerateWave(currentWave == null ? 1 : currentWave.Number + 1, pointsForSpawning);
            pointsForSpawning = Mathf.Clamp(pointsForSpawning + generationConfig.PointsPerWave, generationConfig.StartingPoints, generationConfig.MaxPoints);
            waveSpawningCoroutine = StartCoroutine(SpawnWave());
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
                    yield return new WaitForSeconds(generationConfig.GetRndEnemySpawnDelay());
                    GameObject spawnedEnemy = spawner.Spawn(spawnInfo.Prefab);
                    GameEvents.SignalEnemyShipSpawned(spawnedEnemy);
                }

                currentEnemyIndex++;
            }

            // Start the next wave after the delay
            Invoke(nameof(StartNextWave), generationConfig.NextWaveStartDelay);
        }

        /// <summary>
        /// Sets the value of <see cref="nextAsteroidEvent"/> to a new time based on <see cref="Time.time"/> + <see cref="WaveGenerationConfig.GetRndAsteroidEventSpawnDelay()"/>.
        /// </summary>
        private void UpdateAsteroidEventTime() => nextAsteroidEvent = Time.time + generationConfig.GetRndAsteroidEventSpawnDelay();

        #region Event listeners

        private void OnPlayerShipDestroyed() => EndGame();

        #endregion
    }
}