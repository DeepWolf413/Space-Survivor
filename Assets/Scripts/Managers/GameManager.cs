using UnityEngine;
using UnityEngine.SceneManagement;

namespace DeepWolf.SpaceSurvivor.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField]
        private string levelSceneName = "Level";

        [SerializeField]
        private string mainSceneName = "MainMenu";
        
        public void LoadLevel() => SceneManager.LoadScene(levelSceneName);
        
        public void LoadMainScene() => SceneManager.LoadScene(mainSceneName);
    }
}