Shader "Naxeex/Glass"
{
  Properties
  {
    _Color ("Color", Color) = (0,0,0,1)
    _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
    _StaticReflectionIntensity ("Static Reflection Intensity", float) = 1
    _CubeReflectionMin ("Cube Reflection Min", float) = 0.05
    [Toggle(FREEZE_EFFECT)] _Freeze_effect ("Use freeze effect", float) = 0
    _FreezeTex ("Freeze Effect Texture", 2D) = "black" {}
    _FreezeAmount ("Freeze Effect Amount", Range(0, 1)) = 0
    _FreezeDiffMin ("Freeze Diffuse min", Range(0, 1)) = 0.25
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "LIGHTMODE" = "ALWAYS"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    LOD 800
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      LOD 800
      ZWrite Off
      Cull Off
      Blend One OneMinusSrcAlpha
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
      //uniform float3 _WorldSpaceCameraPos;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_WorldToObject;
      //uniform float4x4 unity_MatrixVP;
      uniform float _StaticReflectionIntensity;
      uniform float _CubeReflectionMin;
      uniform float4 _Color;
      uniform samplerCUBE _Cube;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
      };
      
      struct OUT_Data_Vert
      {
          float3 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 texcoord :TEXCOORD0;
          float2 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float4 u_xlat2;
      float u_xlat3;
      float u_xlat6;
      float u_xlat9;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          u_xlat0.xyz = ((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz);
          u_xlat0.xyz = ((-u_xlat0.xyz) + _WorldSpaceCameraPos.xyz);
          out_v.vertex = mul(unity_MatrixVP, u_xlat1);
          u_xlat0.xyz = normalize(u_xlat0.xyz);
          u_xlat1.x = dot(in_v.normal.xyz, conv_mxt4x4_0(unity_WorldToObject).xyz);
          u_xlat1.y = dot(in_v.normal.xyz, conv_mxt4x4_1(unity_WorldToObject).xyz);
          u_xlat1.z = dot(in_v.normal.xyz, conv_mxt4x4_2(unity_WorldToObject).xyz);
          u_xlat1.xyz = normalize(u_xlat1.xyz);
          u_xlat9 = dot((-u_xlat0.xyz), u_xlat1.xyz);
          u_xlat9 = (u_xlat9 + u_xlat9);
          out_v.texcoord.xyz = ((u_xlat1.xyz * (-float3(u_xlat9, u_xlat9, u_xlat9))) + (-u_xlat0.xyz));
          u_xlat0.x = dot(u_xlat0.xyz, u_xlat1.xyz);
          u_xlat0.x = ((-abs(u_xlat0.x)) + 1);
          u_xlat3 = (u_xlat0.x * u_xlat0.x);
          u_xlat3 = (u_xlat3 * u_xlat3);
          u_xlat6 = (u_xlat3 * u_xlat0.x);
          u_xlat0.x = (((-u_xlat0.x) * u_xlat3) + 1);
          u_xlat0.x = ((_CubeReflectionMin * u_xlat0.x) + u_xlat6);
          out_v.texcoord1.x = (u_xlat0.x * _StaticReflectionIntensity);
          out_v.texcoord1.y = u_xlat0.x;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float3 u_xlat10_0;
      float3 u_xlat16_1;
      float u_xlat2_d;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0.xyz = texCUBE(_Cube, in_f.texcoord.xyz).xyz;
          u_xlat16_1.xyz = (u_xlat10_0.xyz * u_xlat10_0.xyz);
          u_xlat0_d.xyz = (u_xlat16_1.xyz * in_f.texcoord1.xxx);
          u_xlat0_d.xyz = ((u_xlat0_d.xyz * _Color.xyz) + _Color.xyz);
          u_xlat2_d = ((-_Color.w) + 1);
          u_xlat0_d.w = ((in_f.texcoord1.y * u_xlat2_d) + _Color.w);
          out_f.color = (u_xlat0_d * _Color.wwww);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "LIGHTMODE" = "ALWAYS"
      "QUEUE" = "Transparent"
      "RenderType" = "Transparent"
    }
    LOD 600
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "LIGHTMODE" = "ALWAYS"
        "QUEUE" = "Transparent"
        "RenderType" = "Transparent"
      }
      LOD 600
      ZWrite Off
      Cull Off
      Blend One OneMinusSrcAlpha
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _Color;
      struct appdata_t
      {
          float4 vertex :POSITION0;
      };
      
      struct OUT_Data_Vert
      {
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 vertex :Position;
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
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          out_f.color = (_Color.wwww * _Color);
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
