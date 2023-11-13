Shader "Time of Day/Sun"
{
  Properties
  {
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Background+30"
      "RenderType" = "Background"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Background+30"
        "RenderType" = "Background"
      }
      ZWrite Off
      Fog
      { 
        Mode  Off
      } 
      Blend One One
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
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4x4 TOD_World2Sky;
      uniform float3 TOD_SunMeshColor;
      uniform float TOD_SunMeshContrast;
      uniform float TOD_SunMeshBrightness;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float3 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float4 u_xlat2;
      float u_xlat3;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_3(unity_ObjectToWorld) * in_v.vertex.wwww) + u_xlat0);
          out_v.vertex = mul(unity_MatrixVP, u_xlat1);
          u_xlat3 = (u_xlat0.y * conv_mxt4x4_1(TOD_World2Sky).y);
          u_xlat0.x = ((conv_mxt4x4_0(TOD_World2Sky).y * u_xlat0.x) + u_xlat3);
          u_xlat0.x = ((conv_mxt4x4_2(TOD_World2Sky).y * u_xlat0.z) + u_xlat0.x);
          u_xlat0.x = ((conv_mxt4x4_3(TOD_World2Sky).y * u_xlat0.w) + u_xlat0.x);
          u_xlat0.z = (u_xlat0.x * 25);
          u_xlat0.xy = ((in_v.texcoord.xy * float2(2, 2)) + float2(-1, (-1)));
          out_v.texcoord.xyz = u_xlat0.xyz;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float u_xlat16_0;
      float u_xlat1_d;
      float u_xlat16_2;
      float u_xlat3_d;
      int u_xlatb3;
      float u_xlat5;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat16_0 = length(in_f.texcoord.xy);
          u_xlat1_d = log2(u_xlat16_0);
          u_xlatb3 = (0.5>=u_xlat16_0);
          u_xlat3_d = (u_xlatb3)?(TOD_SunMeshBrightness):(float(0));
          u_xlat1_d = (u_xlat1_d * TOD_SunMeshContrast);
          u_xlat1_d = exp2(u_xlat1_d);
          u_xlat1_d = ((-u_xlat1_d) + 1);
          u_xlat1_d = max(u_xlat1_d, 0);
          u_xlat5 = ((u_xlat1_d * (-2)) + 3);
          u_xlat1_d = (u_xlat1_d * u_xlat1_d);
          u_xlat1_d = (u_xlat1_d * u_xlat5);
          u_xlat5 = TOD_SunMeshBrightness;
          u_xlat5 = clamp(u_xlat5, 0, 1);
          u_xlat16_0 = ((u_xlat1_d * u_xlat5) + u_xlat3_d);
          u_xlat16_2 = in_f.texcoord.z;
          u_xlat16_2 = clamp(u_xlat16_2, 0, 1);
          u_xlat16_0 = (u_xlat16_0 * u_xlat16_2);
          out_f.color.xyz = (float3(u_xlat16_0, u_xlat16_0, u_xlat16_0) * TOD_SunMeshColor.xyz);
          out_f.color.w = 0;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
