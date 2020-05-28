using System;
using System.Collections.Generic;
using DeepWolf.SpaceSurvivor.Enums;
using UnityEngine;
using Logger = DeepWolf.Logging.Logger;

namespace DeepWolf.SpaceSurvivor.Gameplay.PowerupSystem
{
    public class PowerupsController : MonoBehaviour
    {
        [SerializeField]
        private List<ActivePowerup> activePowerups = new List<ActivePowerup>();

        #region Properties

        /// <summary>
        /// Gets the amount of active powerups.
        /// </summary>
        public int ActivePowerupsCount => activePowerups.Count;

        #endregion
        
        #region Events

        /// <summary>
        /// Occurs when a <see cref="Powerup"/> gets activated.
        /// <para>
        /// arg1: The <see cref="Powerup"/> that was activated;
        /// </para>
        /// </summary>
        public event Action<Powerup> PowerupActivated = delegate { };
        
        /// <summary>
        /// Occurs when a <see cref="Powerup"/> gets deactivated.
        /// <para>
        /// arg1: The <see cref="Powerup"/> that was deactivated;
        /// </para>
        /// </summary>
        public event Action<Powerup> PowerupDeactivated = delegate { }; 

        #endregion

        #region Unity callbacks

        private void Update()
        {
            for (int i = 0; i < activePowerups.Count; i++)
            {
                if (activePowerups[i].TimeLeft <= 0.0f)
                { DeactivatePowerup(activePowerups[i].Powerup); }
            }
        }

        #endregion

        public ActivePowerup GetActivePowerup(int index)
        {
            if (index < 0 || index >= ActivePowerupsCount)
            {
                Logger.LogError($"There's no active powerup at index({index}).");
                return new ActivePowerup();
            }

            return activePowerups[index];
        }
        
        public void ActivatePowerup(Powerup powerupToActivate)
        {
            // Deactivates the active powerup that is in the same group as the one we're about to activate.
            if (HasActivePowerupInGroup(powerupToActivate.Group, out ActivePowerup activePowerupFound))
            { DeactivatePowerup(activePowerupFound.Powerup); }
            
            // Activate the powerup we want activated.
            activePowerups.Add(new ActivePowerup(powerupToActivate));
            powerupToActivate.Activate(this);
            PowerupActivated?.Invoke(powerupToActivate);
        }

        private void DeactivatePowerup(Powerup powerupToDeactivate)
        {
            // Remove the powerup from the active powerups.
            for (int i = 0; i < ActivePowerupsCount; i++)
            {
                ActivePowerup activePowerup = GetActivePowerup(i);
                if (activePowerup.Powerup != powerupToDeactivate)
                { continue; }
                
                activePowerups.Remove(activePowerup);
                break;
            }
            
            // Deactivate the powerup.
            powerupToDeactivate.Deactivate(this);
            PowerupDeactivated?.Invoke(powerupToDeactivate);
        }
        
        private bool HasActivePowerupInGroup(EPowerupGroup group)
        {
            for (int i = 0; i < ActivePowerupsCount; i++)
            {
                ActivePowerup powerup = GetActivePowerup(i);
                if (powerup.Powerup.Group == group)
                { return true; }
            }

            return false;
        }
        
        private bool HasActivePowerupInGroup(EPowerupGroup group, out ActivePowerup activePowerup)
        {
            for (int i = 0; i < ActivePowerupsCount; i++)
            {
                ActivePowerup powerup = GetActivePowerup(i);
                if (powerup.Powerup.Group != group)
                { continue; }
                
                activePowerup = powerup;
                return true;
            }

            activePowerup = new ActivePowerup();
            return false;
        }
    }
}