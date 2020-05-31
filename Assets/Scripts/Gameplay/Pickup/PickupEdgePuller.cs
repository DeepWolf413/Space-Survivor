using System;
using System.Collections.Generic;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    /// <summary>
    /// Pulls pickups that is out of the boundaries to the edge.
    /// </summary>
    public class PickupEdgePuller : MonoBehaviour
    {
        [SerializeField]
        private float pullSpeed = 20.0f;

        [SerializeField]
        private Vector2 screenPadding = new Vector2(0.05f, 0.05f);
        
        private Vector3 screenBounds = Vector3.zero;
        private Camera cameraComponent = null;

        private List<Transform> pickups = new List<Transform>();

        private void OnEnable()
        {
            GameEvents.PickupSpawned += OnPickupSpawned;
            GameEvents.PickupDespawned += OnPickupDespawned;
        }

        private void OnDisable()
        {
            if (GameManager.IsApplicationQuitting)
            { return; }
            
            GameEvents.PickupSpawned -= OnPickupSpawned;
            GameEvents.PickupDespawned -= OnPickupDespawned;
        }

        private void Start()
        {
            cameraComponent = Camera.main;
            screenBounds = cameraComponent.ViewportToWorldPoint(new Vector3(1.0f - screenPadding.x, 1.0f - screenPadding.y));
        }

        private void Update()
        {
            for (int i = 0; i < pickups.Count; i++)
            {
                if (IsPickupOutOfBounds(pickups[i].position))
                { Pull(pickups[i]); }
            }
        }

        private void Pull(Transform pickupToPull)
        {
            Vector3 pickupPos = pickupToPull.position;
            pickupToPull.position =
                Vector3.MoveTowards(pickupPos, GetClosestBoundaryPos(pickupPos), pullSpeed * Time.deltaTime);
        }
        
        private Vector3 GetClosestBoundaryPos(Vector3 position)
        {
            Vector3 clampedPos = position;
            clampedPos.x = Mathf.Clamp(clampedPos.x, -screenBounds.x, screenBounds.x);
            clampedPos.y = Mathf.Clamp(clampedPos.y, -screenBounds.y, screenBounds.y);
            clampedPos.z = 0.0f;
            return clampedPos;
        }

        private bool IsPickupOutOfBounds(Vector3 pickupPos) => pickupPos.x > screenBounds.x || pickupPos.x < -screenBounds.x ||
                                                               pickupPos.y > screenBounds.y || pickupPos.y < -screenBounds.y;

        private void OnPickupSpawned(Pickup pickup) => pickups.Add(pickup.transform);

        private void OnPickupDespawned(Pickup pickup) => pickups.Remove(pickup.transform);
    }
}