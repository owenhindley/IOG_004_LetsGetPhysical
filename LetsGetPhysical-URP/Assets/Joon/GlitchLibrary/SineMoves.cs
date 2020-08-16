using UnityEngine;

public class SineMoves : MonoBehaviour {

    [System.Serializable]
    public struct SineSetting {
        public Vector3 axis;
        public bool abs;
        public float frequencyMultiplier;
        public float offset;
    }

    public SineSetting[] moves;
    public SineSetting[] rotations;
    public SineSetting[] scales;
    public float frequency = 1;
    public float amplitude = 1;

    Vector3 _startPos;
    Quaternion _startRot;
    Vector3 _startScale;
    float _time;

	void Start () {
        Init();
	}

    public void Init()
    {
        _startPos = transform.localPosition;
        _startRot = transform.localRotation;
        _startScale = transform.localScale;
    }

	
	void Update () {
        _time += Time.deltaTime;



        if (moves != null && moves.Length > 0)
        {
	        for (int i = 0; i < moves.Length; i++)
	        {
		        if (i == 0) transform.localPosition = _startPos; //only on first one
		        var move = moves[i];
		        float val = Mathf.Sin(_time * frequency * move.frequencyMultiplier + move.offset);
		        if (move.abs) val = Mathf.Abs(val);
		        transform.localPosition += move.axis * val * amplitude;
	        }
        }

        if (rotations != null && rotations.Length > 0)
        {
	        for (int i = 0; i < rotations.Length; i++)
	        {
		        if (i == 0) transform.localRotation = _startRot; // only on first one
		        var rotation = rotations[i];
		        float val = Mathf.Sin(_time * frequency * rotation.frequencyMultiplier + rotation.offset);
		        if (rotation.abs) val = Mathf.Abs(val);
		        transform.localRotation = transform.localRotation * Quaternion.Euler(rotation.axis * val * amplitude);
	        }
        }

        if (scales != null && scales.Length > 0)
        {
	        for (int i = 0; i < scales.Length; i++)
	        {
		        if (i == 0) transform.localScale = _startScale; // only on first one
		        var scale = scales[i];
		        float val = Mathf.Sin(_time * frequency * scale.frequencyMultiplier + scale.offset) + 1;
		        if (scale.abs) val = Mathf.Abs(val - 1) + 1;
		        transform.localScale += scale.axis * val * amplitude;
	        }
        }
	}
}
