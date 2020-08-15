Shader "Unlit/ToBlackWhiteShader"
{
    Properties
    {
        _Threshold("BW_Threshold", Float) = 0
        _Tex("InputTex", 2D) = "white" {}
     }

     SubShader
     {
        Lighting Off
        Blend One Zero

        Pass
        {
            CGPROGRAM
            #include "UnityCustomRenderTexture.cginc"
            #pragma vertex CustomRenderTextureVertexShader
            #pragma fragment frag
             #pragma target 3.0

            float _Threshold;
            sampler2D   _Tex;

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 outColor = tex2D(_Tex, IN.localTexcoord.xy);
                if (length(outColor) > _Threshold){
                    outColor = float4(1.0f, 1.0f, 1.0f, 1.0f);
                } else {
                    outColor = float4(0,0,0,0);
                }


                return outColor;
            }
            ENDCG
        }
    }
}
