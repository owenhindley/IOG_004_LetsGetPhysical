Shader "Unlit/SelectColourShader"
{
    Properties
    {
        _TargetHue("TargetHue", Float) = 0
        _HueThreshold("Hue Threshold", Float) = 0
        _LightnessThreshold("Lightness Threshold", Float) = 0
        _MainTex ("Texture", 2D) = "white" {}
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

            float _HueThreshold;
            float _LightnessThreshold;
            float _TargetHue;
            sampler2D   _Tex;

            float3 rgb2hsv(float4 c)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
                float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return float4(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x, 1.0f);
            }

            float4 frag(v2f_customrendertexture IN) : COLOR
            {
                float4 outColor = tex2D(_Tex, IN.localTexcoord.xy);
                float3 outHSV = rgb2hsv(outColor);

                if (abs(outHSV.x - _TargetHue) < _HueThreshold){
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
