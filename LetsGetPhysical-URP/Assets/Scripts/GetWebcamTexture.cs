 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWebcamTexture : MonoBehaviour
{    
    public WebCamTexture webCamTexture; 
    public ColliderPlacer colliders;
    public Renderer renderer;

    public RenderTexture destRT;

    public Camera colliderCamera;

    public System.Action OnNewTexture;

    public GameObject webcamView;

    public List<string> devices;
    private int currentDeviceIndex = 0;
    public int selectedDevice = 0;

    public int numFramesToWaitAfterBlit = 3;

    public float downsampleFactor = 8.0f;

    public bool debugUseTestTexture = false;
    public Texture debugTestTexture;

    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();

        for (int i=0; i < WebCamTexture.devices.Length; i++){
            var d = WebCamTexture.devices[i];
            devices.Add(d.name);
            Debug.Log(d.name);
        }
        webCamTexture.deviceName = devices[selectedDevice];
        currentDeviceIndex = selectedDevice;
        renderer.material.mainTexture = webCamTexture;
        webCamTexture.Play();

        webcamView.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedDevice != currentDeviceIndex){
            webCamTexture.deviceName = devices[selectedDevice];
            currentDeviceIndex = selectedDevice;
        }

        if (webCamTexture != null){
            if (Input.GetKeyDown(KeyCode.Space)){
                Debug.Log("Blitting texture...");



                int newWidth = Mathf.FloorToInt(Screen.width / (float)downsampleFactor);
                int newHeight = Mathf.FloorToInt(Screen.height / (float)downsampleFactor);
                destRT = new RenderTexture(newWidth, newHeight, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
                

               
                // Graphics.Blit(tex, destRT);
                StartCoroutine(CaptureRoutine(destRT));
            }
        }

        if (Input.GetKeyDown(KeyCode.W)){
            if (Input.GetKey(KeyCode.LeftShift)){
                webcamView.SetActive(false);
            } else {
                 webcamView.SetActive(true);
            }
        }
    }

    IEnumerator CaptureRoutine(RenderTexture tgt){

        colliders.DestroyAllColliders();
        
        webcamView.SetActive(true);

        yield return null;
        yield return null;

        // force main camera to render to RT
        var cachedAspect = colliderCamera.aspect;
        colliderCamera.targetTexture = tgt;
        colliderCamera.aspect = cachedAspect;
        colliderCamera.Render();
        colliderCamera.targetTexture = null;

        yield return null;
        yield return null;

        webcamView.SetActive(false);

        int numFrames = 0;
        while(numFrames < numFramesToWaitAfterBlit){
            numFrames++;
            yield return null;
        }
        if (OnNewTexture != null){
            OnNewTexture.Invoke();
        }

        yield return new WaitForSeconds(1.0f);

        colliders.SetColliderVisualState(false);
    }
}
