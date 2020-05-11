using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [System.Serializable]
    public class EnemyWaveInfo
    {
        [SerializeField]
        private GameObject prefab;

        [SerializeField]
        private int pointsRequiredToSpawn;

        [SerializeField, Tooltip("In what wave the enemy should be introduced.")]
        private int introduceAtWave;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyWaveInfo"/> class.
        /// </summary>
        public EnemyWaveInfo()
        {
            prefab = null;
            pointsRequiredToSpawn = 1;
            introduceAtWave = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the prefab of the enemey.
        /// </summary>
        public GameObject Prefab => prefab;

        /// <summary>
        /// Gets an <see cref="int"/> that determines the amount of points that is required to spawn this enemy.
        /// </summary>
        public int PointsRequiredToSpawn => pointsRequiredToSpawn;
        
        /// <summary>
        /// Gets an <see cref="int"/> that determines when the enemy should be introduced.
        /// </summary>
        public int IntroduceAtWave => introduceAtWave;

        #endregion
    }
}