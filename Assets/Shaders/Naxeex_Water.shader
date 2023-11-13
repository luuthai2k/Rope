Shader "Naxeex/Water"
{
  Properties
  {
    _Color ("Shallow Color (Use on Ultra)", Color) = (0.172,0.463,0.435,0)
    _DepthColor ("Depth Color", Color) = (0,0,0,0)
    _CubeColor ("CubeMap Color [RGB] - Horizont", Color) = (1,1,1,1)
    _BumpMap ("Micro Detail", 2D) = "bump" {}
    _Speeds ("Speeds", Vector) = (0.1,0.7,0.1,0.7)
  }
  SubShader
  {
    Tags
    { 
      "LIGHTMODE" = "ALWAYS"
      "QUEUE" = "Geometry+399"
      "RenderType" = "Opaque"
    }
    LOD 800
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "Geometry+399"
        "RenderType" = "Opaque"
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
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _Speeds;
      uniform float4 _BumpMap_ST;
      //uniform float3 _WorldSpaceCameraPos;
      uniform float4 _Color;
      uniform float4 _DepthColor;
      uniform float4 _CubeColor;
      uniform sampler2D _BumpMap;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float3 vs_NORMAL0 :NORMAL0;
          float3 texcoord3 :TEXCOORD3;
          float4 texcoord4 :TEXCOORD4;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 vs_NORMAL0 :NORMAL0;
          float3 texcoord3 :TEXCOORD3;
          float4 texcoord4 :TEXCOORD4;
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
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          out_v.texcoord3.xyz = ((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz);
          out_v.vertex = mul(unity_MatrixVP, u_xlat1);
          u_xlat0.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat0.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat0.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          out_v.vs_NORMAL0.xyz = normalize(u_xlat0.xyz);
          u_xlat0 = (_Time.xxxx * _Speeds);
          u_xlat0 = frac(u_xlat0);
          u_xlat1.xy = TRANSFORM_TEX(in_v.texcoord.xy, _BumpMap);
          u_xlat1.zw = (u_xlat1.xy * float2(0.5, 0.5));
          out_v.texcoord4 = (u_xlat0 + u_xlat1);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float3 u_xlat0_d;
      float u_xlat10_0;
      float3 u_xlat16_1;
      float u_xlat10_2;
      float u_xlat6_d;
      float u_xlat16_7;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xyz = ((-in_f.texcoord3.xyz) + _WorldSpaceCameraPos.xyz);
          u_xlat0_d.xyz = normalize(u_xlat0_d.xyz);
          u_xlat0_d.x = dot(in_f.vs_NORMAL0.xyz, u_xlat0_d.xyz);
          u_xlat0_d.x = ((-u_xlat0_d.x) + 1);
          u_xlat0_d.x = clamp(u_xlat0_d.x, 0, 1);
          u_xlat16_1.xyz = ((-_DepthColor.xyz) + _CubeColor.xyz);
          u_xlat16_1.xyz = ((u_xlat0_d.xxx * u_xlat16_1.xyz) + _DepthColor.xyz);
          u_xlat10_0 = tex2D(_BumpMap, in_f.texcoord4.xy).z;
          u_xlat10_2 = tex2D(_BumpMap, in_f.texcoord4.zw).z;
          u_xlat16_7 = (u_xlat10_2 + u_xlat10_0);
          u_xlat16_7 = (u_xlat16_7 * 0.5);
          out_f.color.xyz = (float3(u_xlat16_7, u_xlat16_7, u_xlat16_7) * u_xlat16_1.xyz);
          out_f.color.w = _Color.w;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "LIGHTMODE" = "ALWAYS"
      "QUEUE" = "Geometry+399"
      "RenderType" = "Opaque"
    }
    LOD 700
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "Geometry+399"
        "RenderType" = "Opaque"
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
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4 _Time;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _Speeds;
      uniform float4 _BumpMap_ST;
      //uniform float3 _WorldSpaceCameraPos;
      uniform float4 _Color;
      uniform float4 _DepthColor;
      uniform float4 _CubeColor;
      uniform sampler2D _BumpMap;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float3 vs_NORMAL0 :NORMAL0;
          float3 texcoord3 :TEXCOORD3;
          float4 texcoord4 :TEXCOORD4;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 vs_NORMAL0 :NORMAL0;
          float3 texcoord3 :TEXCOORD3;
          float4 texcoord4 :TEXCOORD4;
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
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          out_v.texcoord3.xyz = ((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz);
          out_v.vertex = mul(unity_MatrixVP, u_xlat1);
          u_xlat0.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat0.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat0.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          out_v.vs_NORMAL0.xyz = normalize(u_xlat0.xyz);
          u_xlat0 = (_Time.xxxx * _Speeds);
          u_xlat0 = frac(u_xlat0);
          u_xlat1.xy = TRANSFORM_TEX(in_v.texcoord.xy, _BumpMap);
          u_xlat1.zw = (u_xlat1.xy * float2(0.5, 0.5));
          out_v.texcoord4 = (u_xlat0 + u_xlat1);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float3 u_xlat0_d;
      float u_xlat10_0;
      float3 u_xlat16_1;
      float u_xlat10_2;
      float u_xlat6_d;
      float u_xlat16_7;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d.xyz = ((-in_f.texcoord3.xyz) + _WorldSpaceCameraPos.xyz);
          u_xlat0_d.xyz = normalize(u_xlat0_d.xyz);
          u_xlat0_d.x = dot(in_f.vs_NORMAL0.xyz, u_xlat0_d.xyz);
          u_xlat0_d.x = ((-u_xlat0_d.x) + 1);
          u_xlat0_d.x = clamp(u_xlat0_d.x, 0, 1);
          u_xlat16_1.xyz = ((-_DepthColor.xyz) + _CubeColor.xyz);
          u_xlat16_1.xyz = ((u_xlat0_d.xxx * u_xlat16_1.xyz) + _DepthColor.xyz);
          u_xlat10_0 = tex2D(_BumpMap, in_f.texcoord4.xy).z;
          u_xlat10_2 = tex2D(_BumpMap, in_f.texcoord4.zw).z;
          u_xlat16_7 = (u_xlat10_2 + u_xlat10_0);
          u_xlat16_7 = (u_xlat16_7 * 0.5);
          out_f.color.xyz = (float3(u_xlat16_7, u_xlat16_7, u_xlat16_7) * u_xlat16_1.xyz);
          out_f.color.w = _Color.w;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "LIGHTMODE" = "ALWAYS"
      "QUEUE" = "Geometry+399"
      "RenderType" = "Opaque"
    }
    LOD 600
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "Geometry+399"
        "RenderType" = "Opaque"
      }
      LOD 600
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
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _Speeds;
      uniform float4 _BumpMap_ST;
      uniform float4 _CubeColor;
      uniform sampler2D _BumpMap;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float2 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float3 vs_NORMAL0 :NORMAL0;
          float3 texcoord3 :TEXCOORD3;
          float4 texcoord4 :TEXCOORD4;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 texcoord4 :TEXCOORD4;
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
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          out_v.texcoord3.xyz = ((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz);
          out_v.vertex = mul(unity_MatrixVP, u_xlat1);
          u_xlat0.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat0.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat0.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          out_v.vs_NORMAL0.xyz = normalize(u_xlat0.xyz);
          u_xlat0 = (_Time.xxxx * _Speeds);
          u_xlat0 = frac(u_xlat0);
          u_xlat1.xy = TRANSFORM_TEX(in_v.texcoord.xy, _BumpMap);
          u_xlat1.zw = (u_xlat1.xy * float2(0.5, 0.5));
          out_v.texcoord4 = (u_xlat0 + u_xlat1);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float u_xlat10_0;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_BumpMap, in_f.texcoord4.xy).z;
          out_f.color.xyz = (float3(u_xlat10_0, u_xlat10_0, u_xlat10_0) * _CubeColor.xyz);
          out_f.color.w = _CubeColor.w;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Unlit/Color"
}
