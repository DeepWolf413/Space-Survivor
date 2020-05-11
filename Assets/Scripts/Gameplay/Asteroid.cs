using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : MonoBehaviour
    {
        [SerializeField]
        private Vector2 travelSpeedRange = new Vector2(3.0f, 5.0f);

        [SerializeField]
        private float angularVelocity = 100.0f;

        private Rigidbody2D cachedRigidbody = null;

        private void Awake()
        {
            cachedRigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Transform player = GameObject.FindWithTag("Player").transform;
            float travelSpeed = Random.Range(travelSpeedRange.x, travelSpeedRange.y);
            
            cachedRigidbody.velocity = (player.position - transform.position).normalized * travelSpeed;
            cachedRigidbody.angularVelocity = angularVelocity;
        }
    }
}