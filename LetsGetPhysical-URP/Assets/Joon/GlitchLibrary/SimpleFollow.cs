using UnityEngine;

public class SimpleFollow : MonoBehaviour {

	public Transform target;
	public bool followX;
	public bool followY;
	public bool followZ;
	public bool snapOnStart;
	
	private Vector3 offset;
	
	
	void Start () {
		offset = transform.position - target.position;

		if (snapOnStart)
		{
			var x = transform.position.x;
			var y = transform.position.y;
			var z = transform.position.z;

			if (followX) x = target.position.x;
			if (followY) y = target.position.y;
			if (followZ) z = target.position.z;

			transform.position = new Vector3(x, y, z);
		}
	}
	

	void Update () {
		var x = transform.position.x;
		var y = transform.position.y;
		var z = transform.position.z;

		if(followX) x = target.position.x + offset.x;
		if(followY) y = target.position.y + offset.y;
		if(followZ) z = target.position.z + offset.z;

		transform.position = new Vector3(x,y,z);
	}
}
