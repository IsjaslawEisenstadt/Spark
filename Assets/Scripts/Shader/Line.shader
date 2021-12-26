Shader "Unlit/Line"
{
	Properties
	{
		_MainTex("MainTex", 2D) = "white" {}
		_ScrollSpeed ("ScrollSpeed", float) = 1.0
	}
	SubShader
	{
		Tags
		{
			"RenderType"="Opaque"
		}

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2_f
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _ScrollSpeed;

			v2_f vert(const appdata v)
			{
				v2_f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}

			fixed4 frag(const v2_f i) : SV_Target
			{
				const float screen_pos = (1.0 / _ScreenParams.x) * i.vertex.x;
				return fixed4(tex2D(_MainTex, float2(i.uv.x + screen_pos + _Time.y * _ScrollSpeed, i.uv.y)).rgb, 1.0);
			}
			ENDCG
		}
	}
}
