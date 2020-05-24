using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.Feedbacks
{
    public class PoolSpawnFeedback : Feedback
    {
        [SerializeField]
        private PoolData pool = null;

        public override void Play() => PoolManager.Spawn(pool, transform.position, Quaternion.identity);
    }
}