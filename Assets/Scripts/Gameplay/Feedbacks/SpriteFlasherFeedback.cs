using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.Feedbacks
{
    public class SpriteFlasherFeedback : Feedback
    {
        [SerializeField]
        protected SpriteRenderer spriteRenderer = null;

        [SerializeField]
        protected Color flashColor = Color.red;

        [SerializeField]
        private int flashAmount = 2;

        [SerializeField]
        protected float duration = 0.1f;

        protected Coroutine flashCoroutine = null;

        protected Color originalColor = Color.white;
        
        private void OnDisable()
        {
            if (flashCoroutine != null)
            {
                StopCoroutine(flashCoroutine);
                flashCoroutine = null;
            }
            
            spriteRenderer.color = originalColor;
        }
        
        protected virtual void Start() => originalColor = spriteRenderer.color;

        public override void Play()
        {
            if (flashCoroutine != null || !gameObject.activeInHierarchy)
            { return; }

            // TODO: Fix Coroutine couldn't be started because the game object 'Damaged Feedback' is inactive!. We check above for it, but it doesn't seem to work.
            // Now I just changed it to activeInHierarchy to see if it changes anything. Hopefully it fixes it.
            flashCoroutine = StartCoroutine(Flash());
        }

        private IEnumerator Flash()
        {
            float splitDuration = duration / flashAmount;

            for (int i = 0; i < flashAmount; i++)
            {
                spriteRenderer.color = flashColor;
                yield return new WaitForSeconds(splitDuration);
                spriteRenderer.color = originalColor;
                yield return new WaitForSeconds(splitDuration);
            }

            flashCoroutine = null;
        }
    }
}