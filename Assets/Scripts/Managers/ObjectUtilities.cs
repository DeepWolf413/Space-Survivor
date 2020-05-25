using UnityEngine;

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
			return objectFound != null;
		}
		
		public static bool TryGetObjectsOfType<T>(out T[] objectsFound) where T : Object
		{
			objectsFound = Object.FindObjectsOfType<T>();
			return objectsFound != null;
		}
		
		public static bool TryGetWithTag(string tag, out GameObject objectFound)
		{
			objectFound = GameObject.FindWithTag(tag);
			return objectFound != null;
		}
		
		public static bool TryGetObjectsWithTag(string tag, out GameObject[] objectsFound)
		{
			objectsFound = GameObject.FindGameObjectsWithTag(tag);
			return objectsFound != null;
		}
	}
}