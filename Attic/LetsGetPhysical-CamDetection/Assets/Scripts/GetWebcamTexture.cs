using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWebcamTexture : MonoBehaviour
{    
    public WebCamTexture webCamTexture; 
    public Renderer renderer;

    public RenderTexture destRT;

    public List<string> devices;
    private int currentDeviceIndex = 0;
    public int selectedDevice = 0;

    // Start is called before the first frame update
    void Start()
    {
        webCamTexture = new WebCamTexture();

        for (int i=0; i < WebCamTexture.devices.Length; i++){
            var d = WebCamTexture.devices[i];
            devices.Add(d.name);
        }
        webCamTexture.deviceName = devices[selectedDevice];
        currentDeviceIndex = selectedDevice;
        renderer.material.mainTexture = webCamTexture;
        webCamTexture.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedDevice != currentDeviceIndex){
            webCamTexture.deviceName = devices[selectedDevice];
            currentDeviceIndex = selectedDevice;
        }

        if (webCamTexture != null){
            if (webCamTexture.didUpdateThisFrame){
                Graphics.Blit(webCamTexture, destRT);
            }
        }
    }
}
