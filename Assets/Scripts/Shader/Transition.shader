Shader "PostProcessing/Transition"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        _GradientTex ("_GradientTexture", 2D) = "white" {}
        _CutOff ("CutOff", Range(0.0, 1.0)) = 0
        _SmoothSize ("SmoothSize", Range(0.0, 1.0)) = 0
        _Color ("Color", Color) = (0., 0., 0., 1.)
    }
    SubShader
    {
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off ZWrite Off ZTest Always

        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }

        Stencil
        {
            Ref[_Stencil]
            Comp[_StencilComp]
            Pass[_StencilOp]
            ReadMask[_StencilReadMask]
            WriteMask[_StencilWriteMask]
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

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _GradientTex;
            float _CutOff;
            float _SmoothSize;
            fixed4 _Color;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed value = tex2D(_GradientTex, i.uv).r;
                fixed alpha = smoothstep(_CutOff, _CutOff + _SmoothSize, value * (1 - _SmoothSize) + _SmoothSize);
                fixed4 result = _Color;
                result.a = alpha;
                return result;
            }
            ENDCG
        }
    }
}
