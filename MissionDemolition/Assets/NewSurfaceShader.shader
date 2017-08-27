Shader "Custom/NewSurfaceShader" {
	Properties{
		_MainColor("Color", Color) = (1, 1, 1, 1)
		_MainTex("Main Texture", 2D) = "" {}
	}
	SubShader{
		Tags{ "RenderType" = "Opaque" }
		
		CGPROGRAM
#pragma surface surf Cell
#include "UnityCG.cginc"
#include "AutoLight.cginc"
#include "Lighting.cginc"
#include "UnityPBSLighting.cginc"

		fixed4 _MainColor;
		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _MainColor;
		}

		half4 LightingCell(SurfaceOutput s, half3 lightDir, half atten) {
			half ndot1 = dot(s.Normal, lightDir);
			half4 c;
			if (ndot1 > 0.7f)
				ndot1 = 1.0f;
			else if (ndot1 > 0.6f)
				ndot1 = 0.4f;
			else if (ndot1 > 0.4f)
				ndot1 = 0.3f;
			else
				ndot1 = 0.2f;
			
			c.rgb = _MainColor * s.Albedo * _LightColor0.rgb * atten * ndot1;
			c.a = s.Alpha;
			return c;
		}

		


		ENDCG
	}
		Fallback "Diffuse"

}
