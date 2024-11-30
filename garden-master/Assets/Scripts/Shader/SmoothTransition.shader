Shader "Custom/SmoothTransition"
{
    Properties
    {
        _MainTex("Base Sprite", 2D) = "white" {}
        _SecondTex("Second Sprite", 2D) = "white" {}
        _Blend("Blend Factor", Range(0, 1)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" }
            LOD 200

            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha  // 添加混合模式

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

                #include "UnityCG.cginc"

                struct appdata_t
                {
                    float4 vertex : POSITION;
                    float2 texcoord : TEXCOORD0;
                };

                struct v2f
                {
                    float2 uv : TEXCOORD0;
                    float4 vertex : SV_POSITION;
                };

                sampler2D _MainTex;
                sampler2D _SecondTex;
                float _Blend;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.texcoord;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 color1 = tex2D(_MainTex, i.uv);
                    fixed4 color2 = tex2D(_SecondTex, i.uv);
                    return lerp(color1, color2, _Blend); // 平滑混合两张贴图
                }
                ENDCG
            }
        }
}
