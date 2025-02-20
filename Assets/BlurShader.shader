Shader "Custom/BlurShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BlurAmount ("Blur Amount", Range(0, 1)) = 0.80
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

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
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float _BlurAmount;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float4 color = tex2D(_MainTex, i.uv);
                float2 offset = float2(_BlurAmount / 100, _BlurAmount / 100);

                // Sample neighboring pixels to create blur
                float4 blurColor = (
                    tex2D(_MainTex, i.uv + offset) +
                    tex2D(_MainTex, i.uv - offset) +
                    tex2D(_MainTex, i.uv + float2(offset.x, -offset.y)) +
                    tex2D(_MainTex, i.uv - float2(offset.x, -offset.y))
                ) * 0.25;

                return float4(blurColor.rgb, 0.25); // Adjust alpha for transparency
            }
            ENDCG
        }
    }
}
