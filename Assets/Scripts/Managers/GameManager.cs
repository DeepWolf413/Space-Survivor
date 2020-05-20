using System;
using DeepWolf.SpaceSurvivor.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeepWolf.SpaceSurvivor.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private string levelSceneName = "Level";

        [SerializeField]
        private string mainSceneName = "MainMenu";

        [SerializeField]
        private PlayerShipData[] playerShips = new PlayerShipData[0];
        
        [SerializeField]
        private PlayerShipData selectedPlayerShip = null;

        #region Properties

        public int PlayerShipCount => playerShips.Length;
        
        public PlayerShipData SelectedPlayerShip => selectedPlayerShip;
        
        public bool IsApplicationQuitting { get; private set; }

        #endregion

        #region Unity callbacks

        private void OnEnable() => Application.quitting += OnApplicationQuitting;
        
        private void OnDisable() => Application.quitting -= OnApplicationQuitting;

        private void OnApplicationQuitting() => IsApplicationQuitting = true;

        #endregion
        
        #region Player ship methods

        public PlayerShipData GetPlayerShipById(int id)
        {
            for (int i = 0; i < PlayerShipCount; i++)
            {
                PlayerShipData ship = playerShips[i];
                if (ship.Id == id)
                { return ship; }
            }

            return null;
        }
        
        public PlayerShipData GetPlayerShipByIndex(int index)
        {
            if (index < 0 || index >= PlayerShipCount)
            { return null; }

            return playerShips[index];
        }

        #endregion
        
        #region Scene loading methods

        public void LoadLevel() => SceneManager.LoadScene(levelSceneName);
        
        public void LoadMainScene() => SceneManager.LoadScene(mainSceneName);

        #endregion
    }
}