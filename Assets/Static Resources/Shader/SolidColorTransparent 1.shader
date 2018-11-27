Shader "DarkLordGame/Standard/SolidColor Transparent" {
	Properties {
        [HDR] _Color("Color", Color) = (1, 1, 1, 1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard vertex:vert fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input {
            float3 vertexColor; // Vertex color stored here by vert() method
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        
        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input,o);
        }
         
        void surf (Input IN, inout SurfaceOutputStandard o) {
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Albedo = _Color;
            o.Alpha = _Color.a;
        }
        ENDCG
	}
	FallBack "Diffuse"
}
