using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlitToSubtextures : MonoBehaviour
{

    public GetWebcamTexture getWebcamTexture;
    
    [System.Serializable]
    public class Subtexture {
        public Material material;
        public RenderTexture destTexture;
    }

    public List<Subtexture> subtextures;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (getWebcamTexture.webCamTexture != null){
            if (getWebcamTexture.webCamTexture.didUpdateThisFrame){
                for (int i=0; i < subtextures.Count; i++){
                    Graphics.Blit(null, subtextures[i].destTexture, subtextures[i].material);
                }

            }

        }
        
    }

    
}
