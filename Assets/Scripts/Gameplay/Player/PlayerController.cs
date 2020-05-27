﻿using DeepWolf.SpaceSurvivor.Data;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PlayerInputModule inputModule = new PlayerInputModule();

        [Header("[Cached Components]")]
        [SerializeField]
        private PlayerShipMovement movementComponent = null;

        [SerializeField]
        private Shooter shooterComponent = null;
        
        [SerializeField]
        private Health shipHealthComponent = null;

        [SerializeField]
        private Shield shipShieldComponent = null;

        [SerializeField]
        private SpriteRenderer spriteRenderer = null;

        #region Unity callbacks

        private void OnValidate()
        {
            if (!shipHealthComponent)
            { shipHealthComponent = GetComponent<Health>(); }
            
            if (!shipShieldComponent)
            { shipShieldComponent = GetComponent<Shield>(); }

            if (!spriteRenderer)
            { spriteRenderer = GetComponent<SpriteRenderer>(); }
        }

        private void Awake() => inputModule.Initialize(movementComponent, shooterComponent);

        private void Start() => LoadShipData();

        private void OnEnable() => shipHealthComponent.OnDeath += OnShipDead;
        
        private void OnDisable() => shipHealthComponent.OnDeath -= OnShipDead;

        private void Update() => inputModule.ProcessInput();

        #endregion

        /// <summary>
        /// Loads the ship data from the selected player ship that is stored on the <see cref="GameManager"/>.
        /// </summary>
        private void LoadShipData()
        {
            SaveManager saveManager = GameManager.SaveManager;
            if (!saveManager || !saveManager.GetSelectedShip())
            { return; }

            PlayerShipData shipData = saveManager.GetSelectedShip();
            
            spriteRenderer.sprite = shipData.Sprite;
            shipHealthComponent.MaxValue = shipData.Health;
            shipHealthComponent.ResetValue();

            shipShieldComponent.MaxValue = shipData.Shield;
            shipShieldComponent.ResetValue();
            
            movementComponent.ThrusterPower = shipData.Speed;
            shipShieldComponent.ShieldRegenRate = shipData.ShieldRegenRate;
            shipShieldComponent.StartShieldRegenDelay = shipData.StartShieldRegenDelay;
        }

        #region Event listeners
        
        private void OnShipDead() => GameEvents.SignalPlayerShipDestroyed();
        
        #endregion
    }
}