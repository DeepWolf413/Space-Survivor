using DeepWolf.SpaceSurvivor.Data;
using DeepWolf.SpaceSurvivor.Managers;
using DeepWolf.SpaceSurvivor.SaveSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Logger = DeepWolf.Logging.Logger;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class ShipButtonUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private PlayerShipData representedShip = null;

        [SerializeField]
        private Image icon = null;

        [SerializeField]
        private TextMeshProUGUI nameLabel = null;

        [SerializeField]
        private TextMeshProUGUI healthLabel = null;

        [SerializeField]
        private TextMeshProUGUI accelerationLabel = null;
        
        [SerializeField]
        private TextMeshProUGUI shieldLabel = null;

        [SerializeField]
        private TextMeshProUGUI priceLabel = null;

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
            RefreshUI();
            GameManager.SaveManager.SelectedShipChanged += OnSelectedShipChanged;
        }

        private void OnDisable()
        {
            if (GameManager.SaveManager)
            { GameManager.SaveManager.SelectedShipChanged -= OnSelectedShipChanged; }
        }

        private void RefreshUI()
        {
            if (!representedShip)
            {
                Logger.LogError("Failed to refresh UI. Missing reference to player ship data.");
                return;
            }
            
            icon.sprite = representedShip.Sprite;
            nameLabel.text = representedShip.DisplayName;
            healthLabel.text = representedShip.Health.ToString();
            accelerationLabel.text = representedShip.Speed.ToString();
            shieldLabel.text = representedShip.Shield.ToString();
            priceLabel.text = representedShip.Price.ToString();

            // Check if owned and then hide the price if it is.
            CheckOwnedState();
            HighlightButton();
            RefreshStatsColor();
        }

        private void RefreshStatsColor()
        {
            // Color stats based on the chosen ship.
            PlayerShipData chosenShip = GameManager.SaveManager.GetSelectedShip();
            if (chosenShip)
            {
                RefreshStatColor(healthLabel, chosenShip.Health, representedShip.Health);
                RefreshStatColor(accelerationLabel, chosenShip.Speed, representedShip.Speed);
                RefreshStatColor(shieldLabel, chosenShip.Shield, representedShip.Shield);
            }
            else
            {
                healthLabel.color = Color.white;
                accelerationLabel.color = Color.white;
                shieldLabel.color = Color.white;
            }
        }

        private void RefreshStatColor(TextMeshProUGUI statLabel, float chosenShipStat, float representedShipStat)
        {
            if (Mathf.Approximately(chosenShipStat, representedShipStat))
            { statLabel.color = Color.white; }
            else if (chosenShipStat > representedShipStat)
            { statLabel.color = Color.red; }
            else if (chosenShipStat < representedShipStat)
            { statLabel.color = Color.green; }
        }

        private void CheckOwnedState()
        {
            bool isShipOwned = GameManager.SaveManager.IsShipOwned(representedShip);
            if (isShipOwned)
            { priceLabel.gameObject.SetActive(false); }
        }

        private void HighlightButton()
        {
            if (GameManager.SaveManager.IsShipChosen(representedShip))
            { imageComponent.sprite = highlightedSprite; }
        }

        private void UnhighlightButton() => imageComponent.sprite = normalSprite;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (GameManager.SaveManager.IsShipOwned(representedShip))
            {
                GameManager.SaveManager.SelectShip(representedShip);

                if (selectedSfx != null)
                { GameManager.SoundManager.PlayGlobalSound(selectedSfx, ESoundType.Sfx); }
            }
            else
            {
                PlayerSaveState saveState = GameManager.SaveManager.SaveState;
                
                if (saveState.CanAfford(representedShip.Price))
                {
                    saveState.RemoveSpaceCredits(representedShip.Price);
                    saveState.AddShipAsOwned(representedShip.Id);
                    CheckOwnedState();
                }
            }
        }
        
        private void OnSelectedShipChanged(PlayerShipData newShip)
        {
            RefreshStatsColor();
            if (newShip == representedShip)
            { HighlightButton(); }
            else
            { UnhighlightButton(); }
        }
    }
}