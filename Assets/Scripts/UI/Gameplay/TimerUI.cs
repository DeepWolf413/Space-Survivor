﻿using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.Utilities;
using TMPro;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class TimerUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI label = null;

        private GameSession gameSession = null;

        private void Start()
        {
            ObjectUtilities.TryGetObjectOfType(out gameSession);
            InvokeRepeating(nameof(RefreshTimer), 0.05f, 1.0f);
        }

        private void RefreshTimer() => label.text = TimeUtilities.GetFormattedTime(gameSession.TimeSinceGameStarted, "{0}:{1}");
    }
}