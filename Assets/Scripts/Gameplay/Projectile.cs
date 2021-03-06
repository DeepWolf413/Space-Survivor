﻿using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float speed = 5.0f;
        
        [SerializeField, Tooltip("Which tag this is allowed to damage. Leaving this empty means it will damage all objects with a health component.")]
        private string tagToDamage = string.Empty;

        [SerializeField]
        private PoolData destroyFXPool = null;

        private Rigidbody2D cachedRigidbody = null;

        private void Awake() => cachedRigidbody = GetComponent<Rigidbody2D>();

        private void OnEnable() => cachedRigidbody.velocity = transform.up * speed;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (tagToDamage != string.Empty && !other.CompareTag(tagToDamage))
            { return; }

            if (destroyFXPool)
            { PoolManager.Spawn(destroyFXPool, transform.position, Quaternion.identity); }

            PoolManager.Despawn(gameObject);
        }
    }
}