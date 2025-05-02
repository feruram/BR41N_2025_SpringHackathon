Shader "Unlit/Grid_Transparent"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_GridColour ("Grid Colour", Color) = (.255,.0,.0,1)
        _GridSize ("Grid Size", Range(0.01, 1.0)) = 0.1
        _GridLineThickness ("Grid Line Thickness", Range(0.00001, 0.010)) = 0.003
        _Alpha ("Grid Transparency", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _GridColour;
            float _GridSize;
            float _GridLineThickness;
            float _Alpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex.xyz);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            float GridTest(float2 r)
            {
                float result = 0.0;
                for (float i = 0.0; i <= 1; i += _GridSize)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        result += 1.0 - smoothstep(0.0, _GridLineThickness, abs(r[j] - i));
                    }
                }
                return result;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float gridValue = GridTest(i.uv);
                fixed4 gridColour = (_GridColour * gridValue) + tex2D(_MainTex, i.uv);
                gridColour.a = _Alpha; // 全体の透明度適用
                return gridColour;
            }
            ENDCG
        }
    }
}
