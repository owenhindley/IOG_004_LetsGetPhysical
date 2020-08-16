using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    using UnityEngine.Rendering;

public class ColliderPlacer : MonoBehaviour
{
    public GetWebcamTexture getWebcamTexture;


    private Texture sourceTex;

    public GameObject colliderPrefab;

    public List<GameObject> colliders;

    public float colliderScale = 0.1f;

    [Range(0, byte.MaxValue)]
    public byte threshold = 128;

    public float FieldWidth = 8.0f;
    public float FieldHeight = 8.0f;   

    public float collidersWorldZ = 1.0f;

    public bool showGUI = false;

    public Vector3 bodgePositionOffset = Vector3.zero;
    public Vector3 bodgePositionScale = Vector3.one;


    // Start is called before the first frame update
    void Start()
    {
        getWebcamTexture.OnNewTexture += OnNewWebcamTexture;
    }

    void OnGUI(){

        if (showGUI){
            var pos = new Vector2(10.0f, 10.0f);
            threshold = (byte)Mathf.FloorToInt(GUI.HorizontalSlider(new Rect(pos.x, pos.y, 500.0f, 1000.0f), threshold, 0.0f, byte.MaxValue));
        }
        

    }

    public void DestroyAllColliders(){
        
        for (int i=0; i < colliders.Count; i++){
            Destroy(colliders[i]);
        }
    }

    public void OnNewWebcamTexture(){
        PlaceColliders();
    }

    [ContextMenu("Place Colliders")]
    public void PlaceColliders(){

       
        for (int i=0; i < colliders.Count; i++){
            Destroy(colliders[i]);
        }

        colliders.Clear();

        sourceTex = getWebcamTexture.destRT;

        AsyncGPUReadback.Request(sourceTex, 0, TextureFormat.RGBA32, OnCompleteReadback);

        
       

    }

    void OnCompleteReadback(AsyncGPUReadbackRequest request)
    {
        if (request.hasError)
        {
            Debug.Log("GPU readback error detected.");
            return;
        }

        var data = request.GetData<Color32>();

        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;

        int index = 0;
        for (int y=0; y < sourceTex.height; y++){
            for (int x=0; x < sourceTex.width; x++){

                var col = data[index];
                int total = col.r + col.g + col.b;
                total /= 3;
                if (total < threshold){
                    
                    // get main camera world coord for this pixel (because it came from this)
                    var screenPos = new Vector3( ((float)x / (float)sourceTex.width) * Screen.width, ((float)y / (float)sourceTex.height) * Screen.height, 0.0f);
                    var worldPos = Camera.main.ScreenToWorldPoint(screenPos);
                    // place a collider here
                    
                    var c = GameObject.Instantiate(colliderPrefab, Vector3.zero, Quaternion.identity, this.transform);
                    c.transform.position = new Vector3(worldPos.x, worldPos.y, collidersWorldZ);
                    c.transform.localScale = Vector3.one * colliderScale;

                    colliders.Add(c);
                } else {
                    // no collider for you
                }
                index++;
            }
        }

        transform.localPosition = bodgePositionOffset;
        transform.localScale = bodgePositionScale;
    }

    public void SetColliderVisualState(bool state){
        for (int i=0; i < colliders.Count; i++){
             colliders[i].GetComponentInChildren<Renderer>().enabled = state;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6)){
           SetColliderVisualState(!Input.GetKey(KeyCode.LeftShift));
           
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)){
            showGUI = !Input.GetKey(KeyCode.LeftShift);
        }
    }
}
