using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.Feedbacks
{
    public class PlayAudioFeedback : Feedback
    {
        [SerializeField]
        private AudioClip clipToPlay = null;

        public override void Play() => GameManager.SoundManager.PlayGlobalSound(clipToPlay, ESoundType.Sfx);
    }
}