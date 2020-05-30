using System;
using System.IO;
using DeepWolf.SpaceSurvivor.Data;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;
using Logger = DeepWolf.Logging.Logger;

namespace DeepWolf.SpaceSurvivor.SaveSystem
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        private const string SaveFolderPath = "/idbfs/Space Survivor - GDTV Jam";
        private const string SaveFilePath = SaveFolderPath + "/SaveFile.json";

        [SerializeField]
        private PlayerShipData[] playerShips = new PlayerShipData[0];

        #region Properties

        protected override bool UseDontDestroyOnLoad => false;
        
        public int PlayerShipCount => playerShips.Length;

        public PlayerSaveState SaveState { get; private set; }

        #endregion

        #region Events

        public event Action<PlayerShipData> SelectedShipChanged = delegate { };

        #endregion

        protected override void Awake()
        {
            base.Awake();
            LoadSaveGame();
        }

        private void SetupSaveGame()
        {
            // Add all ships that should be owned at the start.
            for (int i = 0; i < playerShips.Length; i++)
            {
                if (playerShips[i].StartOwned)
                {
                    SaveState.AddShipAsOwned(playerShips[i].Id);
                        
                    if (!SaveState.HasSelectedShip)
                    { SaveState.SelectShip(playerShips[i].Id); }
                }
            }
        }
        
        private void LoadSaveGame()
        {
            if (File.Exists(SaveFilePath))
            {
                string saveStateJson = File.ReadAllText(SaveFilePath);
                SaveState = JsonUtility.FromJson<PlayerSaveState>(saveStateJson);
            }
            else
            {
                if (!Directory.Exists(SaveFolderPath))
                { Directory.CreateDirectory(SaveFolderPath); }
                
                SaveState = new PlayerSaveState();
                SetupSaveGame();
            }
        }

        public bool SetBestTime(float time) => SaveState.SetBestTime(GameManager.Instance.SelectedDifficulty.name, time);

        public float GetBestTime() => SaveState.GetBestTimeForDifficulty(GameManager.Instance.SelectedDifficulty.name);

        public void ResetSave()
        {
            SaveState = new PlayerSaveState();
            SetupSaveGame();
            SaveGame();
        }

        private PlayerShipData GetShipById(int id)
        {
            for (int i = 0; i < PlayerShipCount; i++)
            {
                if (playerShips[i].Id == id)
                { return playerShips[i]; }
            }

            return null;
        }

        public void SaveGame()
        {
            if (SaveState == null)
            { return; }

            if (!Directory.Exists(SaveFolderPath))
            { Directory.CreateDirectory(SaveFolderPath); }
            
            string saveStateJson = JsonUtility.ToJson(SaveState);
            File.WriteAllText(SaveFilePath, saveStateJson);
        }

        /// <summary>
        /// Gets the selected ship. Returns null if no ship is selected.
        /// </summary>
        public PlayerShipData GetSelectedShip() => SaveState.HasSelectedShip ? GetShipById(SaveState.SelectedShipId) : null;
        
        /// <summary>
        /// Returns a <see cref="bool"/> that indicates whether the <paramref name="ship"/> is the selected one.
        /// </summary>
        /// <param name="ship">The ship to check if it is the selected one.</param>
        /// <returns>A <see cref="bool"/> that indicates whether the <paramref name="ship"/> is the selected one.</returns>
        public bool IsShipChosen(PlayerShipData ship) => ship == GetSelectedShip();

        public bool IsShipOwned(PlayerShipData ship) => SaveState.IsShipOwned(ship.Id);

        /// <summary>
        /// Selects the specified <paramref name="ship"/>.
        /// <para>
        /// Returns a <see cref="bool"/> which indicates whether it was successful, and a <see cref="string"/> with the error message if unsuccessful.
        /// </para>
        /// </summary>
        /// <param name="ship">The ship to select.</param>
        /// <returns>A <see cref="bool"/> which indicates whether it was successful, and a <see cref="string"/> with the error message if unsuccessful.</returns>
        public (bool, string) SelectShip(PlayerShipData ship)
        {
            (bool success, string error) = SaveState.SelectShip(ship.Id);
            
            if (success)
            { SelectedShipChanged?.Invoke(ship); }
            else
            { Logger.LogError(error); }

            return (success, error);
        }
    }
}