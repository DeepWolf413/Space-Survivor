using DeepWolf.SpaceSurvivor.Managers;
using TMPro;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SpaceCreditsLabel : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI label = null;

        private void OnValidate()
        {
            if (!label)
            { label = GetComponent<TextMeshProUGUI>(); }
        }

        private void OnEnable()
        {
            GameManager.SaveManager.SaveState.SpaceCreditsChanged += OnSpaceCreditsChanged;
            RefreshLabel();
        }

        private void OnDisable()
        {
            if (!GameManager.IsApplicationQuitting)
            { GameManager.SaveManager.SaveState.SpaceCreditsChanged -= OnSpaceCreditsChanged; }
        }

        private void RefreshLabel() => label.text = GameManager.SaveManager.SaveState.SpaceCredits.ToString();

        private void OnSpaceCreditsChanged(int newValue, int oldValue) => RefreshLabel();
    }
}