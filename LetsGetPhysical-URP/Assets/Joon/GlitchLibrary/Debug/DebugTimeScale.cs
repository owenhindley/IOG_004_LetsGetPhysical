using UnityEngine;

public class DebugTimeScale : MonoBehaviour {

    [Range(0,20)]
    public float timeScale;
    public bool on;

	void Start () {
        timeScale = Time.timeScale;
	}
	
	void Update () {
	    if(on)
        {
            Time.timeScale = timeScale;
        }
	}
}
