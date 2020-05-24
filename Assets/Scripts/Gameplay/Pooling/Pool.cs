using System.Collections.Generic;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class Pool : MonoBehaviour
    {
        [SerializeField]
        private PoolData data = null;
        
        private List<GameObject> pool = new List<GameObject>();

        #region Properties

        /// <summary>
        /// Gets the data of the pool.
        /// </summary>
        public PoolData Data => data;

        #endregion
        
        #region Unity callbacks

        private void Awake()
        {
            if (data != null)
            { Initialize(data); }
        }

        #endregion

        public void Initialize(PoolData poolData)
        {
            data = poolData;
            PoolManager.AddPool(this);
            
            if (data.Preload)
            { PreloadObjects(); }
        }
        
        #region Spawning/Despawning methods

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject spawnedObject = null;
            
            if (TryGetAvailableObject(out GameObject objectFound))
            {
                spawnedObject = objectFound;
                
                Transform objectTransform = spawnedObject.transform;
                objectTransform.position = position;
                objectTransform.rotation = rotation;
                spawnedObject.gameObject.SetActive(true);
            }
            else
            { spawnedObject = CreateObjectInPool(); }

            return spawnedObject;
        }

        private bool TryGetAvailableObject(out GameObject objectFound)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].gameObject.activeSelf)
                { continue; }
                    
                objectFound = pool[i];
                return true;
            }

            objectFound = null;
            return false;
        }

        #endregion

        #region Private methods

        private GameObject CreateObjectInPool(bool startInactive = false)
        {
            GameObject spawnedObject = Instantiate(data.Prefab, Vector3.zero, Quaternion.identity);
            spawnedObject.name = spawnedObject.name.Replace("(Clone)", string.Empty);
            spawnedObject.transform.SetParent(transform);
            pool.Add(spawnedObject);

            if (startInactive)
            { spawnedObject.SetActive(false); }

            return spawnedObject;
        }

        private void PreloadObjects()
        {
            for (int i = 0; i < data.PreloadAmount; i++)
            { CreateObjectInPool(true); }
        }

        #endregion
    }
}