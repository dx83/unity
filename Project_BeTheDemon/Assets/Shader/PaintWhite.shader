Shader "PaintWhite"
{
    Properties
    {
        _MainTex("Particle Texture (Alpha8)", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1)
    }

    Category
    {
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        Blend SrcAlpha One
        Cull Off Lighting Off ZWrite Off Fog{ Color(0,0,0,0) }

        BindChannels
        {
            Bind "Color", color
            Bind "Vertex", vertex
            Bind "TexCoord", texcoord
        }

        SubShader
        {
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
              float4 _MainTex_ST;
              float3 _Color;

              v2f vert(appdata v)
              {
                  v2f o;
                  o.vertex = UnityObjectToClipPos(v.vertex);
                  o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                  return o;
              }

              fixed4 frag(v2f i) : SV_Target
              {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                col.rgb = _Color;
               

                return col;
              }
          ENDCG
            }
        }
    }
}