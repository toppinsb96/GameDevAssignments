Shader "Unlit/SplatSurface"
{
	Properties
	{
		_SplatNoise ("Splat Noise", 2D) = "white" {}
		_Color("Color", Color) = (1, 1, 1, 1)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float2 uv : TEXCOORD0;
			};

			sampler2D _SplatNoise;
			float4 _Color;
			float4 _SplatNoise_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _SplatNoise);
				o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = _Color;
				float4 splatNoise = tex2D(_SplatNoise, i.uv);
				i.color.r -= splatNoise.r;
				i.color.b -= splatNoise.b;

				if (i.color.r > 0.2 || i.color.b > 0.2) {
					if (i.color.r > i.color.b) {
						col = float4(1.0, 0.2, 0.2, 1.0);
					}
					else {
						col = float4(0, 0.2, 1.0, 1.0);
					}
				}
				
				return col;
			}
			ENDCG
		}
	}
}
