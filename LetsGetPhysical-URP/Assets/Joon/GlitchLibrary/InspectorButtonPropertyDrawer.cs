using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Reflection;

/// <summary>
/// This attribute can only be applied to fields because its
/// associated PropertyDrawer only operates on fields (either
/// public or tagged with the [SerializeField] attribute) in
/// the target MonoBehaviour.
/// </summary>
[System.AttributeUsage(System.AttributeTargets.Field)]
public class InspectorButtonAttribute : PropertyAttribute
{
	public static float kDefaultButtonWidth = 200;

	public readonly string MethodName;

	private float _buttonWidth = kDefaultButtonWidth;
	public float ButtonWidth
	{
		get { return _buttonWidth; }
		set { _buttonWidth = value; }
	}

	public InspectorButtonAttribute(string MethodName)
	{
		this.MethodName = MethodName;
	}


	public InspectorButtonAttribute(string MethodName, float buttonWidth)
	{
		this.MethodName = MethodName;
		_buttonWidth = buttonWidth;
	}
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(InspectorButtonAttribute))]
public class InspectorButtonPropertyDrawer : PropertyDrawer
{
	private MethodInfo _eventMethodInfo = null;

	public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
	{
		InspectorButtonAttribute inspectorButtonAttribute = (InspectorButtonAttribute) attribute;
		Rect buttonRect = new Rect(position.x + (position.width - inspectorButtonAttribute.ButtonWidth) * 0.5f, position.y, inspectorButtonAttribute.ButtonWidth, position.height);
		if (GUI.Button(buttonRect, label.text))
		{
			var objects = Selection.gameObjects;
			foreach (var o in objects)
			{
				System.Type eventOwnerType = prop.serializedObject.targetObject.GetType();
				var component = o.GetComponent(eventOwnerType);
				string eventName = inspectorButtonAttribute.MethodName;

				if (_eventMethodInfo == null)
					_eventMethodInfo = eventOwnerType.GetMethod(eventName, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

				if (_eventMethodInfo != null)
				{
					_eventMethodInfo.Invoke(component, null);
				}
				else
				{
					Debug.LogWarning(string.Format("InspectorButton: Unable to find method {0} in {1}", eventName,
						eventOwnerType));
				}
			}
		}
	}
}
#endif
