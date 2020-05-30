using UnityEngine;

namespace DeepWolf.SpaceSurvivor
{
    [System.Serializable]
    public class BestTimeEntry
    {
        [SerializeField]
        private string difficultyName;

        [SerializeField]
        private float time;
        
        public BestTimeEntry(string difficultyName, float time = 0.0f)
        {
            DifficultyName = difficultyName;
            Time = time;
        }

        public string DifficultyName
        {
            get => difficultyName;
            private set => difficultyName = value;
        }

        public float Time
        {
            get => time;
            private set => time = value;
        }

        public bool SetTime(float newTime)
        {
            if (Time >= newTime)
            { return false; }
            
            Time = newTime;
            return true;
        }
    }
}