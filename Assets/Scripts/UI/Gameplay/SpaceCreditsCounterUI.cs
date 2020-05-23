
using DeepWolf.SpaceSurvivor.Gameplay;
using DeepWolf.SpaceSurvivor.Managers;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.UI
{
    public class SpaceCreditsCounterUI : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI label = null;

        private void Start()
        {
            if (ReferenceManager.TryGet(out GameSession gameSession))
            { gameSession.SpaceCreditsCounterChanged += OnSpaceCreditsCounterChanged; }
        }

        private void OnSpaceCreditsCounterChanged(int newValue)
        {
            if (label == null)
            { return; }
            
            label.text = newValue.ToString();

            DOTween.Complete(label.transform);
            label.transform.localScale = Vector3.one;
            label.transform.DOPunchScale(new Vector3(0.3f, 0.3f, 0.0f), 0.3f);
        }
    }
}