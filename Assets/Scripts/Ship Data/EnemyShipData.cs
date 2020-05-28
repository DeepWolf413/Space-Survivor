using DeepWolf.SpaceSurvivor.Enums;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Data
{
    [CreateAssetMenu(menuName = "Game/Ship/New enemy ship data")]
    public class EnemyShipData : ShipData
    {
        [Header("[Shooting Pattern]")]
        [SerializeField]
        private EShootPattern shootPattern = EShootPattern.Single;
        
        [SerializeField]
        private float shootDelay = 2.0f;

        [Header("[Shooting Pattern - Burst]")]
        [SerializeField]
        private int burstAmount = 3;
        
        [Header("[Sprite Variation]")]
        [SerializeField]
        private Sprite[] spriteVariations = new Sprite[0];

        #region Properties

        public EShootPattern ShootPattern => shootPattern;

        public float ShootDelay => shootDelay;

        public int BurstAmount => burstAmount;

        #endregion
        
        public Sprite GetRndSpriteVariation() => spriteVariations[Random.Range(0, spriteVariations.Length)];
    }
}