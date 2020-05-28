using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [System.Serializable]
    public class PickupDrop
    {
        [SerializeField, Range(0, 100)]
        private int dropChance;

        [SerializeField]
        private Pickup pickupToDrop;
        
        public PickupDrop()
        {
            dropChance = 100;
            pickupToDrop = null;
        }

        public float DropChance => dropChance;
        
        public Pickup PickupToDrop => pickupToDrop;
    }
}