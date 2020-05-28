using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Data
{
    [CreateAssetMenu(menuName = "Game/Ship/New player ship data")]
    public class PlayerShipData : ShipData
    {
        [Header("[Shop]")]
        [SerializeField]
        private bool startOwned = false;
        
        [SerializeField]
        private int price = 0;

        #region Properties

        public bool StartOwned => startOwned;

        public int Price => price;

        #endregion
    }
}