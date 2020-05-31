using System.Collections.Generic;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    /// <summary>
    /// Pulls pickups that is out of the boundaries to the edge.
    /// </summary>
    public class PickupEdgePuller : MonoBehaviour
    {
        [SerializeField]
        private Vector2 screenPadding = new Vector2(0.05f, 0.05f);
        
        private Vector3 screenBounds = Vector3.zero;
        private Camera cameraComponent = null;

        private List<Pickup> pickups = new List<Pickup>();
        
        private void Start()
        {
            cameraComponent = Camera.main;
            screenBounds = cameraComponent.ViewportToWorldPoint(new Vector3(1.0f - screenPadding.x, 1.0f - screenPadding.y));
        }
    }
}