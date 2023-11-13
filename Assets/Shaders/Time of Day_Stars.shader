Shader "Time of Day/Stars"
{
  Properties
  {
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Background+20"
      "RenderType" = "Background"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Background+20"
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
      //uniform float4 _ScreenParams;
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 UNITY_MATRIX_P;
      //uniform float4x4 unity_MatrixVP;
      uniform float4x4 TOD_World2Sky;
      uniform float TOD_StarSize;
      uniform float TOD_StarBrightness;
      uniform float TOD_StarVisibility;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 tangent :TANGENT0;
          float3 normal :NORMAL0;
          float4 texcoord :TEXCOORD0;
          float4 color :COLOR0;
      };
      
      struct OUT_Data_Vert
      {
          float3 color :COLOR0;
          float3 texcoord :TEXCOORD0;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 color :COLOR0;
          float3 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float3 u_xlat0;
      float4 u_xlat1;
      float4 u_xlat2;
      float4 u_xlat3;
      float u_xlat12;
      float u_xlat13;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          u_xlat0.xyz = (in_v.tangent.yzx * in_v.normal.zxy);
          u_xlat0.xyz = ((in_v.normal.yzx * in_v.tangent.zxy) + (-u_xlat0.xyz));
          u_xlat1.xy = (in_v.texcoord.xy + float2(-0.5, (-0.5)));
          u_xlat0.xyz = (u_xlat0.xyz * u_xlat1.yyy);
          u_xlat1.xyz = (u_xlat1.xxx * in_v.tangent.xyz);
          u_xlat12 = max(conv_mxt4x4_0(UNITY_MATRIX_P).x, 0.100000001);
          u_xlat12 = (float(1) / u_xlat12);
          u_xlat12 = (u_xlat12 * 4);
          u_xlat12 = (u_xlat12 / _ScreenParams.x);
          u_xlat13 = (u_xlat12 * TOD_StarSize);
          u_xlat12 = (u_xlat12 * u_xlat12);
          u_xlat1.xyz = (((-u_xlat1.xyz) * float3(u_xlat13, u_xlat13, u_xlat13)) + in_v.vertex.xyz);
          u_xlat0.xyz = (((-u_xlat0.xyz) * float3(u_xlat13, u_xlat13, u_xlat13)) + u_xlat1.xyz);
          u_xlat1 = (u_xlat0.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat1 = ((conv_mxt4x4_0(unity_ObjectToWorld) * u_xlat0.xxxx) + u_xlat1);
          u_xlat1 = ((conv_mxt4x4_2(unity_ObjectToWorld) * u_xlat0.zzzz) + u_xlat1);
          u_xlat2 = (u_xlat1 + conv_mxt4x4_3(unity_ObjectToWorld));
          u_xlat1 = ((conv_mxt4x4_3(unity_ObjectToWorld) * in_v.vertex.wwww) + u_xlat1);
          out_v.vertex = mul(unity_MatrixVP, u_xlat2);
          u_xlat0.x = (TOD_StarBrightness * TOD_StarVisibility);
          u_xlat0.x = (u_xlat0.x * 9.99999975E-06);
          u_xlat0.x = (u_xlat0.x * in_v.color.w);
          u_xlat0.x = (u_xlat0.x / u_xlat12);
          out_v.color.xyz = sqrt(u_xlat0.xxx);
          u_xlat0.x = (u_xlat1.y * conv_mxt4x4_1(TOD_World2Sky).y);
          u_xlat0.x = ((conv_mxt4x4_0(TOD_World2Sky).y * u_xlat1.x) + u_xlat0.x);
          u_xlat0.x = ((conv_mxt4x4_2(TOD_World2Sky).y * u_xlat1.z) + u_xlat0.x);
          u_xlat0.x = ((conv_mxt4x4_3(TOD_World2Sky).y * u_xlat1.w) + u_xlat0.x);
          u_xlat0.z = (u_xlat0.x * 25);
          u_xlat0.xy = ((in_v.texcoord.xy * float2(2, 2)) + float2(-1, (-1)));
          out_v.texcoord.xyz = u_xlat0.xyz;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float u_xlat16_0;
      float u_xlat16_1;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat16_0 = length(in_f.texcoord.xy);
          u_xlat16_0 = ((-u_xlat16_0) + 1);
          u_xlat16_0 = max(u_xlat16_0, 0);
          u_xlat16_1 = in_f.texcoord.z;
          u_xlat16_1 = clamp(u_xlat16_1, 0, 1);
          u_xlat16_0 = (u_xlat16_0 * u_xlat16_1);
          out_f.color.xyz = (float3(u_xlat16_0, u_xlat16_0, u_xlat16_0) * in_f.color.xyz);
          out_f.color.w = 0;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
