using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class DealDamageOnHit : MonoBehaviour
    {
        [SerializeField]
        private float damageToApply = 25.0f;

        [SerializeField, Tooltip("Which tag this is allowed to damage. Leaving this empty means it will damage all objects with a health component.")]
        private string tagToDamage = string.Empty;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (tagToDamage != string.Empty && !other.CompareTag(tagToDamage))
            { return; }

            if (other.TryGetComponent(out Ship ship))
            { ship.ApplyDamage(damageToApply); }
            else if (other.TryGetComponent(out Health healthComponent))
            { healthComponent.ApplyDamage(damageToApply); }
        }
    }
}