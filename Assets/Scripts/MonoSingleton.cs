using System.Collections;
using System.Collections.Generic;
using DeepWolf.SpaceSurvivor.Utilities;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor
{
    /// <summary>
    /// Represents a generic singleton class for <see cref="MonoBehaviour"/>s.
    /// </summary>
    /// <typeparam name="T">The type of the class that inherits from this class.</typeparam>
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null)
                { return instance; }
                
                return ObjectUtilities.TryGetObjectOfType(out instance) ? instance : null;
            }
        }

        protected virtual bool DontDestroyOnLoad => true;

        protected virtual void Awake()
        {
            if (Instance != this)
            { Destroy(gameObject); }
            else
            {
                if (DontDestroyOnLoad)
                { DontDestroyOnLoad(gameObject); }
            }
        }
    }
}