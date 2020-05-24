using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private List<Pool> pools = new List<Pool>();

        #region Unity callbacks

        private void OnEnable() => SceneManager.activeSceneChanged += OnActiveSceneChanged;
        
        private void OnDisable() => SceneManager.activeSceneChanged += OnActiveSceneChanged;

        #endregion

        /// <summary>
        /// Spawns a gameobject from the specified pool.
        /// </summary>
        /// <param name="poolData">The pool to spawn from.</param>
        /// <param name="position">The position to place the gameobject at.</param>
        /// <param name="rotation">The rotation to place the gameobject on.</param>
        /// <returns>The gameobject that was spawned from the pool.</returns>
        public static GameObject Spawn(PoolData poolData, Vector3 position, Quaternion rotation) =>
            Instance.GetPool(poolData).Spawn(position, rotation);
        
        /// <summary>
        /// Spawns a gameobject from the specified pool. This will use GetComponent() to get the wanted component.
        /// </summary>
        /// <param name="poolData">The pool to spawn from.</param>
        /// <param name="position">The position to place the gameobject at.</param>
        /// <param name="rotation">The rotation to place the gameobject on.</param>
        /// <typeparam name="T">Which component to return.</typeparam>
        /// <returns>The component of type <typeparamref name="T"/> from the gameobject that was spawned from the pool.</returns>
        public static T Spawn<T>(PoolData poolData, Vector3 position, Quaternion rotation) where T : Object =>
            Instance.GetPool(poolData).Spawn(position, rotation).GetComponent<T>();

        /// <summary>
        /// Despawns the <paramref name="objectToDespawn"/>.
        /// </summary>
        /// <param name="objectToDespawn"></param>
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

        /// <summary>
        /// Creates a gameobject and adds a component of type <see cref="Pool"/> to it. Then adds it to the <see cref="pools"/> list.
        /// </summary>
        /// <param name="poolData">The pool to create.</param>
        /// <returns>The <see cref="Pool"/> gameobject that was created.</returns>
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