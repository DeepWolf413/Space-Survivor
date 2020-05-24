using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    // TODO: Get rid of garbage collection, improve, and refactor.
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private List<Pool> pools = new List<Pool>();

        #region Unity callbacks

        private void OnEnable() => SceneManager.activeSceneChanged += OnActiveSceneChanged;
        
        private void OnDisable() => SceneManager.activeSceneChanged += OnActiveSceneChanged;

        #endregion

        public static GameObject Spawn(PoolData poolData, Vector3 position, Quaternion rotation) =>
            Instance.GetPool(poolData).Spawn(position, rotation);
        
        public static T Spawn<T>(PoolData poolData, Vector3 position, Quaternion rotation) where T : Object =>
            Instance.GetPool(poolData).Spawn(position, rotation).GetComponent<T>();

        public static void Despawn(GameObject objectToDespawn) => objectToDespawn.SetActive(false);

        /// <summary>
        /// Adds a <see cref="Pool"/>.
        /// This is called automatically in <see cref="Pool"/>.
        /// </summary>
        /// <param name="pool">The pool to add.</param>
        public static void AddPool(Pool pool)
        {
            if (!Instance.pools.Contains(pool))
            { Instance.pools.Add(pool); }

            pool.transform.SetParent(Instance.transform);
        }

        private static Pool CreatePool(PoolData poolData)
        {
            GameObject poolObject = new GameObject($"Pool - {poolData.name}");
            Pool pool = poolObject.AddComponent<Pool>();
            pool.Initialize(poolData);
            return pool;
        }

        public Pool GetPool(PoolData poolData)
        {
            for (int i = 0; i < pools.Count; i++)
            {
                if (pools[i].Data.Id == poolData.Id)
                { return pools[i]; }
            }

            return CreatePool(poolData);
        }

        #region Event listeners

        private void OnActiveSceneChanged(Scene prevScene, Scene currentScene)
        {
            for (int i = 0; i < pools.Count; i++)
            { Destroy(pools[i].gameObject); }
            
            pools.Clear();
        }

        #endregion
    }
}