﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Data
{
    [System.Serializable]
    public class PlayerSaveState
    {
        /// <summary>
        /// The amount of space credits the player has.
        /// </summary>
        [SerializeField]
        private int spaceCredits;
        
        /// <summary>
        /// The ships the player owns.
        /// </summary>
        [SerializeField]
        private List<int> shipsOwned;
        
        /// <summary>
        /// The player's best survived time.
        /// </summary>
        [SerializeField]
        private float bestTime;

        /// <summary>
        /// The ID of the selected ship.
        /// </summary>
        [SerializeField]
        private int selectedShipId;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerSaveState"/> class.
        /// </summary>
        public PlayerSaveState()
        {
            SpaceCredits = 0;
            shipsOwned = new List<int>();
            bestTime = 0.0f;
            selectedShipId = -1;
        }

        #endregion
        
        #region Properties

        /// <summary>
        /// Gets the player's best survived time.
        /// </summary>
        public float BestTime => bestTime;

        /// <summary>
        /// Gets or sets(private) the amount of space credits the player has.
        /// </summary>
        public int SpaceCredits
        {
            get => spaceCredits;
            private set
            {
                int oldValue = SpaceCredits;
                spaceCredits = value < 0 ? 0 : value;
                SpaceCreditsChanged?.Invoke(SpaceCredits, oldValue);
            }
        }

        /// <summary>
        /// Gets the id of the selected ship.
        /// </summary>
        public int SelectedShipId => selectedShipId;

        public bool HasSelectedShip => IsShipOwned(SelectedShipId);

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the player's space credits value has changed.
        /// <para>
        /// arg1: The new space credits value;
        /// arg2: The old space credits value;
        /// </para>
        /// </summary>
        public event Action<int, int> SpaceCreditsChanged = delegate { };

        #endregion

        #region Space credits methods

        public void AddSpaceCredits(int amount) => SpaceCredits += amount;

        public void RemoveSpaceCredits(int amount) => SpaceCredits -= amount;
        
        public bool CanAfford(int price) => SpaceCredits >= price;

        #endregion

        #region Best time methods

        public bool SetBestTime(float time)
        {
            if (time > BestTime)
            {
                bestTime = time;
                return true;
            }

            return false;
        }
        
        public string GetFormattedBestTime()
        {
            float minutes = Mathf.Floor(BestTime / 60);
            float seconds = Mathf.Floor(BestTime % 60);
            return $"{minutes:00}m {seconds:00}s";
        }

        #endregion
        
        #region Ship methods

        public void AddShipAsOwned(int shipId) => shipsOwned.Add(shipId);

        public bool IsShipOwned(int shipId)
        {
            for (int i = 0; i < shipsOwned.Count; i++)
            {
                if (shipsOwned[i] == shipId)
                { return true; }
            }

            return false;
        }

        /// <summary>
        /// Returns the id of the first owned ship.
        /// </summary>
        public int GetFirstOwnedShip() => shipsOwned.Count > 0 ? shipsOwned[0] : -1;

        public (bool, string) SelectShip(int shipId)
        {
            if (!IsShipOwned(shipId))
            { return (false, $"Ship with id '{shipId.ToString()}' is not owned."); }

            selectedShipId = shipId;
            return (true, string.Empty);
        }

        #endregion
    }
}