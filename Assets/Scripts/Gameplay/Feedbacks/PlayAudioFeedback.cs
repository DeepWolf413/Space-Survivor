using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.Feedbacks
{
    public class PlayAudioFeedback : Feedback
    {
        [SerializeField]
        private AudioClip clipToPlay = null;

        public override void Play()
        { SoundManager.Instance.PlayGlobalSound(clipToPlay, ESoundType.Sfx); }
    }
}