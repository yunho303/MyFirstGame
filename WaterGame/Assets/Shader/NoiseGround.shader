Shader "Custom/NoiseGround"
{
    Properties
    {
        _Tess ("Tessellation", Range(1,8)) = 4
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows tessellate:tess

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 4.6

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };
        float _Tess;
        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float4 tess(){
            return _Tess;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
