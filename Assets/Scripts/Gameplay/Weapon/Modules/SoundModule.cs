using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class SoundModule : WeaponModule
    {
        [SerializeField]
        private AudioClip startClip = null;

        [SerializeField]
        private AudioClip endClip = null;

        [SerializeField]
        private AudioClip[] cycleClips = new AudioClip[0];

        private int currentClipIndex = 0;

        private bool isFirstUse = false;
        
        public override void OnBeginUse()
        {
            if (startClip)
            { SoundManager.Instance.PlayGlobalSound(startClip, ESoundType.Sfx); }

            isFirstUse = true;
        }

        public override void OnEndUse()
        {
            if (endClip)
            { SoundManager.Instance.PlayGlobalSound(endClip, ESoundType.Sfx); }
            
            currentClipIndex = 0;
        }

        public override void OnUse()
        {
            if (!isFirstUse || startClip == null)
            { SoundManager.Instance.PlayGlobalSound(GetNextClipInCycle(), ESoundType.Sfx); }
            else
            { isFirstUse = false; }
        }

        private AudioClip GetNextClipInCycle()
        {
            if (currentClipIndex + 1 >= cycleClips.Length)
            { currentClipIndex = 0; }
            else
            { currentClipIndex++; }
            
            return cycleClips[currentClipIndex];
        }
    }
}