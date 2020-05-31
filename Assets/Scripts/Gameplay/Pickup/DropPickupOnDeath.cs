using System.Collections.Generic;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class DropPickupOnDeath : MonoBehaviour
    {
        [SerializeField]
        private PickupDrop[] possibleDrops = new PickupDrop[0];

        [SerializeField]
        private Health healthComponent = null;

        private void OnValidate()
        {
            if (!healthComponent)
            { healthComponent = GetComponent<Health>(); }
        }

        private void OnEnable() => healthComponent.OnDeath += OnDeath;

        private void OnDisable()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }
            
            healthComponent.OnDeath -= OnDeath;
        }

        private void DropPickup()
        {
            List<Pickup> potentialDrops = new List<Pickup>();
            
            int rndNumber = GameSession.PickupDropRng.Next(0, 101);
            for (int i = 0; i < possibleDrops.Length; i++)
            {
                if (possibleDrops[i].DropChance >= rndNumber)
                { potentialDrops.Add(possibleDrops[i].PickupToDrop); }
            }

            if (potentialDrops.Count > 0)
            { GameEvents.SignalPickupSpawned(Instantiate(potentialDrops[GameSession.PickupDropRng.Next(0, potentialDrops.Count)], transform.position, Quaternion.identity)); }
        }

        #region Event listeners

        private void OnDeath() => DropPickup();
        #endregion
    }
}