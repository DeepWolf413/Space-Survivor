using System;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [CreateAssetMenu(menuName = "Game/Pooling/New pool")]
    public class PoolData : ScriptableObject
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private GameObject prefab = null;

        [Header("[Preloading]")]
        [SerializeField]
        private bool preload = false;

        [SerializeField]
        private int preloadAmount = 5;
        
        #region Properties

        /// <summary>
        /// Gets the id of the pool.
        /// </summary>
        public string Id => id;
        
        public GameObject Prefab => prefab;

        public bool Preload => preload;

        public int PreloadAmount => preloadAmount;

        #endregion

        private void OnValidate() => id = prefab ? prefab.name : string.Empty;
    }
}