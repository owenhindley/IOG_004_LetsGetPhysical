using UnityEngine;

public class Lookat : MonoBehaviour {
    public Transform target;
	void Update () {
        transform.LookAt(target);
	
	}
}
