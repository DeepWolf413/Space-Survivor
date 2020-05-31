using DeepWolf.SpaceSurvivor.Managers;
using DG.Tweening;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public abstract class Pickup : MonoBehaviour
    {
        [Header("[General]")]
        [SerializeField, Tooltip("How many seconds it takes before the pickup despawns.")]
        protected float despawnDelay = 10.0f;

        [SerializeField, Tooltip("How many seconds before the pickup despawns should the warning start.")]
        protected float despawnWarningTime = 3.0f;

        [SerializeField]
        protected AudioClip pickupSfx = null;

        [Header("[Bounce]")]
        [SerializeField]
        protected float scaleDuration = 0.5f;

        [SerializeField]
        protected float scaleValue = 0.8f;

        [Header("[Despawning]")]
        [SerializeField]
        protected float fadeBlinkDuration = 0.3f;

        [SerializeField]
        protected SpriteRenderer spriteRenderer;

        #region Unity callbacks

        protected virtual void OnValidate()
        {
            if (!spriteRenderer)
            { spriteRenderer = GetComponentInChildren<SpriteRenderer>(); }
        }

        protected virtual void OnEnable()
        {
            Invoke(nameof(Despawn), despawnDelay);
            Invoke(nameof(StartDespawnWarning), despawnWarningTime);
            transform.DOScale(scaleValue, scaleDuration).SetLoops(-1, LoopType.Yoyo);
        }
        
        protected virtual void OnDisable()
        {
            if (GameManager.IsApplicationQuitting || GameManager.SceneManager.IsChangingScene)
            { return; }
            
            CancelInvoke(nameof(Despawn));
            CancelInvoke(nameof(StartDespawnWarning));
            StopDespawnWarning();
            DOTween.Kill(transform);
            GameEvents.SignalPickupDespawned(this);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            { Take(other.gameObject); }
        }

        #endregion

        public virtual void Take(GameObject playerObject)
        {
            if (pickupSfx != null)
            { GameManager.SoundManager.PlayGlobalSound(pickupSfx, ESoundType.Sfx); }
            
            PoolManager.Despawn(gameObject);
        }

        protected virtual void Despawn() => PoolManager.Despawn(gameObject);

        protected void StartDespawnWarning() => spriteRenderer.DOFade(0.3f, fadeBlinkDuration).SetLoops(-1, LoopType.Yoyo);
        
        protected void StopDespawnWarning() => DOTween.Kill(spriteRenderer);
    }
}