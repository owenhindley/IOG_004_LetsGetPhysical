using UnityEngine;

public class spin3D : MonoBehaviour {
    public Vector3 speed;

	void Update () {
        transform.Rotate(speed * Time.deltaTime, Space.Self);
	}
}
