using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TankMeshGenerator : MonoBehaviour {
    public int segmentsX;
    public int segmentsY;
    public MeshFilter filter;
    public Transform[] points;
    public Vector3[] verts;
    public int[] tris;
    public bool LiveUpdate;

    public bool VisibleMarkers = false;

    public GameObject selectedPin;
    public float moveAmount;

    void Awake()
    {
     
    }
    void Start()
    {
        GenerateMesh();

       
        for (int i=0; i < points.Length; i++){
            points[i].gameObject.SetActive(VisibleMarkers);
        }
    }
    void Update()
    {
        if (LiveUpdate) 
            GenerateMesh();

        if (Input.GetKeyDown(KeyCode.Alpha9)){
            VisibleMarkers = !VisibleMarkers;
            for (int i=0; i < points.Length; i++){
                points[i].gameObject.SetActive(VisibleMarkers);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
          
            
            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit) 
            {              
                if (hitInfo.transform.gameObject.tag == "MeshCornerPin")
                {
                    hitInfo.transform.gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
                    selectedPin = hitInfo.transform.parent.gameObject;
                } 


            } else {
                for (int i=0; i < points.Length; i++){
                    points[i].gameObject.GetComponentInChildren<Renderer>().material.SetColor("_BaseColor", Color.red);
                }
                selectedPin = null;  
            }
            
        } 

        if (selectedPin != null && Input.GetMouseButton(0)) {
            var screenPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedPin.transform.position = new Vector3(screenPoint.x, screenPoint.y, selectedPin.transform.position.z);
        }
    }
    void GenerateMesh()
    {
        // find and clean filter
        if (filter.mesh != null) filter.mesh.Clear();
        // prep empty arrays
        Mesh mesh = new Mesh();
        //Vector3[] verts = new Vector3[segmentsX  * segmentsY];
        verts = new Vector3[(segmentsX +1) * (segmentsY +1)];
        Vector3[] norms = new Vector3[verts.Length];
        Vector2[] uvs = new Vector2[verts.Length];
        //int[] tris = new int[(segmentsX - 1) * (segmentsY -1) * 2 * 3];
        tris = new int[segmentsX * segmentsY * 2 * 3];
        var center = transform.position;
        for (int x = 0; x < segmentsX +1; x++)
        {
            for (int y = 0; y < segmentsY+1; y++)
            {
                var bottomX = Vector3.Lerp(points[0].position - center, points[3].position - center, x / (float)segmentsX);
                var topX = Vector3.Lerp(points[1].position - center, points[2].position - center, x / (float)segmentsX);
                verts[GetIndex(x, y)] = Vector3.Lerp(bottomX, topX, y / (float)segmentsY);
                if(x < segmentsX && y < segmentsY)
                {
                    var id = (x + y * segmentsX)  * 2 * 3;
                    tris[id + 0] = GetIndex(x, y);
                    tris[id + 2] = GetIndex(x+1, y);
                    tris[id + 1] = GetIndex(x, y+1);
                    id+=3;
                    tris[id + 0] = GetIndex(x+1, y);
                    tris[id + 2] = GetIndex(x + 1, y+1);
                    tris[id + 1] = GetIndex(x, y + 1);
                }
                uvs[GetIndex(x, y)] = new Vector2(x / (float)segmentsX, y / (float)segmentsY);
            }
        }
        mesh.vertices = verts;
        mesh.normals = norms;
        mesh.uv = uvs;
        mesh.triangles = tris;
        filter.mesh = mesh;
    }
    private int GetIndex(int x, int y)
    {
        return x + (y * (segmentsX+1));
    }
}