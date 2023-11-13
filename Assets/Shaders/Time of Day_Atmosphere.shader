Shader "Time of Day/Atmosphere"
{
  Properties
  {
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Background+50"
      "RenderType" = "Background"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Background+50"
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
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float3 TOD_SunSkyColor;
      uniform float3 TOD_MoonSkyColor;
      uniform float3 TOD_FogColor;
      uniform float3 TOD_GroundColor;
      uniform float3 TOD_LocalSunDirection;
      uniform float3 TOD_LocalMoonDirection;
      uniform float TOD_Contrast;
      uniform float TOD_Brightness;
      uniform float TOD_Fogginess;
      uniform float TOD_MoonHaloPower;
      uniform float3 TOD_MoonHaloColor;
      uniform float3 TOD_kBetaMie;
      uniform float4 TOD_kSun;
      uniform float4 TOD_k4PI;
      uniform float4 TOD_kRadius;
      uniform float4 TOD_kScale;
      struct appdata_t
      {
          float4 vertex :POSITION0;
      };
      
      struct OUT_Data_Vert
      {
          float4 texcoord1 :TEXCOORD1;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 texcoord1 :TEXCOORD1;
      };
      
      struct OUT_Data_Frag
      {
          float4 color :SV_Target0;
      };
      
      float4 u_xlat0;
      float4 u_xlat1;
      float3 u_xlat2;
      float3 u_xlat3;
      float3 u_xlat4;
      float3 u_xlat5;
      float4 u_xlat6;
      float u_xlat7;
      float3 u_xlat8;
      float3 u_xlat11;
      float u_xlat14;
      float u_xlat22;
      float u_xlat24;
      float u_xlat25;
      float u_xlat26;
      int u_xlati28;
      float u_xlat29;
      int u_xlatb29;
      float u_xlat30;
      OUT_Data_Vert vert(appdata_t in_v)
      {
          OUT_Data_Vert out_v;
          out_v.vertex = UnityObjectToClipPos(in_v.vertex);
          u_xlat0.x = dot(in_v.vertex.xyz, in_v.vertex.xyz);
          u_xlat0.x = rsqrt(u_xlat0.x);
          u_xlat0.xyz = (u_xlat0.xxx * in_v.vertex.yxz);
          u_xlat0.w = max(u_xlat0.x, 0);
          u_xlat1.x = dot(u_xlat0.yzw, u_xlat0.yzw);
          u_xlat1.x = rsqrt(u_xlat1.x);
          u_xlat1.xyz = (u_xlat0.ywz * u_xlat1.xxx);
          u_xlat2.y = (TOD_kRadius.x + TOD_kScale.w);
          u_xlat25 = (u_xlat1.y * u_xlat1.y);
          u_xlat25 = ((u_xlat25 * TOD_kRadius.y) + TOD_kRadius.w);
          u_xlat25 = (u_xlat25 + (-TOD_kRadius.y));
          u_xlat25 = sqrt(u_xlat25);
          u_xlat25 = (((-TOD_kRadius.x) * u_xlat1.y) + u_xlat25);
          u_xlat26 = ((-TOD_kScale.w) * TOD_kScale.z);
          u_xlat26 = (u_xlat26 * 1.44269502);
          u_xlat26 = exp2(u_xlat26);
          u_xlat3.x = (u_xlat1.y * u_xlat2.y);
          u_xlat3.x = (u_xlat3.x / u_xlat2.y);
          u_xlat3.x = ((-u_xlat3.x) + 1);
          u_xlat11.x = ((u_xlat3.x * 5.25) + (-6.80000019));
          u_xlat11.x = ((u_xlat3.x * u_xlat11.x) + 3.82999992);
          u_xlat11.x = ((u_xlat3.x * u_xlat11.x) + 0.458999991);
          u_xlat3.x = ((u_xlat3.x * u_xlat11.x) + (-0.00286999997));
          u_xlat3.x = (u_xlat3.x * 1.44269502);
          u_xlat3.x = exp2(u_xlat3.x);
          u_xlat26 = (u_xlat26 * u_xlat3.x);
          u_xlat25 = (u_xlat25 * 0.5);
          u_xlat3.x = (u_xlat25 * TOD_kScale.x);
          u_xlat11.xyz = (float3(u_xlat25, u_xlat25, u_xlat25) * u_xlat1.xyz);
          u_xlat2.x = float(0);
          u_xlat2.z = float(0);
          u_xlat2.xyz = ((u_xlat11.xyz * float3(0.5, 0.5, 0.5)) + u_xlat2.xyz);
          u_xlat11.xyz = (TOD_k4PI.www + TOD_k4PI.xyz);
          u_xlat4.xyz = u_xlat2.xyz;
          u_xlat5.x = float(0);
          u_xlat5.y = float(0);
          u_xlat5.z = float(0);
          int u_xlati_loop_1 = 0;
          while((u_xlati_loop_1<2))
          {
              u_xlat29 = length(u_xlat4.xyz);
              u_xlat29 = max(u_xlat29, 1);
              u_xlat6.x = (float(1) / u_xlat29);
              u_xlat29 = ((-u_xlat29) + TOD_kRadius.x);
              u_xlat29 = (u_xlat29 * TOD_kScale.z);
              u_xlat29 = (u_xlat29 * 1.44269502);
              u_xlat29 = exp2(u_xlat29);
              u_xlat14 = (u_xlat3.x * u_xlat29);
              u_xlat22 = dot(u_xlat1.xyz, u_xlat4.xyz);
              u_xlat30 = dot(TOD_LocalSunDirection.xyz, u_xlat4.xyz);
              u_xlat30 = (((-u_xlat30) * u_xlat6.x) + 1);
              u_xlat7 = ((u_xlat30 * 5.25) + (-6.80000019));
              u_xlat7 = ((u_xlat30 * u_xlat7) + 3.82999992);
              u_xlat7 = ((u_xlat30 * u_xlat7) + 0.458999991);
              u_xlat30 = ((u_xlat30 * u_xlat7) + (-0.00286999997));
              u_xlat30 = (u_xlat30 * 1.44269502);
              u_xlat30 = exp2(u_xlat30);
              u_xlat6.x = (((-u_xlat22) * u_xlat6.x) + 1);
              u_xlat22 = ((u_xlat6.x * 5.25) + (-6.80000019));
              u_xlat22 = ((u_xlat6.x * u_xlat22) + 3.82999992);
              u_xlat22 = ((u_xlat6.x * u_xlat22) + 0.458999991);
              u_xlat6.x = ((u_xlat6.x * u_xlat22) + (-0.00286999997));
              u_xlat6.x = (u_xlat6.x * 1.44269502);
              u_xlat6.x = exp2(u_xlat6.x);
              u_xlat6.x = (u_xlat6.x * 0.25);
              u_xlat6.x = ((u_xlat30 * 0.25) + (-u_xlat6.x));
              u_xlat29 = (u_xlat29 * u_xlat6.x);
              u_xlat29 = ((u_xlat26 * 0.25) + u_xlat29);
              u_xlat6.xzw = (u_xlat11.xyz * (-float3(u_xlat29, u_xlat29, u_xlat29)));
              u_xlat6.xzw = (u_xlat6.xzw * float3(1.44269502, 1.44269502, 1.44269502));
              u_xlat6.xzw = exp2(u_xlat6.xzw);
              u_xlat5.xyz = ((u_xlat6.xzw * float3(u_xlat14, u_xlat14, u_xlat14)) + u_xlat5.xyz);
              u_xlat4.xyz = ((u_xlat1.xyz * float3(u_xlat25, u_xlat25, u_xlat25)) + u_xlat4.xyz);
              u_xlati_loop_1 = (u_xlati_loop_1 + 1);
          }
          u_xlat1.xyz = (u_xlat5.xyz * TOD_SunSkyColor.xyz);
          u_xlat2.xyz = (u_xlat1.xyz * TOD_kSun.xyz);
          u_xlat1.xyz = (u_xlat1.xyz * TOD_kSun.www);
          u_xlat25 = dot(TOD_LocalSunDirection.yxz, u_xlat0.xyz);
          u_xlat26 = (u_xlat25 * u_xlat25);
          u_xlat24 = (((-u_xlat0.w) * 0.75) + 1);
          u_xlat8.x = dot(u_xlat0.yxz, TOD_LocalMoonDirection.xyz);
          u_xlat8.x = max(u_xlat8.x, 0);
          u_xlat8.x = log2(u_xlat8.x);
          u_xlat8.x = (u_xlat8.x * TOD_MoonHaloPower);
          u_xlat8.x = exp2(u_xlat8.x);
          u_xlat3.xyz = (u_xlat8.xxx * TOD_MoonHaloColor.xyz);
          u_xlat8.xyz = ((TOD_MoonSkyColor.xyz * float3(u_xlat24, u_xlat24, u_xlat24)) + u_xlat3.xyz);
          u_xlat26 = ((u_xlat26 * 0.75) + 0.75);
          u_xlat8.xyz = ((float3(u_xlat26, u_xlat26, u_xlat26) * u_xlat2.xyz) + u_xlat8.xyz);
          u_xlat2.x = ((u_xlat25 * u_xlat25) + 1);
          u_xlat2.x = (u_xlat2.x * TOD_kBetaMie.x);
          u_xlat25 = ((TOD_kBetaMie.z * u_xlat25) + TOD_kBetaMie.y);
          u_xlat25 = log2(u_xlat25);
          u_xlat25 = (u_xlat25 * 1.5);
          u_xlat25 = exp2(u_xlat25);
          u_xlat25 = (u_xlat2.x / u_xlat25);
          u_xlat8.xyz = ((float3(u_xlat25, u_xlat25, u_xlat25) * u_xlat1.xyz) + u_xlat8.xyz);
          u_xlat0.x = (-u_xlat0.x);
          u_xlat0.x = clamp(u_xlat0.x, 0, 1);
          u_xlat1.xyz = ((-u_xlat8.xyz) + TOD_GroundColor.xyz);
          u_xlat0.xyz = ((u_xlat0.xxx * u_xlat1.xyz) + u_xlat8.xyz);
          u_xlat1.xyz = ((-u_xlat0.xyz) + TOD_FogColor.xyz);
          u_xlat0.xyz = ((float3(float3(TOD_Fogginess, TOD_Fogginess, TOD_Fogginess)) * u_xlat1.xyz) + u_xlat0.xyz);
          u_xlat0.xyz = (u_xlat0.xyz * float3(TOD_Brightness, TOD_Brightness, TOD_Brightness));
          u_xlat0.xyz = log2(u_xlat0.xyz);
          u_xlat0.xyz = (u_xlat0.xyz * float3(float3(TOD_Contrast, TOD_Contrast, TOD_Contrast)));
          u_xlat0.xyz = exp2(u_xlat0.xyz);
          u_xlat0.w = 1;
          u_xlat0 = (u_xlat0 * (-float4(TOD_Brightness, TOD_Brightness, TOD_Brightness, TOD_Brightness)));
          u_xlat0 = exp2(u_xlat0);
          u_xlat0 = ((-u_xlat0) + float4(1, 1, 1, 1));
          out_v.texcoord1 = sqrt(u_xlat0);
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          out_f.color.xyz = in_f.texcoord1.xyz;
          out_f.color.w = 0;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
