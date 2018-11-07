Shader "DarkLordGame/Standard/VerticeColor Transparent" {
	Properties {
        _Transparency("Transparency", Range(0.0,1)) = 0.25
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		LOD 200
        Blend SrcAlpha OneMinusSrcAlpha
		Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float4 color : Color;
            };

            struct v2f
            {
                float4 color : Color0;
                float4 vertex : SV_POSITION;
            };
            
            float _Transparency;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.color = v.color;
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
            
                fixed4 col = i.color;
                col.a *= _Transparency;
                return col;
            }
            ENDCG
        }
	}
	FallBack "Diffuse"
}
