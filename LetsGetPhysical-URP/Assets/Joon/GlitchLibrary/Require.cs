using UnityEngine;
using System.Collections.Generic;

// from Marco Bancale - march 2018
// edits by Joon
public class Require 
{

	public static T Component<T>(Component target) where T: Component
	{
		return Component<T>(target.gameObject);
	}

	public static T Component<T>(GameObject target)  where T: Component
	{
		T component = target.GetComponent<T>();
		if (component == null)
		{
			Debug.LogError("Component of type " + typeof(T).Name + " required on " + target, target);
		}

		return component;
	}

	public static T ComponentInChildren<T>(Component target) where T: Component
	{
		return ComponentInChildren<T>(target.gameObject);
	}

	public static T ComponentInChildren<T>(GameObject target) where T: Component
	{
		T component = target.GetComponentInChildren<T>();
		if (component == null)
		{
			Debug.LogError(target + " requires component of type '" + typeof(T).Name + "' in children.", target);
		}
		
		return component;
	}

	public static T[] ComponentsInChildren<T>(Component target, bool includeInactive = false) where T: Component
	{
		T[] components = target.GetComponentsInChildren<T>(includeInactive: includeInactive);
		if (components.Length == 0)
		{
			Debug.LogError(target + " requires component of type '" + typeof(T).Name + "' in children.", target);
		}
		
		return components;
	}

	public static T[] ComponentsInChildren<T>(Component target, int depth, bool includeInactive = false) where T: Component
	{
		return RecurseGetComponentInChildren<T>(target.transform, depth, 0);
	}

	static T[] RecurseGetComponentInChildren<T>(Transform target, int depth, int currentDepth) where T:Component
	{
		currentDepth++;
		if(currentDepth > depth) 
		{
			return new T[0];
		}

		List<T>	list = new List<T>();
		foreach (Transform child in target)
		{
			T component = child.GetComponent<T>();
			if(component != null)
			{
				list.Add(component);
			}
			list.AddRange(RecurseGetComponentInChildren<T>(target, depth, currentDepth));
		}

		return list.ToArray();
	}


	public static T ComponentInParent<T>(Component target) where T: Component
	{
		return ComponentInParent<T>(target.gameObject);
	}

	public static T ComponentInParent<T>(GameObject target) where T: Component
	{
		T component = target.GetComponentInParent<T>();
		if (component == null)
		{
			Debug.LogError(target + " requires component of type '" + typeof(T).Name + "' in parent.", target);
		}
		
		return component;
	}
	
	public static Transform ChildWithTag(string tag, Component target)
	{
		Transform found = null;
		foreach (Transform child in target.transform)
		{
			if (child.CompareTag(tag))
			{
				if (found != null)
				{
					Debug.LogError(target + " has multiple children tagged '" + tag + "'.", target);
				}
				found = child;
			}
		}

		if (found != null)
		{
			return found;
		}
		else
		{
			Debug.LogError(target + " requires child with tag '" + tag + "'.", target);
		}
		// Will never be reached
		return null;
	}
	
	public static Transform ChildWithTagDeep(string tag, Component target, bool optional = false)
	{
		Transform found = null;
		foreach (Transform child in target.transform.GetComponentsInChildren<Transform>())
		{
			if (child.CompareTag(tag) == true)
			{
				if (found != null)
				{
					Debug.LogError(target + " has multiple children tagged '" + tag + "'.", target);
				}
				found = child;
			}
		}

		if (found != null)
		{
			return found;
		}
		else if (optional == false)
		{
			Debug.LogError(target + " requires child with tag '" + tag + "'.", target);
		}

		return null;
	}

		
	public static T ChildWithTagDeep<T>(string tag, Component target, bool optional = false)
	{
		Transform found = null;
		foreach (Transform child in target.transform.GetComponentsInChildren<Transform>())
		{
			if (child.CompareTag(tag) == true)
			{
				if (found != null)
				{
					Debug.LogError(target + " has multiple children tagged '" + tag + "'.", target);
				}
				found = child;
			}
		}

		if (found != null)
		{
			var component = found.GetComponent<T>();
			if(component == null)
			{
				Debug.LogError(found + " requires component '" + typeof(T).ToString() + "'.", found);
			}
			return component;
		}
		else if (optional == false)
		{
			Debug.LogError(target + " requires child with tag '" + tag + "'.", target);
		}

		return default(T);
	}

	public static GameObject UniqueGameObjectWithTag(string tag)
	{
		GameObject[] found = GameObject.FindGameObjectsWithTag(tag);
		if (found.Length > 1)
		{
			Debug.LogError("More than one object with tag " + tag + " found.", found[0]);
		}
		else if (found.Length == 0)
		{
			Debug.LogError("No objects found with tag " + tag + ".");
		}
		else 
		{
			return found[0];
		}
		// Will never be reached
		return null;
	}

	public static GameObject UniqueGameObjectWithScript<T>()
	{
		return (UniqueScript<T>() as Component).gameObject;
	}

	public static T UniqueScript<T>()
	{
		T[] found = Object.FindObjectsOfType(typeof (T)) as T[];
		if (found.Length > 1)
		{
			Debug.LogError("More than one object with script " + typeof(T).Name + " found.");
		}
		else if (found.Length == 0)
		{
			Debug.LogError("No objects found with script " + typeof(T).Name + ".");
		}
		else 
		{
			return found[0];
		}
		// Will never be reached
		return default(T);
	}

	public static Collider2D ColliderInLayer(string layer, Component target)
	{
		return ColliderInLayer(layer, target.gameObject);
	}

	public static Collider2D ColliderInLayer(string layer, GameObject target)
	{
		Collider2D[] colliders = target.GetComponentsInChildren<Collider2D>(target);
		foreach (Collider2D collider in colliders)
		{
			if (collider.gameObject.layer == LayerMask.NameToLayer(layer))
			{
				return collider;
			}
		}
		Debug.LogError(target + " requires collider in '" + layer + "' layer.", target);
		// never reached
		return null;
	}
}
