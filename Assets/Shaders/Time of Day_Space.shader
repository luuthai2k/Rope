Shader "Time of Day/Space"
{
  Properties
  {
    _CubeTex ("Cube (RGB)", Cube) = "black" {}
    _Brightness ("Brightness", float) = 0
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Background+10"
      "RenderType" = "Background"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Background+10"
        "RenderType" = "Background"
      }
      ZWrite Off
      Fog
      { 
        Mode  Off
      } 
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
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float TOD_StarVisibility;
      uniform float _Brightness;
      uniform samplerCUBE _CubeTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
      };
      
      struct OUT_Data_Vert
      {
          float4 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float u_xlat2;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          u_xlat0.x = dot(in_v.vertex.xyz, in_v.vertex.xyz);
          u_xlat0.x = rsqrt(u_xlat0.x);
          u_xlat0.xyz = (u_xlat0.xxx * in_v.vertex.xyz);
          u_xlat1.xyz = (u_xlat0.yyy * conv_mxt4x4_1(unity_ObjectToWorld).xyz);
          u_xlat1.xyz = ((conv_mxt4x4_0(unity_ObjectToWorld).xyz * u_xlat0.xxx) + u_xlat1.xyz);
          u_xlat1.xyz = ((conv_mxt4x4_2(unity_ObjectToWorld).xyz * u_xlat0.zzz) + u_xlat1.xyz);
          out_v.texcoord.xyz = u_xlat0.xyz;
          u_xlat0.x = dot(u_xlat1.xyz, u_xlat1.xyz);
          u_xlat0.x = rsqrt(u_xlat0.x);
          u_xlat0.x = (u_xlat0.x * u_xlat1.y);
          u_xlat2 = (TOD_StarVisibility * _Brightness);
          out_v.texcoord.w = (u_xlat0.x * u_xlat2);
          out_v.texcoord.w = clamp(out_v.texcoord.w, 0, 1);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float3 u_xlat0_d;
      float3 u_xlat10_0;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0.xyz = texCUBE(_CubeTex, in_f.texcoord.xyz).xyz;
          u_xlat0_d.xyz = (u_xlat10_0.xyz * in_f.texcoord.www);
          out_f.color.xyz = u_xlat0_d.xyz;
          out_f.color.w = 0;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
