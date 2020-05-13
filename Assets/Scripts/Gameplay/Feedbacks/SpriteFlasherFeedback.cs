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
        protected float duration = 0.1f;

        protected Color originalColor = Color.white;

        private void OnDestroy() => DOTween.Kill(spriteRenderer);
        
        protected virtual void Start() => originalColor = spriteRenderer.color;

        public override void Play()
        {
            if (DOTween.IsTweening(spriteRenderer))
            { DOTween.Complete(spriteRenderer); }

            spriteRenderer.color = originalColor;
            
            float halfDuration = duration * 0.5f;
            spriteRenderer.DOColor(flashColor, halfDuration).onComplete += delegate
            {
                if (!spriteRenderer)
                { return; }
                spriteRenderer.DOColor(originalColor, halfDuration);
            };
        }
    }
}