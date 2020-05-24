using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class Shredder : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other) => PoolManager.Despawn(other.gameObject);
    }
}