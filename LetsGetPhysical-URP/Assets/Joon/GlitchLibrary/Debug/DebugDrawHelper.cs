using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class DebugDrawHelper : MonoBehaviour
{
	public static DebugDrawHelper instance;

	public struct Line
	{
		public Vector3 a;
		public Vector3 b;
		public Color color;

		public Line(Vector3 a, Vector3 b, Color? color)
		{
			this.a = a;
			this.b = b;
			this.color = color ?? Color.red;
		}
	}

	public struct Label
	{
		public Vector3 position;
		public string label;

		public Label(Vector3 position, string label)
		{
			this.position = position;
			this.label = label;
		}
	}
	
	static List<Line> lines = new List<Line>();
	static List<Label> labels = new List<Label>();
	
	static void Init()
	{
		if (instance == null)
		{
			instance = FindObjectOfType<DebugDrawHelper>();
		}

		if (instance == null)
		{
			instance = new GameObject("Debug Draw Helper (generated)", typeof(DebugDrawHelper))
				.GetComponent<DebugDrawHelper>();
		}
	}

	public static void Clear()
	{
		Init();
		lines.Clear();
		labels.Clear();
	}
	
	public static void AddLine(Vector3 a, Vector3 b, Color color)
	{
		Init();
		lines.Add(new Line(a,b, color));
	}
	
	public static void AddQuaternion(Vector3 position, Quaternion orientation, Color color)
	{
		Init();
		lines.Add(new Line(position, orientation * Vector3.forward + position, color));
	}
	
	public static void AddLabel(Vector3 position, string text)
	{
		Init();
		labels.Add(new Label(position, text));
	}

	
	void OnDrawGizmos()
	{
		foreach (var line in lines)
		{
			Gizmos.color = line.color;
			Gizmos.DrawLine(line.a, line.b);
			Gizmos.DrawCube(line.a,Vector3.one * 0.2f);
		}
		foreach (var label in labels)
		{
			#if UNITY_EDITOR
			Handles.Label(label.position, label.label);
			#endif
		}
	}

	
	
	[InspectorButton("EditorClear")] public bool _clearAll;
	public void EditorClear()
	{
		Clear();
	}
	
}
