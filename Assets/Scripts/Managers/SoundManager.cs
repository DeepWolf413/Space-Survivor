using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace DeepWolf.SpaceSurvivor
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField]
        private AudioSource audioSourcePrefab = null;

        [SerializeField]
        private AudioMixerGroup musicMixer = null;

        [SerializeField]
        private AudioMixerGroup sfxMixer = null;

        private List<AudioSource> pool = new List<AudioSource>();

        /// <summary>
        /// Plays an <see cref="AudioClip"/> in 3D.
        /// </summary>
        /// <param name="clip">The <see cref="AudioClip"/> to play.</param>
        /// <param name="position">The position to play the clip at.</param>
        /// <param name="radius">How far away the <see cref="AudioClip"/> can be heard.</param>
        public void PlaySoundAtPosition(AudioClip clip, Vector3 position, float radius, ESoundType soundType)
        {
            AudioSource audioSource = GetAvailableAudioSource();
            audioSource.gameObject.SetActive(true);
            audioSource.transform.position = position;
            audioSource.clip = clip;
            audioSource.spatialBlend = 1.0f;
            audioSource.maxDistance = radius;

            switch (soundType)
            {
                case ESoundType.Music:
                    audioSource.outputAudioMixerGroup = musicMixer;
                    break;
                case ESoundType.Sfx:
                    audioSource.outputAudioMixerGroup = sfxMixer;
                    break;
            }
            
            audioSource.Play();
        }
        
        /// <summary>
        /// Plays an <see cref="AudioClip"/> in 2D.
        /// </summary>
        /// <param name="clip">The <see cref="AudioClip"/> to play.</param>
        public void PlayGlobalSound(AudioClip clip, ESoundType soundType)
        {
            AudioSource audioSource = GetAvailableAudioSource();
            audioSource.gameObject.SetActive(true);
            audioSource.clip = clip;
            audioSource.spatialBlend = 0.0f;
            audioSource.maxDistance = 5000;

            switch (soundType)
            {
                case ESoundType.Music:
                    audioSource.outputAudioMixerGroup = musicMixer;
                    break;
                case ESoundType.Sfx:
                    audioSource.outputAudioMixerGroup = sfxMixer;
                    break;
            }

            audioSource.Play();
        }
        
        private AudioSource GetAvailableAudioSource()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].gameObject.activeSelf)
                { return pool[i]; }
            }

            AudioSource audioSource = Instantiate(audioSourcePrefab);
            pool.Add(audioSource);
            return audioSource;
        }
    }
}