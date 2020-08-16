using UnityEngine;

[System.AttributeUsage(System.AttributeTargets.Field)]
public class GlitchTagAttribute : PropertyAttribute
{
	public readonly string tag;

	public GlitchTagAttribute(string tag)
	{
		this.tag = tag;
	}
}