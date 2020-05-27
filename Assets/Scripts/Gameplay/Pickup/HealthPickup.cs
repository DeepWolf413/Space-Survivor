using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class HealthPickup : Pickup
    {
        [Header("[Health Pickup]")]
        [SerializeField, Range(0.01f, 1.0f)]
        private float percentageAmountToGive = 0.5f;

        public override void Take(GameObject playerObject)
        {
            if (playerObject.TryGetComponent(out Health healthComponent))
            {
                float healAmount = healthComponent.MaxValue * percentageAmountToGive;
                healthComponent.Heal(healAmount);
            }
            
            base.Take(playerObject);
        }
    }
}