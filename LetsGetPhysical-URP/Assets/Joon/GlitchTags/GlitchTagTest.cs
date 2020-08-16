using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GlitchTagTest : MonoBehaviour
{
	[GlitchTagAttribute("test")]
	public Transform test;
	
	[GlitchTagAttribute("test2")]
	public Transform test2;
	
	[GlitchTagAttribute("test3")]
	public Rigidbody test3;
	
	[GlitchTagAttribute("bbq")]
	public Transform[] test4;
	
	[GlitchTagAttribute("bbq")]
	public List<Collider> test5;
}
