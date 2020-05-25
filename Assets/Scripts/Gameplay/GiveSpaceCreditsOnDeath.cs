using DeepWolf.SpaceSurvivor.Managers;
using DeepWolf.SpaceSurvivor.Utilities;
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

            if (ObjectUtilities.TryGetObjectOfType(out GameSession gameSession))
            { gameSession.AddSpaceCreditsReward(amountToGive); }
        }
    }
}