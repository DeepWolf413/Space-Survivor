using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

using Type = System.Type;

namespace DeepWolf.SpaceSurvivor.Managers
{
	public static class ReferenceManager
	{
		private static Dictionary<Type, List<Object>> referenceList;

		static ReferenceManager()
		{
			referenceList = new Dictionary<Type, List<Object>>();
			UnitySceneManager.activeSceneChanged += SceneManager_ActiveSceneChanged;
		}

		private static void SceneManager_ActiveSceneChanged(Scene arg0, Scene arg1) => referenceList.Clear();

		/// <summary>
		/// Registers a reference to the manager.
		/// </summary>
		/// <typeparam name="T">The type of the reference.</typeparam>
		/// <param name="reference">The reference object itself.</param>
		public static void Register<T>(T reference) where T : Object => referenceList[typeof(T)].Add(reference);
		
		/// <summary>
		/// Unregisters a reference from the manager.
		/// </summary>
		/// <typeparam name="T">The type of the reference.</typeparam>
		/// <param name="reference">The reference object itself.</param>
		public static void Unregister<T>(T reference) where T : Object => referenceList[typeof(T)].Remove(reference);

		public static T Get<T>() where T : Object
		{
			if (referenceList.ContainsKey(typeof(T)))
			{
				if (referenceList[typeof(T)].Count > 0)
				{ return (T) referenceList[typeof(T)][0]; }
			}

			return null;
		}

		public static List<T> GetAll<T>() where T : Object
		{
			if (referenceList.ContainsKey(typeof(T)))
			{
				if (referenceList[typeof(T)].Count > 0)
				{ return referenceList[typeof(T)] as List<T>; }
			}

			return null;
		}
	}
}