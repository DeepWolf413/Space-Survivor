using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField]
        private RectTransform mainMenu = null;
        
        [SerializeField]
        private RectTransform changeShipMenu = null;

        [SerializeField]
        private RectTransform optionsMenu = null;
        
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
        
        public void StartGame() => GameManager.Instance.LoadLevel();
        
        /// <summary>
        /// Quits the game.
        /// </summary>
        public void QuitGame()
        {
            // Save progress.
            
            // Quit game.
            Application.Quit();
        }
        
        #endregion
        
        private void HideAllMenues()
        {
            mainMenu.gameObject.SetActive(false);
            changeShipMenu.gameObject.SetActive(false);
            optionsMenu.gameObject.SetActive(false);
        }
    }
}