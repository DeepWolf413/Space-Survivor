using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.Managers;
using DeepWolf.SpaceSurvivor.Utilities;
using TMPro;
using UnityEngine;
using Logger = DeepWolf.Logging.Logger;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class GameOverMenuUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI timeLabel = null;

        [SerializeField]
        private TextMeshProUGUI spaceCreditsLabel = null;

        [SerializeField]
        private GameObject personalBestTimeLabel = null;

        private void OnEnable()
        {
            GameSession gameSession = FindObjectOfType<GameSession>();
            if (!gameSession)
            {
                Logger.LogInfo("The game session could not be found.");
                return;
            }

            timeLabel.text = TimeUtilities.GetFormattedTime(gameSession.EndTime);
            if (gameSession.HasNewBestTime)
            { personalBestTimeLabel.SetActive(true); }
        }

        #region Button methods

        public void Continue() => GameManager.SceneManager.LoadMainScene();

        public void PlayAgain() => GameManager.SceneManager.LoadLevel();

        #endregion
    }
}