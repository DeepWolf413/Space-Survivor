using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class GiveSpaceCreditsOnDestroy : MonoBehaviour
    {
        [SerializeField]
        private int amountToGive = 25;

        private void OnDestroy()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }

            if (ReferenceManager.TryGet(out GameSession gameSession))
            { gameSession.AddSpaceCreditsReward(amountToGive); }
        }
    }
}