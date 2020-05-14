using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.Managers;
using TMPro;
using UnityEngine;

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
                Debug.Log("The game session could not be found.");
                return;
            }

            timeLabel.text = GetFormattedTimer(gameSession.EndTime);
            spaceCreditsLabel.text = 4929.ToString();
        }

        private string GetFormattedTimer(float timeInSeconds)
        {
            float minutes = Mathf.Floor(timeInSeconds / 60);
            float seconds = Mathf.Floor(timeInSeconds % 60);
            return $"{minutes:00}m {seconds:00}s";
        }

        #region Button methods

        public void Continue() => GameManager.Instance.LoadMainScene();

        public void PlayAgain() => GameManager.Instance.LoadLevel();

        #endregion
    }
}