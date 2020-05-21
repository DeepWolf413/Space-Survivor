using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Managers
{
    public class SceneManager : MonoSingleton<SceneManager>
    {
        [SerializeField]
        private string levelSceneName = "Level";

        [SerializeField]
        private string mainSceneName = "MainMenu";

        protected override bool DontDestroyOnLoad => false;

        public void LoadLevel() => UnityEngine.SceneManagement.SceneManager.LoadScene(levelSceneName);
        
        public void LoadMainScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(mainSceneName);
    }
}