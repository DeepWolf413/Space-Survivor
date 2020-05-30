using DG.Tweening;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.Feedbacks
{
    public class CameraEffects : MonoBehaviour
    {
        private Vector3 originalPosition = Vector3.zero;

        private new Transform transform;

        private void Awake()
        {
            transform = base.transform;
            originalPosition = transform.position;
        }

        public void Shake(float duration, Vector3 strength, int vibrato = 10, float randomness = 90.0f)
        {
            transform.DOComplete();
            transform.position = originalPosition;
            transform.DOShakePosition(duration, strength, vibrato, randomness);
        }
    }
}