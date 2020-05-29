using System;
using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.Managers;
using DeepWolf.SpaceSurvivor.Utilities;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class PauseMenuUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI spaceCreditsLabel = null;

        [SerializeField]
        private TextMeshProUGUI timeLabel = null;

        [SerializeField]
        private CanvasGroup canvasGroupComponent = null;

        #region Unity callbacks

        private void OnValidate()
        {
            if (!canvasGroupComponent)
            { canvasGroupComponent = GetComponent<CanvasGroup>(); }
        }

        private void OnEnable()
        {
            GameManager.PauseGame(true);
            canvasGroupComponent.alpha = 0;
            canvasGroupComponent.DOFade(1.0f, 0.3f).SetUpdate(true);
            
            if (ObjectUtilities.TryGetObjectOfType(out GameSession gameSession))
            {
                spaceCreditsLabel.text = gameSession.SpaceCreditsCounter.ToString();
                timeLabel.text = TimeUtilities.GetFormattedTime(gameSession.TimeSinceGameStarted);
            }
        }

        private void OnDisable()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }
            
            GameManager.PauseGame(false);
            canvasGroupComponent.DOComplete();
        }

        #endregion

        #region Button methods

        public void MainMenu() => GameManager.SceneManager.LoadMainScene();

        #endregion
    }
}