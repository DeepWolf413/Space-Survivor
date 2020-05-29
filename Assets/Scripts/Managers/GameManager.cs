using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        #region Properties

        public static SceneManager SceneManager => SceneManager.Instance;

        public static SoundManager SoundManager => SoundManager.Instance;

        public static SaveManager SaveManager => SaveManager.Instance;

        public static bool IsApplicationQuitting { get; private set; }

        public static bool IsGamePaused => Mathf.Approximately(Time.timeScale, 0.0f);
        
        #endregion

        #region Unity callbacks

        private void OnEnable() => Application.quitting += OnApplicationQuitting;
        
        private void OnDisable() => Application.quitting -= OnApplicationQuitting;

        private void OnApplicationQuitting()
        {
            IsApplicationQuitting = true;
            SaveManager.SaveGame();
        }

        #endregion

        public static void PauseGame(bool pause) => Time.timeScale = pause ? 0.0f : 1.0f;
    }
}