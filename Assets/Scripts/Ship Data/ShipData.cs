using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Data
{
    public class ShipData : ScriptableObject
    {
        [SerializeField]
        protected int id = 0;
        
        [SerializeField]
        protected string displayName = "Unnamed";

        [SerializeField]
        protected Sprite sprite = null;

        [Header("[Stats]")]
        [SerializeField]
        protected float health = 100.0f;

        [SerializeField]
        protected float speed = 25.0f;

        [SerializeField]
        protected float shield = 200.0f;

        [SerializeField]
        protected float startShieldRegenDelay = 3.0f;

        [SerializeField]
        protected float shieldRegenRate = 400.0f;

        #region Properties

        public int Id => id;
        
        public string DisplayName => displayName;

        public Sprite Sprite => sprite;

        public float Health => health;

        public float Speed => speed;

        public float Shield => shield;

        public float StartShieldRegenDelay => startShieldRegenDelay;

        public float ShieldRegenRate => shieldRegenRate;

        #endregion
    }
}