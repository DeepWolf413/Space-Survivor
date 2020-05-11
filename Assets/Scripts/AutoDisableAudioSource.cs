using UnityEngine;

namespace DeepWolf.SpaceSurvivor
{
    [RequireComponent(typeof(AudioSource))]
    public class AutoDisableAudioSource : MonoBehaviour
    {
        private AudioSource cachedAudioSource = null;

        private bool isDisabling = false;
        
        private void Awake() => cachedAudioSource = GetComponent<AudioSource>();

        private void Update()
        {
            if (!isDisabling && cachedAudioSource.clip != null)
            {
                Invoke(nameof(Disable), cachedAudioSource.clip.length);
                isDisabling = true;
            }
        }

        private void Disable()
        {
            gameObject.SetActive(false);
            isDisabling = false;
        }
    }
}