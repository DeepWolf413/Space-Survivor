using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Logger = DeepWolf.Logging.Logger;
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
		public static void Register<T>(T reference) where T : Object
		{
			if (referenceList.ContainsKey(typeof(T)))
			{ referenceList[typeof(T)].Add(reference); }
			else
			{ referenceList.Add(typeof(T), new List<Object>{reference}); }
		}

		/// <summary>
		/// Unregisters a reference from the manager.
		/// </summary>
		/// <typeparam name="T">The type of the reference.</typeparam>
		/// <param name="reference">The reference object itself.</param>
		public static void Unregister<T>(T reference) where T : Object
		{
			if (referenceList.ContainsKey(typeof(T)))
			{ referenceList[typeof(T)].Remove(reference); }
		}

		public static bool TryGet<T>(out T reference) where T : Object
		{
			if (referenceList.ContainsKey(typeof(T)))
			{
				if (referenceList[typeof(T)].Count > 0)
				{
					reference = (T)referenceList[typeof(T)][0];
					return true;
				}
			}

			Logger.LogError($"Failed to get reference of type '{typeof(T).Name}'.");
			reference = null;
			return false;
		}

		public static bool TryGetAll<T>(out List<T> references) where T : Object
		{
			if (referenceList.ContainsKey(typeof(T)))
			{
				if (referenceList[typeof(T)].Count > 0)
				{
					references = referenceList[typeof(T)] as List<T>;
					return true;
				}
			}

			references = null;
			return false;
		}
	}
}