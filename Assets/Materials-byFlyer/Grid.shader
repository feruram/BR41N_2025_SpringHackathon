Shader "Unlit/Grid"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        [HDR]_GridColour ("Grid Colour", Color) = (.255,.0,.0,1)
        _GridSize ("Grid Size", Range(0.01, 1.0)) = 0.1
        _GridLineThickness ("Grid Line Thickness", Range(0.00001, 0.010)) = 0.003
        _Alpha ("Grid Transparency", Range(0, 1)) = 0.5
        _Intensity ("Emission Intensity", Range(-5,5)) = 0
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
                float4 vertex : SV_POSITION; // クリップ空間の位置
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _GridColour;
            float _GridSize;
            float _GridLineThickness;
            float _Alpha;
            float _Intensity;

            v2f vert (appdata v)
            {
                v2f o;

                // UnityObjectToClipPosを使用してオブジェクト空間からクリップ空間へ変換
                o.vertex = UnityObjectToClipPos(v.vertex.xyz);

                // UV座標の計算
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);

                // フォグ情報を転送
                UNITY_TRANSFER_FOG(o, o.vertex);

                return o;
            }

            // グリッドパターンの計算
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
                // UV座標に基づくグリッド色の計算
                fixed4 gridColour = (_GridColour * GridTest(i.uv)) + tex2D(_MainTex, i.uv);

                // アルファ値の適用
                gridColour = float4(gridColour.rgb, _Alpha);

                return gridColour;
            }
            ENDCG
        }
    }
}
