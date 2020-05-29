using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.Managers;
using DeepWolf.SpaceSurvivor.Utilities;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class GameSessionUIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerHudUI = null;

        [SerializeField]
        private GameOverMenuUI gameOverMenu = null;

        [SerializeField]
        private PauseMenuUI pauseMenu = null;

        [SerializeField]
        private PoolData enemyDangerIndicatorPool = null;
        
        [SerializeField]
        private PoolData asteroidDangerIndicatorPool = null;

        #region Unity callbacks

        private void OnEnable()
        {
            if (ObjectUtilities.TryGetObjectOfType(out GameSession gameSession))
            { gameSession.GameEnded += OnGameEnded; }
            
            GameEvents.EnemyShipSpawned += OnEnemyShipSpawned;
            GameEvents.AsteroidSpawned += OnAsteroidSpawned;
        }

        private void OnDisable()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }
            
            GameEvents.EnemyShipSpawned -= OnEnemyShipSpawned;
            GameEvents.AsteroidSpawned -= OnAsteroidSpawned;
            
            if (GameManager.SceneManager.IsChangingScene)
            { return; }
            
            if (ObjectUtilities.TryGetObjectOfType(out GameSession gameSession))
            { gameSession.GameEnded -= OnGameEnded; }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.P) && !gameOverMenu.gameObject.activeSelf)
            { TogglePauseMenu(); }
        }

        #endregion

        private void TogglePauseMenu()
        {
            bool newActiveState = !pauseMenu.gameObject.activeSelf;
            pauseMenu.gameObject.SetActive(newActiveState);
            playerHudUI.gameObject.SetActive(!newActiveState);
        }

        #region Danger Indicator methods

        private void AddEnemyDangerIndicator(Transform target)
        {
            DangerIndicator dangerIndicator = PoolManager.Spawn<DangerIndicator>(enemyDangerIndicatorPool, playerHudUI.transform);
            dangerIndicator.Target = target;
        }
        
        private void AddAsteroidDangerIndicator(Transform target)
        {
            DangerIndicator dangerIndicator = PoolManager.Spawn<DangerIndicator>(asteroidDangerIndicatorPool, playerHudUI.transform);
            dangerIndicator.Target = target;
        }

        #endregion

        #region Event listeners

        private void OnEnemyShipSpawned(GameObject shipSpawned) => AddEnemyDangerIndicator(shipSpawned.transform);
        
        private void OnAsteroidSpawned(GameObject asteroidSpawned) => AddAsteroidDangerIndicator(asteroidSpawned.transform);
        
        private void OnGameEnded()
        {
            playerHudUI.SetActive(false);
            gameOverMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }

        #endregion
    }
}