using System;
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

        #region Unity callbacks

        private void OnEnable()
        {
            if (ObjectUtilities.TryGetObjectOfType(out GameSession gameSession))
            { gameSession.GameEnded += OnGameEnded; }
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

        #region Event listeners

        private void OnGameEnded()
        {
            playerHudUI.SetActive(false);
            gameOverMenu.gameObject.SetActive(true);
            pauseMenu.gameObject.SetActive(false);
        }

        #endregion
    }
}