Shader "Naxeex/CutOut"
{
  Properties
  {
    _MainTex ("MainTex", 2D) = "white" {}
    _Cutoff ("Alpha cutoff", Range(0, 1)) = 0.5
    [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", float) = 2
    _OffsetFactor ("Offset Factor", float) = 0
    _OffsetUnits ("Offset Units", float) = 0
  }
  SubShader
  {
    Tags
    { 
      "QUEUE" = "AlphaTest"
      "RenderType" = "TransparentCutout"
    }
    LOD 900
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "AlphaTest"
        "RenderType" = "TransparentCutout"
      }
      LOD 900
      Cull Off
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.texcoord.xy = in_v.texcoord.xy;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float2 u_xlat0_d;
      float4 u_xlat10_0;
      int u_xlatb0;
      float u_xlat3;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = TRANSFORM_TEX(in_f.texcoord.xy, _MainTex);
          u_xlat10_0 = tex2D(_MainTex, u_xlat0_d.xy);
          u_xlat3 = (u_xlat10_0.w + (-_Cutoff));
          out_f.color.xyz = (u_xlat10_0.xyz * _LightColor0.www);
          u_xlatb0 = (u_xlat3<0);
          if(u_xlatb0)
          {
              discard;
          }
          out_f.color.w = 1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
    Pass // ind: 2, name: ShadowCaster
    {
      Name "ShadowCaster"
      Tags
      { 
        "LIGHTMODE" = "SHADOWCASTER"
        "QUEUE" = "AlphaTest"
        "RenderType" = "TransparentCutout"
        "SHADOWSUPPORT" = "true"
      }
      LOD 900
      Cull Off
      Offset 1, 1
      // m_ProgramMask = 6
      CGPROGRAM
      #pragma multi_compile SHADOWS_DEPTH
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 unity_LightShadowBias;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float u_xlat4;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0 = UnityObjectToClipPos(in_v.vertex);
          u_xlat1.x = (unity_LightShadowBias.x / u_xlat0.w);
          u_xlat1.x = clamp(u_xlat1.x, 0, 1);
          u_xlat4 = (u_xlat0.z + u_xlat1.x);
          u_xlat1.x = max((-u_xlat0.w), u_xlat4);
          out_v.vertex.xyw = u_xlat0.xyw;
          u_xlat0.x = ((-u_xlat4) + u_xlat1.x);
          out_v.vertex.z = ((unity_LightShadowBias.y * u_xlat0.x) + u_xlat4);
          out_v.texcoord1.xy = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float u_xlat0_d;
      float u_xlat10_0;
      int u_xlatb0;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_MainTex, in_f.texcoord1.xy).w;
          u_xlat0_d = (u_xlat10_0 + (-_Cutoff));
          u_xlatb0 = (u_xlat0_d<0);
          if(u_xlatb0)
          {
              discard;
          }
          out_f.color = float4(0, 0, 0, 0);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "FORCENOSHADOWCASTING" = "true"
      "QUEUE" = "AlphaTest"
      "RenderType" = "TransparentCutout"
    }
    LOD 800
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "FORCENOSHADOWCASTING" = "true"
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "AlphaTest"
        "RenderType" = "TransparentCutout"
      }
      LOD 800
      Cull Off
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.texcoord.xy = in_v.texcoord.xy;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float2 u_xlat0_d;
      float4 u_xlat10_0;
      int u_xlatb0;
      float u_xlat3;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = TRANSFORM_TEX(in_f.texcoord.xy, _MainTex);
          u_xlat10_0 = tex2D(_MainTex, u_xlat0_d.xy);
          u_xlat3 = (u_xlat10_0.w + (-_Cutoff));
          out_f.color.xyz = (u_xlat10_0.xyz * _LightColor0.www);
          u_xlatb0 = (u_xlat3<0);
          if(u_xlatb0)
          {
              discard;
          }
          out_f.color.w = 1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "FORCENOSHADOWCASTING" = "true"
      "QUEUE" = "AlphaTest"
      "RenderType" = "TransparentCutout"
    }
    LOD 700
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "FORCENOSHADOWCASTING" = "true"
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "AlphaTest"
        "RenderType" = "TransparentCutout"
      }
      LOD 700
      Cull Off
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.texcoord.xy = in_v.texcoord.xy;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float2 u_xlat0_d;
      float4 u_xlat10_0;
      int u_xlatb0;
      float u_xlat3;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = TRANSFORM_TEX(in_f.texcoord.xy, _MainTex);
          u_xlat10_0 = tex2D(_MainTex, u_xlat0_d.xy);
          u_xlat3 = (u_xlat10_0.w + (-_Cutoff));
          out_f.color.xyz = (u_xlat10_0.xyz * _LightColor0.www);
          u_xlatb0 = (u_xlat3<0);
          if(u_xlatb0)
          {
              discard;
          }
          out_f.color.w = 1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "FORCENOSHADOWCASTING" = "true"
      "QUEUE" = "AlphaTest"
      "RenderType" = "TransparentCutout"
    }
    LOD 600
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "FORCENOSHADOWCASTING" = "true"
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "AlphaTest"
        "RenderType" = "TransparentCutout"
      }
      LOD 600
      Cull Off
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _MainTex_ST;
      uniform float _Cutoff;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.texcoord.xy = in_v.texcoord.xy;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float2 u_xlat0_d;
      float4 u_xlat10_0;
      int u_xlatb0;
      float u_xlat3;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xy = TRANSFORM_TEX(in_f.texcoord.xy, _MainTex);
          u_xlat10_0 = tex2D(_MainTex, u_xlat0_d.xy);
          u_xlat3 = (u_xlat10_0.w + (-_Cutoff));
          out_f.color.xyz = u_xlat10_0.xyz;
          u_xlatb0 = (u_xlat3<0);
          if(u_xlatb0)
          {
              discard;
          }
          out_f.color.w = 1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Unlit/Texture"
}
