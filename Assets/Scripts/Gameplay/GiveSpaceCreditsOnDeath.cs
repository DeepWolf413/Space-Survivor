using System;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [RequireComponent(typeof(Health))]
    public class GiveSpaceCreditsOnDeath : MonoBehaviour
    {
        [SerializeField]
        private int amountToGive = 25;

        [SerializeField]
        private Health healthComponent = null;

        private void OnValidate()
        {
            if (!healthComponent)
            { healthComponent = GetComponent<Health>(); }
        }

        private void OnEnable() => healthComponent.OnDeath += OnDeath;

        private void OnDisable() => healthComponent.OnDeath -= OnDeath;

        private void OnDeath()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }

            if (ReferenceManager.TryGet(out GameSession gameSession))
            { gameSession.AddSpaceCreditsReward(amountToGive); }
        }
    }
}