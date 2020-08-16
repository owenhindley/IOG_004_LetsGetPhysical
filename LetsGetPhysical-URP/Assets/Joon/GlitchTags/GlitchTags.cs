using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;

public class GlitchTags
{
	static bool _DEBUG = false;
	
	const string localLookup = "_this";
	[UnityEditor.Callbacks.DidReloadScripts]
	public static void OnReloadScripts()
	{
		ReloadGlitchTags();
	}

	public static void ResetGlitchTags()
	{
		// Find all types in the world
		var types = System.AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(MonoBehaviour));
		foreach (var type in types)
		{
			var fields = type.GetFields().Where(
				field=> Attribute.IsDefined(field, typeof(GlitchTagAttribute)));
			foreach (var field in fields)
			{
				if(_DEBUG) Debug.Log("Found field: " + field.Name + ", in: " + type.Name);

				// Find objects that use this type
				var fieldTag = field.GetCustomAttribute<GlitchTagAttribute>().tag;
				var objectsLookingForGlitchTags = GameObject.FindObjectsOfType(type);
				foreach (var objectLookingForGlitchTag in objectsLookingForGlitchTags)
				{
					field.SetValue(objectLookingForGlitchTag, null);
					UnityEditor.EditorUtility.SetDirty(objectLookingForGlitchTag);
				}
			}
		} 

	}
	
	// TODO: make better
	static int GetHierarchyIndex(Transform t)
	{
		if (t.parent != null)
		{
			return GetHierarchyIndex(t.parent) * 1000 + t.GetSiblingIndex();
		}

		return t.GetSiblingIndex();
	}
	
	public static void ReloadGlitchTags()
	{
		// Find all tags in the world
		var glitchTagDirectory = GameObject.FindObjectsOfType<GlitchTag>().OrderBy(gt=>GetHierarchyIndex(gt.transform));

		// Find all types in the world
		var types = System.AppDomain.CurrentDomain.GetAllDerivedTypes(typeof(MonoBehaviour));
		foreach (var type in types)
		{
			var fields = type.GetFields().Where(
				field=> Attribute.IsDefined(field, typeof(GlitchTagAttribute)));
			foreach (var field in fields)
			{
				if(_DEBUG) Debug.Log("Found field: " + field.Name + ", in: " + type.Name);

				// Find objects that use this type
				var fieldTag = field.GetCustomAttribute<GlitchTagAttribute>().tag;


				var relevantGlitchTags = glitchTagDirectory.Where(t =>  t.tag1 == fieldTag || t.tag2 == fieldTag || t.tag3 == fieldTag );
				var objectsLookingForGlitchTags = GameObject.FindObjectsOfType(type);
				foreach (var objectLookingForGlitchTag in objectsLookingForGlitchTags)
				{
					if (fieldTag == localLookup)
					{
						// For finding rigidbody of itself...
						// Single item
						var value = (objectLookingForGlitchTag  as Component).GetComponent(field.FieldType);
						field.SetValue(objectLookingForGlitchTag, value);
						UnityEditor.EditorUtility.SetDirty(objectLookingForGlitchTag);
						continue;
					}


					
					if (relevantGlitchTags == null || !relevantGlitchTags.Any())
					{
						Debug.LogWarning("Couldn't satisfy: " + fieldTag);
					}
					else
					{ 
						if (field.FieldType.IsArray)
						{
							// Array
							var value = relevantGlitchTags.Select(t => t.GetComponent(field.FieldType.GetElementType()))
								.Where(t=>t!=null).ToArray();
							var typedValue = Array.CreateInstance(field.FieldType.GetElementType(), value.Length);
							for (int i = 0; i < value.Length; i++)
							{
								typedValue.SetValue(value[i], i);
							}
							
							if (value.Length == 0)
							{
								Debug.LogWarning("Found 0 valid: " + fieldTag);
							}
							
							field.SetValue(objectLookingForGlitchTag, typedValue);
						}
						else if (field.FieldType.IsGenericType)
						{
							// List
							var value = relevantGlitchTags.Select(t => t.GetComponent(field.FieldType.GetGenericArguments()[0]))
								.Where(t=>t!=null).ToArray();
							var typedValue = Activator.CreateInstance(field.FieldType);
							for (int i = 0; i < value.Length; i++)
							{
								((IList) typedValue).Add(value[i]);
							}

							if (value.Length == 0)
							{
								Debug.LogWarning("Found 0 valid: " + fieldTag);
							}
							
							field.SetValue(objectLookingForGlitchTag, typedValue);

						}
						else
						{
							// Single item
							var value = relevantGlitchTags.First().GetComponent(field.FieldType);
							field.SetValue(objectLookingForGlitchTag, value);
						}
						
						
						
						UnityEditor.EditorUtility.SetDirty(objectLookingForGlitchTag);
					}

				}
			}
		}
		
		if(_DEBUG) Debug.Log("Done");
		
		// foreach(Type type in assembly.GetTypes()) {
		// 	if (type.GetCustomAttributes(typeof(HelpAttribute), true).Length > 0) {
		// 		//yield return type;
		// 	}
		// }
		
		// var type = typeof(Component);
		// var props = type.GetProperties().Where(
		// 	prop=> );
	}
	

}



