using System;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DeepWolf.SpaceSurvivor.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonSFX : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        [SerializeField]
        private AudioClip pointerEnterSfx = null;

        [SerializeField]
        private AudioClip pointerClickSfx = null;

        private Button buttonComponent = null;

        private void Awake() => buttonComponent = GetComponent<Button>();

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (pointerEnterSfx == null || !buttonComponent.IsInteractable())
            { return; }
            
            GameManager.SoundManager.PlayGlobalSound(pointerEnterSfx, ESoundType.Sfx);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (pointerClickSfx == null || !buttonComponent.IsInteractable())
            { return; }
            
            GameManager.SoundManager.PlayGlobalSound(pointerClickSfx, ESoundType.Sfx);
        }
    }
}