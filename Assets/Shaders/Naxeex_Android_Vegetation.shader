Shader "Naxeex/Android/Vegetation"
{
  Properties
  {
    _AtlasVegetationRGBA ("AtlasVegetation (RGBA)", 2D) = "white" {}
    _AlphaCutoff ("Alpha Cutoff", Range(0, 1)) = 0.5
    _Turbulence ("Turbulence", float) = 0
    [Enum(UnityEngine.Rendering.CullMode)] _Cull ("Cull", float) = 2
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
        "LIGHTMODE" = "FORWARDBASE"
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
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _Time;
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_AmbientSky;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _AtlasVegetationRGBA_ST;
      uniform float _Turbulence;
      uniform float _AlphaCutoff;
      uniform sampler2D _AtlasVegetationRGBA;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float4 color :COLOR0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float3 u_xlat2;
      float2 u_xlat4;
      float u_xlat6;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0.xy = (in_v.vertex.yy * conv_mxt4x4_1(unity_ObjectToWorld).xy);
          u_xlat0.xy = ((conv_mxt4x4_0(unity_ObjectToWorld).xy * in_v.vertex.xx) + u_xlat0.xy);
          u_xlat0.xy = ((conv_mxt4x4_2(unity_ObjectToWorld).xy * in_v.vertex.zz) + u_xlat0.xy);
          u_xlat0.xy = ((conv_mxt4x4_3(unity_ObjectToWorld).xy * in_v.vertex.ww) + u_xlat0.xy);
          u_xlat4.xy = (u_xlat0.xy + float2(0.212699994, 0.212699994));
          u_xlat0.xy = ((u_xlat0.xy * float2(0.371300012, 0.371300012)) + u_xlat4.xy);
          u_xlat2.xy = (u_xlat0.xy * float2(489.122986, 489.122986));
          u_xlat0.x = (u_xlat0.x + 1);
          u_xlat2.xy = sin(u_xlat2.xy);
          u_xlat2.xy = (u_xlat2.xy * float2(4.78900003, 4.78900003));
          u_xlat2.x = (u_xlat2.y * u_xlat2.x);
          u_xlat0.x = (u_xlat0.x * u_xlat2.x);
          u_xlat0.x = frac(u_xlat0.x);
          u_xlat0.x = (u_xlat0.x * _Time.y);
          u_xlat0.x = (u_xlat0.x * 20);
          u_xlat0.x = sin(u_xlat0.x);
          u_xlat0.x = (u_xlat0.x * in_v.color.y);
          u_xlat2.xyz = (in_v.normal.xyz * float3(float3(_Turbulence, _Turbulence, _Turbulence)));
          u_xlat0.xyz = ((u_xlat0.xxx * u_xlat2.xyz) + in_v.vertex.xyz);
          out_v.vertex = UnityObjectToClipPos(u_xlat0);
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _AtlasVegetationRGBA);
          u_xlat0.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat0.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat0.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          u_xlat0.xyz = normalize(u_xlat0.xyz);
          u_xlat0.x = dot(u_xlat0.xyz, _WorldSpaceLightPos0.xyz);
          u_xlat0.x = clamp(u_xlat0.x, 0, 1);
          out_v.texcoord1.xyz = ((u_xlat0.xxx * _LightColor0.xyz) + unity_AmbientSky.xyz);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat10_0;
      int u_xlatb0;
      float u_xlat3;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_AtlasVegetationRGBA, in_f.texcoord.xy);
          u_xlat3 = (u_xlat10_0.w + (-_AlphaCutoff));
          out_f.color.xyz = (u_xlat10_0.xyz * in_f.texcoord1.xyz);
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
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _Time;
      //uniform float4 unity_LightShadowBias;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _AtlasVegetationRGBA_ST;
      uniform float _Turbulence;
      uniform float _AlphaCutoff;
      uniform sampler2D _AtlasVegetationRGBA;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float4 color :COLOR0;
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
      float3 u_xlat2;
      float2 u_xlat4;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0.xy = (in_v.vertex.yy * conv_mxt4x4_1(unity_ObjectToWorld).xy);
          u_xlat0.xy = ((conv_mxt4x4_0(unity_ObjectToWorld).xy * in_v.vertex.xx) + u_xlat0.xy);
          u_xlat0.xy = ((conv_mxt4x4_2(unity_ObjectToWorld).xy * in_v.vertex.zz) + u_xlat0.xy);
          u_xlat0.xy = ((conv_mxt4x4_3(unity_ObjectToWorld).xy * in_v.vertex.ww) + u_xlat0.xy);
          u_xlat4.xy = (u_xlat0.xy + float2(0.212699994, 0.212699994));
          u_xlat0.xy = ((u_xlat0.xy * float2(0.371300012, 0.371300012)) + u_xlat4.xy);
          u_xlat2.xy = (u_xlat0.xy * float2(489.122986, 489.122986));
          u_xlat0.x = (u_xlat0.x + 1);
          u_xlat2.xy = sin(u_xlat2.xy);
          u_xlat2.xy = (u_xlat2.xy * float2(4.78900003, 4.78900003));
          u_xlat2.x = (u_xlat2.y * u_xlat2.x);
          u_xlat0.x = (u_xlat0.x * u_xlat2.x);
          u_xlat0.x = frac(u_xlat0.x);
          u_xlat0.x = (u_xlat0.x * _Time.y);
          u_xlat0.x = (u_xlat0.x * 20);
          u_xlat0.x = sin(u_xlat0.x);
          u_xlat0.x = (u_xlat0.x * in_v.color.y);
          u_xlat2.xyz = (in_v.normal.xyz * float3(float3(_Turbulence, _Turbulence, _Turbulence)));
          u_xlat0.xyz = ((u_xlat0.xxx * u_xlat2.xyz) + in_v.vertex.xyz);
          u_xlat0 = UnityObjectToClipPos(u_xlat0);
          u_xlat1.x = (unity_LightShadowBias.x / u_xlat0.w);
          u_xlat1.x = clamp(u_xlat1.x, 0, 1);
          u_xlat4.x = (u_xlat0.z + u_xlat1.x);
          u_xlat1.x = max((-u_xlat0.w), u_xlat4.x);
          out_v.vertex.xyw = u_xlat0.xyw;
          u_xlat0.x = ((-u_xlat4.x) + u_xlat1.x);
          out_v.vertex.z = ((unity_LightShadowBias.y * u_xlat0.x) + u_xlat4.x);
          out_v.texcoord1.xy = TRANSFORM_TEX(in_v.texcoord.xy, _AtlasVegetationRGBA);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float u_xlat0_d;
      float u_xlat10_0;
      int u_xlatb0;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_AtlasVegetationRGBA, in_f.texcoord1.xy).w;
          u_xlat0_d = (u_xlat10_0 + (-_AlphaCutoff));
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
        "LIGHTMODE" = "FORWARDBASE"
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
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _Time;
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_AmbientSky;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _AtlasVegetationRGBA_ST;
      uniform float _Turbulence;
      uniform float _AlphaCutoff;
      uniform sampler2D _AtlasVegetationRGBA;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float4 color :COLOR0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float3 u_xlat2;
      float2 u_xlat4;
      float u_xlat6;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0.xy = (in_v.vertex.yy * conv_mxt4x4_1(unity_ObjectToWorld).xy);
          u_xlat0.xy = ((conv_mxt4x4_0(unity_ObjectToWorld).xy * in_v.vertex.xx) + u_xlat0.xy);
          u_xlat0.xy = ((conv_mxt4x4_2(unity_ObjectToWorld).xy * in_v.vertex.zz) + u_xlat0.xy);
          u_xlat0.xy = ((conv_mxt4x4_3(unity_ObjectToWorld).xy * in_v.vertex.ww) + u_xlat0.xy);
          u_xlat4.xy = (u_xlat0.xy + float2(0.212699994, 0.212699994));
          u_xlat0.xy = ((u_xlat0.xy * float2(0.371300012, 0.371300012)) + u_xlat4.xy);
          u_xlat2.xy = (u_xlat0.xy * float2(489.122986, 489.122986));
          u_xlat0.x = (u_xlat0.x + 1);
          u_xlat2.xy = sin(u_xlat2.xy);
          u_xlat2.xy = (u_xlat2.xy * float2(4.78900003, 4.78900003));
          u_xlat2.x = (u_xlat2.y * u_xlat2.x);
          u_xlat0.x = (u_xlat0.x * u_xlat2.x);
          u_xlat0.x = frac(u_xlat0.x);
          u_xlat0.x = (u_xlat0.x * _Time.y);
          u_xlat0.x = (u_xlat0.x * 20);
          u_xlat0.x = sin(u_xlat0.x);
          u_xlat0.x = (u_xlat0.x * in_v.color.y);
          u_xlat2.xyz = (in_v.normal.xyz * float3(float3(_Turbulence, _Turbulence, _Turbulence)));
          u_xlat0.xyz = ((u_xlat0.xxx * u_xlat2.xyz) + in_v.vertex.xyz);
          out_v.vertex = UnityObjectToClipPos(u_xlat0);
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _AtlasVegetationRGBA);
          u_xlat0.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat0.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat0.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          u_xlat0.xyz = normalize(u_xlat0.xyz);
          u_xlat0.x = dot(u_xlat0.xyz, _WorldSpaceLightPos0.xyz);
          u_xlat0.x = clamp(u_xlat0.x, 0, 1);
          out_v.texcoord1.xyz = ((u_xlat0.xxx * _LightColor0.xyz) + unity_AmbientSky.xyz);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat10_0;
      int u_xlatb0;
      float u_xlat3;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_AtlasVegetationRGBA, in_f.texcoord.xy);
          u_xlat3 = (u_xlat10_0.w + (-_AlphaCutoff));
          out_f.color.xyz = (u_xlat10_0.xyz * in_f.texcoord1.xyz);
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
        "LIGHTMODE" = "FORWARDBASE"
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
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _WorldSpaceLightPos0;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4 unity_AmbientSky;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _AtlasVegetationRGBA_ST;
      uniform float _AlphaCutoff;
      uniform sampler2D _AtlasVegetationRGBA;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float2 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float u_xlat6;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _AtlasVegetationRGBA);
          u_xlat0.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat0.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat0.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          u_xlat0.xyz = normalize(u_xlat0.xyz);
          u_xlat0.x = dot(u_xlat0.xyz, _WorldSpaceLightPos0.xyz);
          u_xlat0.x = clamp(u_xlat0.x, 0, 1);
          out_v.texcoord1.xyz = ((u_xlat0.xxx * _LightColor0.xyz) + unity_AmbientSky.xyz);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat10_0;
      int u_xlatb0;
      float u_xlat3;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_AtlasVegetationRGBA, in_f.texcoord.xy);
          u_xlat3 = (u_xlat10_0.w + (-_AlphaCutoff));
          out_f.color.xyz = (u_xlat10_0.xyz * in_f.texcoord1.xyz);
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
        "LIGHTMODE" = "FORWARDBASE"
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
      uniform float4 _AtlasVegetationRGBA_ST;
      uniform float _AlphaCutoff;
      uniform sampler2D _AtlasVegetationRGBA;
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
          out_v.texcoord.xy = TRANSFORM_TEX(in_v.texcoord.xy, _AtlasVegetationRGBA);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat10_0;
      int u_xlatb0;
      float u_xlat3;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_AtlasVegetationRGBA, in_f.texcoord.xy);
          u_xlat3 = (u_xlat10_0.w + (-_AlphaCutoff));
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
