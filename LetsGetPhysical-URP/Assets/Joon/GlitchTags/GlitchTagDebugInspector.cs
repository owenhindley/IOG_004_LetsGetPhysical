using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class GlitchTagDebugInspector : MonoBehaviour
{
#if UNITY_EDITOR
	void Awake()
	{
		Debug.Log("yo");
	}
	
	
	[InspectorButton("ReloadButton")] public bool _reload;
	public void ReloadButton() { GlitchTags.ReloadGlitchTags();}
	
	[InspectorButton("ResetButton")] public bool _reset;
	public void ResetButton() { GlitchTags.ResetGlitchTags();}
#endif
	
}



