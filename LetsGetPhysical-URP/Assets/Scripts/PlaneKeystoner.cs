using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneKeystoner : MonoBehaviour
{
    public MeshFilter targetFilter;

    private Mesh defaultMesh;

    public Mesh newMesh;

    public float keystoneFactor = 0.0f;
    public int topLeftVertex = 2;
    public int topRightVertex = 3;


    // Start is called before the first frame update
    void Start()
    {
        defaultMesh = (Mesh)Instantiate(targetFilter.mesh);
        newMesh = new Mesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8)){
            keystoneFactor += 0.1f;
            UpdateMesh();
        }
        if (Input.GetKeyDown(KeyCode.Alpha9)){
            keystoneFactor -= 0.1f;
            UpdateMesh();
        }
       
    }

    public void UpdateMesh(){
        Destroy(newMesh);
        newMesh = null;
        newMesh = (Mesh)Instantiate(defaultMesh);
        var verts = new List<Vector3>();
        
        newMesh.GetVertices(verts);
        for (int i=0; i < verts.Count; i++){
            Debug.Log(verts[i]);
            if(i == topLeftVertex){
                verts[i] += Vector3.right * keystoneFactor;
            }
            if (i == topRightVertex){
                verts[i] += Vector3.left * keystoneFactor;
            }
        }
        newMesh.SetVertices(verts);

        targetFilter.mesh = newMesh;
    }
}
