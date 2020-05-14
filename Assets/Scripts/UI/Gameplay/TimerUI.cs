using DeepWolf.SpaceSurvivor.Gameplay;
using TMPro;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI label = null;

        private GameSession gameSession = null;

        private void OnDisable() => CancelInvoke(nameof(RefreshTimer));

        private void Start()
        {
            gameSession = FindObjectOfType<GameSession>();
            InvokeRepeating(nameof(RefreshTimer), 0.05f, 1.0f);
        }

        private void RefreshTimer() => label.text = GetFormattedTimer();

        private string GetFormattedTimer()
        {
            float time = gameSession.TimeSinceGameStarted;
            float minutes = Mathf.Floor(time / 60);
            float seconds = Mathf.Floor(time % 60);
            return $"{minutes:00}:{seconds:00}";
        }
    }
}