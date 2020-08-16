using UnityEngine;
 
[System.Serializable]
public struct FloatRange
{
	public float min;
	public float max;
	
	public FloatRange(float min, float max)
	{
		this.min = min;
		this.max = max;
	}

	public float random { get { return Random(); } }

	public float Random() {
		return UnityEngine.Random.Range(min, max);
	}


	public float randomInt { get { return RandomInt(); } }

	public float RandomInt() {
		return UnityEngine.Random.Range((int)min, (int)max);
	}

	public float Lerp(float time)
	{
		return Mathf.Lerp(min, max, time);
	}

	public bool Encapsulates(float value, bool inclusive=false)
	{
		if (inclusive) return value <= max && value >= min;
		return value < max && value > min;
	}
	
	public float clamp(float value)
	{
		return Mathf.Clamp(value, min, max);
	}

	/* 
	public static float operator *(float left, FloatRange right)
	{
		return left * right.Random();
	}

	public static float operator *(FloatRange right, float left)
	{
		return left * right.Random();
	}

	public static explicit operator float(FloatRange floatRange)
	{
		return floatRange.Random();
	}
	*/
}