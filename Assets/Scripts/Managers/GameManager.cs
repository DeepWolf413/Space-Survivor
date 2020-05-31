using System;
using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.SaveSystem;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Managers
{
    [DefaultExecutionOrder(-1000)]
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private WaveGenerationConfig defaultDifficulty = null;

        #region Properties

        public static SceneManager SceneManager => SceneManager.Instance;

        public static SoundManager SoundManager => SoundManager.Instance;

        public static SaveManager SaveManager => SaveManager.Instance;

        public static bool IsApplicationQuitting { get; private set; }

        public static bool IsGamePaused => Mathf.Approximately(Time.timeScale, 0.0f);
        
        public WaveGenerationConfig SelectedDifficulty { get; private set; }
        
        #endregion

        #region Events

        public event Action<WaveGenerationConfig> DifficultyChanged = delegate { };

        #endregion

        #region Unity callbacks

        protected override void Awake()
        {
            base.Awake();
            SelectDifficulty(defaultDifficulty);
        }

        private void OnEnable() => Application.quitting += OnApplicationQuitting;
        
        private void OnDisable() => Application.quitting -= OnApplicationQuitting;

        private void OnApplicationQuitting()
        {
            IsApplicationQuitting = true;
            SaveManager.SaveGame();
        }

        #endregion

        public void SelectDifficulty(WaveGenerationConfig difficulty)
        {
            if (SelectedDifficulty == difficulty)
            { return; }
            
            SelectedDifficulty = difficulty;
            DifficultyChanged?.Invoke(SelectedDifficulty);
        }

        public static void PauseGame(bool pause) => Time.timeScale = pause ? 0.0f : 1.0f;
    }
}