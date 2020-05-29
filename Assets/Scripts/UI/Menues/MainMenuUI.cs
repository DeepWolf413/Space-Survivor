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
        private RectTransform mainMenu = null;
        
        [SerializeField]
        private RectTransform changeShipMenu = null;

        [SerializeField]
        private RectTransform optionsMenu = null;

        private void Start() => RefreshBestTimeLabel();

        private void RefreshBestTimeLabel()
        {
            string headerText = "<color=#00ABFD>Personal Best</color>";
            string formattedTime = $"{TimeUtilities.GetFormattedTime(GameManager.SaveManager.SaveState.BestTime)}";
            bestTimeLabel.text = $"{headerText}\n{formattedTime}";
        }
        
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

        public void ResetBestTime()
        {
            GameManager.SaveManager.SaveState.ResetBestTime();
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
            optionsMenu.gameObject.SetActive(false);
        }
    }
}