using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.Feedbacks
{
    public class InstantiaterFeedback : Feedback
    {
        [SerializeField]
        private GameObject prefab = null;

        public override void Play() => Instantiate(prefab, transform.position, Quaternion.identity);
    }
}