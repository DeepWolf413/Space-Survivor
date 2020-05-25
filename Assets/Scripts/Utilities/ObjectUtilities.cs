using UnityEngine;
using Logger = DeepWolf.Logging.Logger;

namespace DeepWolf.SpaceSurvivor.Utilities
{
	/// <summary>
	/// Provides a few utilities for the <see cref="Object"/> class.
	/// This includes stuff for <see cref="Object.FindObjectOfType"/>, <see cref="GameObject.FindWithTag"/>, etc.
	/// </summary>
	public static class ObjectUtilities
	{
		public static bool TryGetObjectOfType<T>(out T objectFound) where T : Object
		{
			objectFound = Object.FindObjectOfType<T>();
			if (objectFound == null)
			{ Logger.LogError($"Couldn't find an object of type '{typeof(T).Name}'."); }
			
			return objectFound != null;
		}
		
		public static bool TryGetObjectsOfType<T>(out T[] objectsFound) where T : Object
		{
			objectsFound = Object.FindObjectsOfType<T>();
			if (objectsFound == null || objectsFound.Length == 0)
			{ Logger.LogError($"Couldn't find any objects of type '{typeof(T).Name}'."); }
			
			return objectsFound != null && objectsFound.Length > 0;
		}
		
		public static bool TryGetWithTag(string tag, out GameObject objectFound)
		{
			objectFound = GameObject.FindWithTag(tag);
			if (objectFound == null)
			{ Logger.LogError($"Couldn't find an object with tag '{tag}'."); }
			
			return objectFound != null;
		}
		
		public static bool TryGetObjectsWithTag(string tag, out GameObject[] objectsFound)
		{
			objectsFound = GameObject.FindGameObjectsWithTag(tag);
			if (objectsFound == null)
			{ Logger.LogError($"Couldn't find any objects with tag '{tag}'."); }
			
			return objectsFound != null;
		}
	}
}