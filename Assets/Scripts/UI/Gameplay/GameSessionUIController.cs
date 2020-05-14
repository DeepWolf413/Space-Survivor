using DeepWolf.SpaceSurvivor.Gameplay;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class GameSessionUIController : MonoBehaviour
    {
        [SerializeField]
        private GameObject playerHudUI = null;

        [SerializeField]
        private GameOverMenuUI gameOverMenu = null;

        #region Unity callbacks

        private void OnEnable()
        {
            GameSession gameSession = FindObjectOfType<GameSession>();
            if (!gameSession)
            {
                Debug.Log("The game session could not be found.");
                return;
            }
            
            gameSession.GameEnded += OnGameEnded;
        }
        
        private void OnDisable()
        {
            GameSession gameSession = FindObjectOfType<GameSession>();
            if (!gameSession)
            {
                Debug.Log("The game session could not be found.");
                return;
            }
            
            gameSession.GameEnded -= OnGameEnded;
        }

        #endregion

        #region Event listeners

        private void OnGameEnded()
        {
            playerHudUI.SetActive(false);
            gameOverMenu.gameObject.SetActive(true);
        }

        #endregion
    }
}