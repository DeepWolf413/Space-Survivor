using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

namespace DeepWolf.SpaceSurvivor.Managers
{
    public class SceneManager : MonoSingleton<SceneManager>
    {
        [SerializeField]
        private string levelSceneName = "Level";

        [SerializeField]
        private string mainSceneName = "MainMenu";

        public bool IsChangingScene { get; private set; }
        
        protected override bool UseDontDestroyOnLoad => false;

        private void OnEnable() => UnitySceneManager.sceneLoaded += OnSceneLoaded;
        
        private void OnDisable() => UnitySceneManager.sceneLoaded -= OnSceneLoaded;

        public void LoadLevel()
        {
            IsChangingScene = true;
            UnitySceneManager.LoadScene(levelSceneName);
        }

        public void LoadMainScene()
        {
            IsChangingScene = true;
            UnitySceneManager.LoadScene(mainSceneName);
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode loadMode) => IsChangingScene = false;
    }
}