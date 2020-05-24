using UnityEngine;
using Random = UnityEngine.Random;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private PoolData[] asteroidPools = null;
        
        private Camera cachedCamera = null;
        
        private void Awake() => cachedCamera = Camera.main;

        public void SpawnAsteroid()
        {
            PoolData asteroidPool = asteroidPools[Random.Range(0, asteroidPools.Length)];
            Spawn(asteroidPool);
        }
        
        public GameObject Spawn(GameObject prefab, float offsetFromScreenEdge = 5.0f)
        {
            return Instantiate(prefab, GetRndPosOutsideScreen(), Quaternion.identity);
        }
        
        public GameObject Spawn(GameObject prefab, EScreenEdge screenEdge, float offsetFromScreenEdge = 5.0f)
        {
            return Instantiate(prefab, GetRndPosOutsideScreen(screenEdge, offsetFromScreenEdge), Quaternion.identity);
        }
        
        public GameObject Spawn(PoolData pool, float offsetFromScreenEdge = 5.0f)
        {
            return PoolManager.Spawn(pool, GetRndPosOutsideScreen(), Quaternion.identity);
        }
        
        public GameObject Spawn(PoolData pool, EScreenEdge screenEdge, float offsetFromScreenEdge = 5.0f)
        {
            return PoolManager.Spawn(pool, GetRndPosOutsideScreen(screenEdge, offsetFromScreenEdge), Quaternion.identity);
        }
        
        private Vector2 GetRndPosOutsideScreen(EScreenEdge screenEdge, float offsetFromEdge = 5.0f)
        {
            Vector3 screenBounds = cachedCamera.ViewportToWorldPoint(Vector2.one);
            float posX = 0.0f;
            float posY = 0.0f;
            
            switch (screenEdge)
            {
                case EScreenEdge.Top:
                    posX = Random.Range(-screenBounds.x, screenBounds.x);
                    posY = screenBounds.y + offsetFromEdge;
                    break;
                case EScreenEdge.Right:
                    posX = screenBounds.x + offsetFromEdge;
                    posY = Random.Range(-screenBounds.y, screenBounds.y);
                    break;
                case EScreenEdge.Bottom:
                    posX = Random.Range(-screenBounds.x, screenBounds.x);
                    posY = -screenBounds.y - offsetFromEdge;
                    break;
                case EScreenEdge.Left:
                    posX = -screenBounds.x - offsetFromEdge;
                    posY = Random.Range(-screenBounds.y, screenBounds.y);
                    break;
            }
            
            return new Vector2(posX, posY);
        }
        
        private Vector2 GetRndPosOutsideScreen(float offsetFromEdge = 5.0f) => GetRndPosOutsideScreen((EScreenEdge)Random.Range(0, 4), offsetFromEdge);
    }
}