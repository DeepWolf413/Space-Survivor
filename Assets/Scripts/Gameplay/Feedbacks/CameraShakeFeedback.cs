using System;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.Feedbacks
{
    public class CameraShakeFeedback : Feedback
    {
        [SerializeField]
        private float duration = 0.2f;

        [SerializeField]
        private Vector3 strength = new Vector3(0.3f, 0.3f, 0.0f);

        [SerializeField]
        private int vibrato = 10;

        [SerializeField]
        private float randomness = 90.0f;

        private CameraEffects cameraEffectsComponent = null;

        private void Start() => cameraEffectsComponent = Camera.main.GetComponent<CameraEffects>();

        public override void Play() => cameraEffectsComponent.Shake(duration, strength, vibrato, randomness);
    }
}