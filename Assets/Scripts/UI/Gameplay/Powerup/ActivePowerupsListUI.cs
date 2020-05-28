using System;
using System.Collections.Generic;
using DeepWolf.SpaceSurvivor.Gameplay.PowerupSystem;
using DeepWolf.SpaceSurvivor.Managers;
using DeepWolf.SpaceSurvivor.Utilities;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class ActivePowerupsListUI : MonoBehaviour
    {
        [SerializeField]
        private ActivePowerupUI activePowerupUIPrefab = null;

        [SerializeField]
        private RectTransform activePowerupContainer = null;

        private List<ActivePowerupUI> pooledUI = new List<ActivePowerupUI>();

        private void OnEnable()
        {
            if (!ObjectUtilities.TryGetWithTag("Player", out GameObject playerObject))
            { return; }
            
            if (!playerObject.TryGetComponent(out PowerupsController powerupsController))
            { return; }
            
            powerupsController.PowerupActivated += OnPowerupActivated;
            powerupsController.PowerupDeactivated += OnPowerupDeactivated;
        }

        private void OnDisable()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }
            
            if (!ObjectUtilities.TryGetWithTag("Player", out GameObject playerObject))
            { return; }
            
            if (!playerObject.TryGetComponent(out PowerupsController powerupsController))
            { return; }
                
            powerupsController.PowerupActivated -= OnPowerupActivated;
            powerupsController.PowerupDeactivated -= OnPowerupDeactivated;
        }

        private void Start()
        {
            if (TryGetComponent(out UIWorldObjectFollower objectFollowerComponent))
            { objectFollowerComponent.Target = GameObject.FindWithTag("Player").transform; }
        }

        private bool TryGetFromPool(out ActivePowerupUI powerupUI)
        {
            for (int i = 0; i < pooledUI.Count; i++)
            {
                if (!pooledUI[i].gameObject.activeSelf)
                { continue; }
                
                powerupUI = pooledUI[i];
                return true;
            }

            powerupUI = null;
            return false;
        }

        private void AddPowerup(ActivePowerup powerup)
        {
            ActivePowerupUI powerupUI = null;

            if (!TryGetFromPool(out powerupUI))
            {
                powerupUI = Instantiate(activePowerupUIPrefab, activePowerupContainer);
                pooledUI.Add(powerupUI);
            }

            powerupUI.Show(powerup);
        }
        
        private void RemovePowerup(ActivePowerup powerup)
        {
            for (int i = 0; i < pooledUI.Count; i++)
            {
                if (!pooledUI[i].gameObject.activeSelf)
                { continue; }

                if (pooledUI[i].IsRepresentingPowerup(powerup))
                { pooledUI[i].Hide(); }
            }
        }

        private void OnPowerupActivated(ActivePowerup powerup) => AddPowerup(powerup);

        private void OnPowerupDeactivated(ActivePowerup powerup) => RemovePowerup(powerup);
    }
}