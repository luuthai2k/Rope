Shader "Time of Day/Moon"
{
  Properties
  {
    _MainTex ("Base (RGB)", 2D) = "white" {}
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Background+40"
      "RenderType" = "Background"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Background+40"
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
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4x4 TOD_World2Sky;
      uniform float4 _MainTex_ST;
      uniform float3 TOD_MoonMeshColor;
      uniform float3 TOD_SunDirection;
      uniform float TOD_MoonMeshContrast;
      uniform float TOD_MoonMeshBrightness;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float3 normal :NORMAL0;
          float4 texcoord :TEXCOORD0;
      };
      
      struct OUT_Data_Vert
      {
          float3 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float3 texcoord :TEXCOORD0;
          float3 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float4 u_xlat2;
      float u_xlat3;
      float u_xlat9;
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
          u_xlat0.xy = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.texcoord.xyz = u_xlat0.xyz;
          u_xlat0.xyz = (in_v.normal.yyy * conv_mxt4x4_1(unity_ObjectToWorld).xyz);
          u_xlat0.xyz = ((conv_mxt4x4_0(unity_ObjectToWorld).xyz * in_v.normal.xxx) + u_xlat0.xyz);
          u_xlat0.xyz = ((conv_mxt4x4_2(unity_ObjectToWorld).xyz * in_v.normal.zzz) + u_xlat0.xyz);
          u_xlat0.xyz = normalize(u_xlat0.xyz);
          out_v.texcoord1.xyz = u_xlat0.xyz;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float u_xlat0_d;
      float3 u_xlat16_1;
      float u_xlat2_d;
      float3 u_xlat10_2;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d = dot(in_f.texcoord1.xyz, TOD_SunDirection.xyz);
          u_xlat0_d = max(u_xlat0_d, 0);
          u_xlat0_d = log2(u_xlat0_d);
          u_xlat0_d = (u_xlat0_d * TOD_MoonMeshContrast);
          u_xlat0_d = exp2(u_xlat0_d);
          u_xlat16_1.x = in_f.texcoord.z;
          u_xlat16_1.x = clamp(u_xlat16_1.x, 0, 1);
          u_xlat2_d = (u_xlat16_1.x * TOD_MoonMeshBrightness);
          u_xlat0_d = (u_xlat0_d * u_xlat2_d);
          u_xlat10_2.xyz = tex2D(_MainTex, in_f.texcoord.xy).xyz;
          u_xlat16_1.xyz = (float3(u_xlat0_d, u_xlat0_d, u_xlat0_d) * u_xlat10_2.xyz);
          out_f.color.xyz = (u_xlat16_1.xyz * TOD_MoonMeshColor.xyz);
          out_f.color.w = 0;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
