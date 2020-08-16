using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArrowLineRenderer : MonoBehaviour {

	public float widthMultiplier;
	public float angle;
	public float radius;
	public int detail;
	public float headWidthMultiplier;
	public float headLength;
	
	LineRenderer _arrowLine;
	LineRenderer _headLine;

#if UNITY_EDITOR
	[InspectorButton("Regenerate")]
	public bool regenerateButton;
#endif

	void Start()
	{
	}

	void Update()
	{
	//	_line.material.SetTextureOffset("_MainTex", new Vector2(Time.realtimeSinceStartup * textureScrollTimescale, 0));
	}

	void Regenerate()
	{
		_arrowLine = GetComponent<LineRenderer>();
		_headLine = transform.GetChild(0).GetComponent<LineRenderer>();

		_arrowLine.widthMultiplier =  widthMultiplier;

		var points = new List<Vector3>();
		for (int i = 0; i < detail; i++)
		{
			var angle = i/(float)detail * 360f;
			var nextPoint = Quaternion.Euler(0,angle,0) * Vector3.forward * radius;
			if(angle > this.angle) break;
			
			points.Add(nextPoint);
			if(i == 0) 
			{
				points.Add(nextPoint + Vector3.right * 0.01f);
				
			}
		}

		_arrowLine.positionCount = points.Count;
		_arrowLine.SetPositions(points.ToArray());

		_headLine.widthMultiplier =  _arrowLine.widthMultiplier * headWidthMultiplier;

		var headPoints = new List<Vector3>();
		var lastPoint = points.Last();
		var lastLastPoint = points[points.Count - 2];
		var direction = -(lastLastPoint - lastPoint).normalized * headLength * widthMultiplier *2;
		headPoints.Add(lastLastPoint);
		headPoints.Add(lastPoint + direction);

		_headLine.positionCount = headPoints.Count;
		_headLine.SetPositions(headPoints.ToArray());

	}
}
