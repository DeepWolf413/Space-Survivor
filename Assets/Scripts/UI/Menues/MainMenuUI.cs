using System;
using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.Managers;
using DeepWolf.SpaceSurvivor.Utilities;
using TMPro;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI bestTimeLabel = null;

        [SerializeField]
        private TextMeshProUGUI difficultyLabel = null;

        [SerializeField]
        private RectTransform mainMenu = null;
        
        [SerializeField]
        private RectTransform changeShipMenu = null;
        
        [SerializeField]
        private RectTransform difficultySelectionMenu = null;

        [SerializeField]
        private RectTransform instructionsMenu = null;

        [SerializeField]
        private RectTransform creditsMenu = null;

        private void OnEnable() => GameManager.Instance.DifficultyChanged += OnDifficultyChanged;

        private void OnDisable()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }
            
            GameManager.Instance.DifficultyChanged -= OnDifficultyChanged;
        }

        private void Start()
        {
            RefreshBestTimeLabel();
            RefreshDifficultyLabel();
        }

        private void RefreshBestTimeLabel()
        {
            string headerText = "<color=#00ABFD>Personal Best</color>";
            string formattedTime = $"{TimeUtilities.GetFormattedTime(GameManager.SaveManager.GetBestTime())}";
            bestTimeLabel.text = $"{headerText}\n{formattedTime}";
        }

        private void RefreshDifficultyLabel() => difficultyLabel.text = GameManager.Instance.SelectedDifficulty.name;

        #region Button methods

        public void TransitionToMainMenu()
        {
            HideAllMenues();
            mainMenu.gameObject.SetActive(true);
        }
        
        public void TransitionToChangeShipMenu()
        {
            HideAllMenues();
            changeShipMenu.gameObject.SetActive(true);
        }
        
        public void TransitionToDifficultySelectionMenu()
        {
            HideAllMenues();
            difficultySelectionMenu.gameObject.SetActive(true);
        }
        
        public void TransitionToInstructionsMenu()
        {
            HideAllMenues();
            instructionsMenu.gameObject.SetActive(true);
        }
        
        public void TransitionToCreditsMenu()
        {
            HideAllMenues();
            creditsMenu.gameObject.SetActive(true);
        }
        
        public void StartGame() => GameManager.SceneManager.LoadLevel();

        public void ResetSave()
        {
            GameManager.SaveManager.ResetSave();
            RefreshBestTimeLabel();
        }
        
        /// <summary>
        /// Quits the game.
        /// </summary>
        public void QuitGame() => Application.Quit();
        
        #endregion
        
        private void HideAllMenues()
        {
            mainMenu.gameObject.SetActive(false);
            changeShipMenu.gameObject.SetActive(false);
            difficultySelectionMenu.gameObject.SetActive(false);
            instructionsMenu.gameObject.SetActive(false);
            creditsMenu.gameObject.SetActive(false);
        }

        #region Event listeners

        private void OnDifficultyChanged(WaveGenerationConfig difficulty)
        {
            RefreshBestTimeLabel();
            RefreshDifficultyLabel();
        }

        #endregion
    }
}