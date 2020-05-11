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
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (!playerObject)
            { return; }
            
            float travelSpeed = Random.Range(travelSpeedRange.x, travelSpeedRange.y);
            
            cachedRigidbody.velocity = (playerObject.transform.position - transform.position).normalized * travelSpeed;
            cachedRigidbody.angularVelocity = angularVelocity;
        }
    }
}