Shader "Custom/DissolveShader"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _DissolveTex("Dissolve Mask", 2D) = "white" {}
        _DissolveAmount("Dissolve Amount", Range(0, 1)) = 0
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #include "UnityCG.cginc"

                struct appdata_t
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
                sampler2D _DissolveTex;
                float _DissolveAmount;

                v2f vert(appdata_t v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = v.uv;
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    fixed4 mainTex = tex2D(_MainTex, i.uv);
                    fixed4 dissolveTex = tex2D(_DissolveTex, i.uv);

                    // ¿ØÖÆ dissolve Ð§¹û
                    if (dissolveTex.r < _DissolveAmount)
                        discard;

                    return mainTex;
                }
                ENDCG
            }
        }
}
