using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [CreateAssetMenu(menuName = "Game/Weapon System/New weapon data")]
    public class WeaponData : ScriptableObject
    {
        [Header("[General]")]
        [SerializeField]
        private int id = 0;

        [SerializeField]
        private string displayName = "Unnamed";

        [SerializeField]
        private float useRate = 1.0f;
        
        [Header("[Damage]")]
        [SerializeField]
        private float damage = 2.0f;

        #region Properties

        public int Id => id;

        public string DisplayName => displayName;

        public float UseRate => useRate;

        public float Damage => damage;

        #endregion
    }
}