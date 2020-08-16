using UnityEngine;

public class spin2D : MonoBehaviour
{

	public float speed;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0,1 * speed * Time.deltaTime,0);
	}
}
