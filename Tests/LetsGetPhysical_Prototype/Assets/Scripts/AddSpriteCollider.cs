using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AddSpriteCollider : MonoBehaviour
{
    private PolygonCollider2D polygonCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    [ContextMenu("AddCollider")]
    public void AddPolygonCollider(){
        if (polygonCollider != null){
            Destroy(polygonCollider);
        }
        polygonCollider = gameObject.AddComponent<PolygonCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
