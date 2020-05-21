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
    }
}