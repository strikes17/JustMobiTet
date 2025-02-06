Shader "Custom/TiledSprite"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {} // Main texture
        _Tiling ("Tiling", Vector) = (1, 1, 0, 0) // Tiling factor for X and Y
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST; // Texture scale and offset
            float2 _Tiling; // Tiling factor for X and Y

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv * _Tiling; // Apply tiling to UV coordinates
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the texture with tiled UV coordinates
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}