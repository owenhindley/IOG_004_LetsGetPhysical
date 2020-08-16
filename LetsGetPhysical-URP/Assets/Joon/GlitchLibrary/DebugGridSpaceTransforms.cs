using UnityEngine;

[ExecuteInEditMode]
public class DebugGridSpaceTransforms : MonoBehaviour {

    public Vector2 spacing;
    public int maxOnX;
    public Material material;

    void OnEnable()
    {
        Debug.Log("Farts");
        for (int i = 0; i < transform.childCount; i++)
        {
            var t = transform.GetChild(i);
            var max = (maxOnX>0?maxOnX:1000);
            t.localPosition = new Vector3((i % max) * spacing.x, 0, (int)(i / max) * spacing.y);
        }

        /*if(material != null)
        foreach (var item in GetComponentsInChildren<Renderer>())
        {
            
            item.material = material;
            if(item.gameObject.name.Contains("collider"))
            {
                item.gameObject.SetActive(false);
            }
        }*/
    }
}

