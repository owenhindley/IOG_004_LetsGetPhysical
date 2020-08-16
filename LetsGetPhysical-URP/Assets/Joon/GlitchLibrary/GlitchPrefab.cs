using UnityEngine;

public class GlitchPrefab : MonoBehaviour
{
    public GameObject prefab;
    public bool breakPrefabInstance = false;   // setting to false will lock, true will unlock
    public bool inheritLayer = true;
}
