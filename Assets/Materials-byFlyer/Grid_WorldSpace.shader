Shader "Unlit/Grid_WorldSpace"
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
            };

            struct v2f
            {
                float3 worldPos : TEXCOORD0;  // ワールド座標を送る
                float4 vertex : SV_POSITION;
            };

            float4 _GridColour;
            float _GridSize;
            float _GridLineThickness;
            float _Alpha;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;  // ワールド座標を取得
                return o;
            }

            float GridTest(float2 r)
            {
                float2 grid = frac(r / _GridSize); // 0～1のグリッド値
                float2 lines = min(grid, 1.0 - grid) / _GridLineThickness;
                float lineIntensity = max(lines.x, lines.y);
                return 1.0 - saturate(lineIntensity);
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float gridValue = GridTest(i.worldPos.xz); // XZ 平面でグリッドを計算
                fixed4 gridColour = _GridColour * gridValue;
                gridColour.a = lerp(_Alpha, 1.0, gridValue); // グリッド線を不透明に
                return gridColour;
            }
            ENDCG
        }
    }
}
