using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class DifficultyButtonUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private WaveGenerationConfig difficulty = null;

        [Header("[Button]")]
        [SerializeField]
        private AudioClip selectedSfx = null;
        
        [SerializeField]
        private Image imageComponent = null;

        [SerializeField]
        private Sprite highlightedSprite = null;

        private Sprite normalSprite = null;
        
        private void Awake() => normalSprite = imageComponent.sprite;

        private void OnEnable()
        {
            GameManager.Instance.DifficultyChanged += OnDifficultyChanged;

            if (GameManager.Instance.SelectedDifficulty == difficulty)
            { HighlightButton(); }
        }

        private void OnDisable()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }
            
            GameManager.Instance.DifficultyChanged -= OnDifficultyChanged;
        }

        private void HighlightButton()
        {
            imageComponent.sprite = highlightedSprite;
            GameManager.SoundManager.PlayGlobalSound(selectedSfx, ESoundType.Sfx);
        }

        private void UnhighlightButton() => imageComponent.sprite = normalSprite;

        public void OnPointerClick(PointerEventData eventData) => GameManager.Instance.SelectDifficulty(difficulty);
        
        private void OnDifficultyChanged(WaveGenerationConfig newDifficulty)
        {
            if (newDifficulty == difficulty)
            { HighlightButton(); }
            else
            { UnhighlightButton(); }
        }
    }
}