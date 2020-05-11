﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor
{
    /// <summary>
    /// Represents a generic singleton class for <see cref="MonoBehaviour"/>s.
    /// </summary>
    /// <typeparam name="T">The type of the class that inherits from this class.</typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                { instance = FindObjectOfType<T>(); }
                
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (Instance != this)
            { Destroy(gameObject); }
            else
            { DontDestroyOnLoad(gameObject); }
        }
    }
}