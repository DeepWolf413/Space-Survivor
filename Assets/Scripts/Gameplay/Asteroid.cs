﻿using System;
using DeepWolf.SpaceSurvivor.Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField]
        private Vector2 travelSpeedRange = new Vector2(3.0f, 5.0f);

        [SerializeField]
        private float angularVelocity = 100.0f;

        [SerializeField]
        private bool setInitialVelocity = true;
        
        [Header("[Destruction]")]
        [SerializeField]
        private bool spawnPiecesOnDestroy = false;

        [SerializeField]
        private float piecesMaxSpawnDistance = 1.0f;

        [SerializeField, Tooltip("The amount of pieces to spawn when this asteroid is destroyed. NOTE: This is only used if 'Spawn Pieces On Destroy' is enabled.")]
        private int piecesSpawnAmount;

        [SerializeField, Tooltip("An array of possible pieces to spawn when this asteroid is destroyed. NOTE: This is only used if 'Spawn Pieces On Destroy' is enabled.")]
        private Rigidbody2D[] piecesToSpawn = new Rigidbody2D[0];

        [SerializeField]
        private float explosionForce = 1.5f;

        private Rigidbody2D cachedRigidbody = null;

        private void Awake()
        {
            cachedRigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (!playerObject)
            { return; }

            if (setInitialVelocity)
            {
                float travelSpeed = Random.Range(travelSpeedRange.x, travelSpeedRange.y);
                cachedRigidbody.velocity = (playerObject.transform.position - transform.position).normalized * travelSpeed;
                cachedRigidbody.angularVelocity = angularVelocity;
            }
        }

        private void OnDestroy()
        {
            if (!spawnPiecesOnDestroy)
            { return; }

            if (!GameManager.Instance || GameManager.Instance.IsApplicationQuitting)
            { return; }
            
            SpawnPieces();
        }

        private void SpawnPieces()
        {
            for (int i = 0; i < piecesSpawnAmount; i++)
            {
                Vector3 spawnPos = transform.position + (Vector3)Random.insideUnitCircle * piecesMaxSpawnDistance;
                Rigidbody2D asteroidPiece = Instantiate(GetRndPiece(), spawnPos, Quaternion.identity);

                Vector2 direction = asteroidPiece.transform.position - transform.position;
                asteroidPiece.AddForceAtPosition(direction.normalized * explosionForce, transform.position, ForceMode2D.Impulse);
            }
        }

        private Rigidbody2D GetRndPiece() => piecesToSpawn[Random.Range(0, piecesToSpawn.Length)];
    }
}