using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.Feedbacks
{
    public class FeedbackPlayer : MonoBehaviour
    {
        [SerializeField]
        private Feedback[] feedbacks = new Feedback[0];

        private void OnValidate() => feedbacks = GetComponents<Feedback>();

        public void Play()
        {
            for (int i = 0; i < feedbacks.Length; i++)
            { feedbacks[i].Play(); }
        }
    }
}