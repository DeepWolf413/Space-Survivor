using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.Managers;
using DeepWolf.SpaceSurvivor.Utilities;
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

        [SerializeField]
        private AudioClip appearSfx = null;

        private void OnEnable()
        {
            if (!ObjectUtilities.TryGetObjectOfType(out GameSession gameSession))
            { return; }
            
            timeLabel.text = TimeUtilities.GetFormattedTime(gameSession.EndTime);
            spaceCreditsLabel.text = gameSession.SpaceCreditsCounter.ToString();
            
            if (gameSession.HasNewBestTime)
            { personalBestTimeLabel.SetActive(true); }

            if (appearSfx != null)
            { GameManager.SoundManager.PlayGlobalSound(appearSfx, ESoundType.Sfx); }
        }

        #region Button methods

        public void Continue() => GameManager.SceneManager.LoadMainScene();

        public void PlayAgain() => GameManager.SceneManager.LoadLevel();

        #endregion
    }
}