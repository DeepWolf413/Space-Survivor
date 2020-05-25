using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [System.Serializable]
    public class EnemyWaveInfo
    {
        [SerializeField]
        private PoolData enemyPool;

        [SerializeField]
        private int pointsRequiredToSpawn;

        [SerializeField, Tooltip("In what wave the enemy should be introduced.")]
        private int introduceAtWave;

        [SerializeField]
        private int introductionSpawnAmount;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnemyWaveInfo"/> class.
        /// </summary>
        public EnemyWaveInfo()
        {
            enemyPool = null;
            pointsRequiredToSpawn = 1;
            introduceAtWave = 1;
            introductionSpawnAmount = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the pool of the enemy to spawn.
        /// </summary>
        public PoolData EnemyPool => enemyPool;

        /// <summary>
        /// Gets an <see cref="int"/> that determines the amount of points that is required to spawn this enemy.
        /// </summary>
        public int PointsRequiredToSpawn => pointsRequiredToSpawn;
        
        /// <summary>
        /// Gets an <see cref="int"/> that determines when the enemy should be introduced.
        /// </summary>
        public int IntroduceAtWave => introduceAtWave;

        /// <summary>
        /// Gets an <see cref="int"/> that determines the amount of this enemy to spawn when introduced.
        /// </summary>
        public int IntroductionSpawnAmount => introductionSpawnAmount;

        #endregion
    }
}