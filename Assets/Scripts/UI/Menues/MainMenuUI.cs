using System;
using DeepWolf.SpaceSurvivor.Managers;
using TMPro;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI bestTimeLabel = null;

        [SerializeField]
        private RectTransform mainMenu = null;
        
        [SerializeField]
        private RectTransform changeShipMenu = null;

        [SerializeField]
        private RectTransform optionsMenu = null;

        private void Start() => bestTimeLabel.text = $"<color=#00ABFD>Personal Best</color>\n{GameManager.SaveManager.SaveState.GetFormattedBestTime()}";

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
        
        public void TransitionToOptionsMenu()
        {
            HideAllMenues();
            optionsMenu.gameObject.SetActive(true);
        }
        
        public void StartGame() => GameManager.SceneManager.LoadLevel();
        
        /// <summary>
        /// Quits the game.
        /// </summary>
        public void QuitGame() => Application.Quit();
        
        #endregion
        
        private void HideAllMenues()
        {
            mainMenu.gameObject.SetActive(false);
            changeShipMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
        }
    }
}