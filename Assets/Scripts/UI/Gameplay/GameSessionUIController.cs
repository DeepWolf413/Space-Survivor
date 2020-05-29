using System;
using System.Collections.Generic;
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
        private DangerIndicator dangerIndicatorPrefab = null;

        private List<DangerIndicator> dangerIndicatorsPool = new List<DangerIndicator>();

        #region Unity callbacks

        private void OnEnable()
        {
            if (ObjectUtilities.TryGetObjectOfType(out GameSession gameSession))
            { gameSession.GameEnded += OnGameEnded; }
            
            GameEvents.EnemyShipSpawned += GameEventsOnEnemyShipSpawned;
        }

        private void OnDisable()
        {
            if (GameManager.IsApplicationQuitting || GameManager.SceneManager.IsChangingScene)
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

        private void AddDangerIndicator(Transform target)
        {
            DangerIndicator dangerIndicator = null;
            
            for (int i = 0; i < dangerIndicatorsPool.Count; i++)
            {
                if (dangerIndicatorsPool[i].gameObject.activeSelf)
                { continue; }

                dangerIndicator = dangerIndicatorsPool[i];
                dangerIndicator.gameObject.SetActive(true);
                break;
            }

            if (dangerIndicator == null)
            {
                dangerIndicator = Instantiate(dangerIndicatorPrefab, playerHudUI.transform);
                dangerIndicatorsPool.Add(dangerIndicator);
            }
            
            dangerIndicator.Target = target;
        }

        #endregion

        #region Event listeners

        private void GameEventsOnEnemyShipSpawned(GameObject shipSpawned) => AddDangerIndicator(shipSpawned.transform);
        
        private void OnGameEnded()
        {
            playerHudUI.SetActive(false);
            gameOverMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }

        #endregion
    }
}