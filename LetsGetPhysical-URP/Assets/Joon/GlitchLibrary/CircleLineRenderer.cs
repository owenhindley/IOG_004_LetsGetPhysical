using System.Collections.Generic;
using UnityEngine;

public class CircleLineRenderer : MonoBehaviour {

	public float textureScrollTimescale;
	public float radius;
	public int detail;
	LineRenderer _line;

#if UNITY_EDITOR
	[InspectorButton("Regenerate")]
	public bool regenerateButton;
#endif

	void Start()
	{
		_line = GetComponent<LineRenderer>();
	}

	void Update()
	{
		_line.material.SetTextureOffset("_MainTex", new Vector2(Time.realtimeSinceStartup * textureScrollTimescale, 0));
	}

	void Regenerate()
	{
		if(_line == null) _line = GetComponent<LineRenderer>();

		var points = new List<Vector3>();
		for (int i = 0; i < detail; i++)
		{
			points.Add(Quaternion.Euler(0,i/(float)detail * 360f,0) * Vector3.forward * radius);
		}

		_line.positionCount = points.Count;
		_line.SetPositions(points.ToArray());

	}
}
