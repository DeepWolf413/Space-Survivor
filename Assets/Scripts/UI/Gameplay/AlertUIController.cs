using DeepWolf.SpaceSurvivor.Gameplay;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class AlertUIController : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI alertLabel = null;

        private void OnEnable() => GameEvents.AsteroidsEventSpawned += OnAsteroidsEventSpawned;
        
        private void OnDisable() => GameEvents.AsteroidsEventSpawned -= OnAsteroidsEventSpawned;

        private void ShowAlert(string message)
        {
            alertLabel.gameObject.SetActive(true);
            alertLabel.text = message;
        }

        private void HideAlert()
        {
            alertLabel.alpha = 1.0f;
            alertLabel.DOFade(0.0f, 0.2f).onComplete += delegate
            { alertLabel.gameObject.SetActive(false); };
        }
        
        private void OnAsteroidsEventSpawned()
        {
            ShowAlert("Asteroids Incoming!");
            alertLabel.alpha = 0.0f;
            alertLabel.DOFade(1.0f, 0.2f);
            alertLabel.transform.DOPunchScale(new Vector3(0.15f, 0.15f), 0.4f);
            Invoke(nameof(HideAlert), 3.0f);
        }
    }
}