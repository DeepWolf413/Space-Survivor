using UnityEngine;
using UnityEngine.Serialization;

namespace DeepWolf.SpaceSurvivor.Data
{
    [CreateAssetMenu(menuName = "Game/Data/New player ship data")]
    public class PlayerShipData : ScriptableObject
    {
        [SerializeField]
        private int id = 0;
        
        [SerializeField]
        private string displayName = "Unnamed";

        [FormerlySerializedAs("icon")]
        [SerializeField]
        private Sprite sprite = null;

        [Header("[Stats]")]
        [SerializeField]
        private float health = 100.0f;

        [SerializeField]
        private float speed = 25.0f;

        [SerializeField]
        private float shield = 200.0f;
        
        [Header("[Shop]")]
        [SerializeField]
        private bool startOwned = false;
        
        [SerializeField]
        private float price = 0.0f;

        #region Properties

        public int Id => id;
        
        public string DisplayName => displayName;

        public Sprite Sprite => sprite;

        public float Health => health;

        public float Speed => speed;

        public float Shield => shield;

        public bool StartOwned => startOwned;

        public float Price => price;

        #endregion
    }
}