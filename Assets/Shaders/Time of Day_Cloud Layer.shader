Shader "Time of Day/Cloud Layer"
{
  Properties
  {
    _MainTex ("Density Map (RGBA)", 2D) = "white" {}
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Geometry+530"
      "RenderType" = "Background"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Geometry+530"
        "RenderType" = "Background"
      }
      ZWrite Off
      Fog
      { 
        Mode  Off
      } 
      Blend SrcAlpha OneMinusSrcAlpha
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
      uniform float3 TOD_SunCloudColor;
      uniform float3 TOD_MoonCloudColor;
      uniform float3 TOD_FogColor;
      uniform float3 TOD_GroundColor;
      uniform float3 TOD_LocalSunDirection;
      uniform float3 TOD_LocalMoonDirection;
      uniform float TOD_Contrast;
      uniform float TOD_Brightness;
      uniform float TOD_Fogginess;
      uniform float TOD_MoonHaloPower;
      uniform float3 TOD_MoonHaloColor;
      uniform float TOD_CloudOpacity;
      uniform float TOD_CloudColoring;
      uniform float TOD_CloudScattering;
      uniform float TOD_CloudBrightness;
      uniform float3 TOD_CloudOffset;
      uniform float3 TOD_CloudWind;
      uniform float3 TOD_CloudSize;
      uniform float3 TOD_kBetaMie;
      uniform float4 TOD_kSun;
      uniform float4 TOD_k4PI;
      uniform float4 TOD_kRadius;
      uniform float4 TOD_kScale;
      uniform float TOD_CloudCoverage;
      uniform float TOD_CloudDensity;
      uniform float TOD_CloudAttenuation;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
      };
      
      struct OUT_Data_Vert
      {
          float4 texcoord :TEXCOORD0;
          float4 texcoord1 :TEXCOORD1;
          float3 texcoord2 :TEXCOORD2;
          float3 texcoord3 :TEXCOORD3;
          float3 texcoord4 :TEXCOORD4;
          float4 vertex :SV_POSITION;
      };
      
      struct v2f
      {
          float4 texcoord :TEXCOORD0;
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
      float3 u_xlat11;
      float u_xlat12;
      float u_xlat14;
      float2 u_xlat17;
      float u_xlat22;
      float u_xlat24;
      float u_xlat25;
      float u_xlat26;
      float u_xlat27;
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
          u_xlat0.xyz = (u_xlat0.xxx * in_v.vertex.xyz);
          u_xlat1.x = ((u_xlat0.y * 0.899999976) + 0.100000001);
          u_xlat1.x = (float(1) / u_xlat1.x);
          u_xlat1.xy = (u_xlat0.xz * u_xlat1.xx);
          u_xlat1.xy = (u_xlat1.xy / TOD_CloudSize.xz);
          u_xlat17.xy = (u_xlat1.xy + TOD_CloudOffset.xz);
          out_v.texcoord1.xy = (u_xlat17.xy + TOD_CloudWind.xz);
          u_xlat17.x = dot(float2(0.98480773, (-0.173648179)), u_xlat1.xy);
          u_xlat17.y = dot(float2(0.173648179, 0.98480773), u_xlat1.xy);
          u_xlat1.xy = (u_xlat17.xy + TOD_CloudOffset.xz);
          out_v.texcoord1.zw = (u_xlat1.xy + TOD_CloudWind.xz);
          u_xlat0.w = max(u_xlat0.y, 0);
          u_xlat1.x = dot(u_xlat0.xzw, u_xlat0.xzw);
          u_xlat1.x = rsqrt(u_xlat1.x);
          u_xlat1.xyz = (u_xlat0.xwz * u_xlat1.xxx);
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
          u_xlat25 = ((TOD_LocalSunDirection.y * 4) + 1);
          u_xlat25 = clamp(u_xlat25, 0, 1);
          u_xlat26 = dot(u_xlat0.xyz, TOD_LocalSunDirection.xyz);
          u_xlat3.x = (u_xlat26 + 1.25);
          u_xlat3.x = clamp(u_xlat3.x, 0, 1);
          u_xlat25 = (u_xlat25 * u_xlat3.x);
          u_xlat3.xyz = (TOD_SunCloudColor.xyz + (-TOD_MoonCloudColor.xyz));
          u_xlat3.xyz = ((float3(u_xlat25, u_xlat25, u_xlat25) * u_xlat3.xyz) + TOD_MoonCloudColor.xyz);
          u_xlat4.xyz = ((-u_xlat3.xyz) + TOD_FogColor.xyz);
          u_xlat3.xyz = ((float3(float3(TOD_Fogginess, TOD_Fogginess, TOD_Fogginess)) * u_xlat4.xyz) + u_xlat3.xyz);
          u_xlat25 = (u_xlat26 * u_xlat26);
          u_xlat24 = (((-u_xlat0.w) * 0.75) + 1);
          u_xlat27 = dot(u_xlat0.xyz, TOD_LocalMoonDirection.xyz);
          u_xlat27 = max(u_xlat27, 0);
          u_xlat27 = log2(u_xlat27);
          u_xlat27 = (u_xlat27 * TOD_MoonHaloPower);
          u_xlat27 = exp2(u_xlat27);
          u_xlat25 = ((u_xlat25 * 0.75) + 0.75);
          u_xlat2.xyz = (u_xlat2.xyz * float3(u_xlat25, u_xlat25, u_xlat25));
          u_xlat25 = ((u_xlat26 * u_xlat26) + 1);
          u_xlat4.x = (u_xlat25 * TOD_kBetaMie.x);
          u_xlat12 = ((TOD_kBetaMie.z * u_xlat26) + TOD_kBetaMie.y);
          u_xlat12 = log2(u_xlat12);
          u_xlat12 = (u_xlat12 * 1.5);
          u_xlat12 = exp2(u_xlat12);
          u_xlat4.x = (u_xlat4.x / u_xlat12);
          u_xlat25 = (u_xlat25 * 0.653110027);
          u_xlat12 = (((-u_xlat26) * 0.600000024) + 1.09000003);
          u_xlat25 = (u_xlat25 / u_xlat12);
          u_xlat25 = ((u_xlat26 * 0.300000012) + u_xlat25);
          u_xlat25 = (u_xlat25 * TOD_CloudScattering);
          u_xlat25 = (u_xlat25 * u_xlat4.x);
          u_xlat1.xyz = (u_xlat1.xyz * float3(u_xlat25, u_xlat25, u_xlat25));
          u_xlat1.xyz = ((TOD_MoonHaloColor.xyz * float3(u_xlat27, u_xlat27, u_xlat27)) + u_xlat1.xyz);
          u_xlat25 = (-u_xlat0.y);
          u_xlat25 = clamp(u_xlat25, 0, 1);
          u_xlat4.xyz = ((-u_xlat1.xyz) + TOD_GroundColor.xyz);
          u_xlat1.xyz = ((float3(u_xlat25, u_xlat25, u_xlat25) * u_xlat4.xyz) + u_xlat1.xyz);
          u_xlat4.xyz = ((-u_xlat1.xyz) + TOD_FogColor.xyz);
          u_xlat1.xyz = ((float3(float3(TOD_Fogginess, TOD_Fogginess, TOD_Fogginess)) * u_xlat4.xyz) + u_xlat1.xyz);
          u_xlat1.xyz = (u_xlat1.xyz * float3(TOD_Brightness, TOD_Brightness, TOD_Brightness));
          u_xlat1.xyz = log2(u_xlat1.xyz);
          u_xlat1.xyz = (u_xlat1.xyz * float3(float3(TOD_Contrast, TOD_Contrast, TOD_Contrast)));
          u_xlat1.xyz = exp2(u_xlat1.xyz);
          u_xlat2.xyz = ((TOD_MoonSkyColor.xyz * float3(u_xlat24, u_xlat24, u_xlat24)) + u_xlat2.xyz);
          u_xlat4.xyz = ((-u_xlat2.xyz) + TOD_GroundColor.xyz);
          u_xlat2.xyz = ((float3(u_xlat25, u_xlat25, u_xlat25) * u_xlat4.xyz) + u_xlat2.xyz);
          u_xlat4.xyz = ((-u_xlat2.xyz) + TOD_FogColor.xyz);
          u_xlat2.xyz = ((float3(float3(TOD_Fogginess, TOD_Fogginess, TOD_Fogginess)) * u_xlat4.xyz) + u_xlat2.xyz);
          u_xlat2.xyz = (u_xlat2.xyz * float3(TOD_Brightness, TOD_Brightness, TOD_Brightness));
          u_xlat2.xyz = log2(u_xlat2.xyz);
          u_xlat2.xyz = (u_xlat2.xyz * float3(float3(TOD_Contrast, TOD_Contrast, TOD_Contrast)));
          u_xlat2.xyz = exp2(u_xlat2.xyz);
          u_xlat24 = (u_xlat0.y * u_xlat0.y);
          u_xlat24 = (u_xlat24 * 500);
          u_xlat24 = min(u_xlat24, 1.00001001);
          u_xlat3.xyz = ((float3(TOD_Brightness, TOD_Brightness, TOD_Brightness) * u_xlat3.xyz) + (-u_xlat2.xyz));
          u_xlat2.xyz = ((float3(float3(TOD_CloudColoring, TOD_CloudColoring, TOD_CloudColoring)) * u_xlat3.xyz) + u_xlat2.xyz);
          u_xlat2.xyz = (u_xlat2.xyz * float3(float3(TOD_CloudBrightness, TOD_CloudBrightness, TOD_CloudBrightness)));
          out_v.texcoord.w = (u_xlat24 * TOD_CloudOpacity);
          u_xlat2.xyz = (u_xlat2.xyz * (-float3(TOD_Brightness, TOD_Brightness, TOD_Brightness)));
          u_xlat2.xyz = exp2(u_xlat2.xyz);
          u_xlat2.xyz = ((-u_xlat2.xyz) + float3(1, 1, 1));
          u_xlat1.xyz = (u_xlat1.xyz * (-float3(TOD_Brightness, TOD_Brightness, TOD_Brightness)));
          u_xlat1.xyz = exp2(u_xlat1.xyz);
          u_xlat1.xyz = ((-u_xlat1.xyz) + float3(1, 1, 1));
          out_v.texcoord.xyz = sqrt(u_xlat2.xyz);
          out_v.texcoord4.xyz = sqrt(u_xlat1.xyz);
          out_v.texcoord2.xyz = u_xlat0.xyz;
          out_v.texcoord3.xyz = TOD_LocalSunDirection.xyz;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float u_xlat10_0;
      float4 u_xlat16_1;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0 = tex2D(_MainTex, in_f.texcoord1.xy).x;
          u_xlat0_d.x = (u_xlat10_0 + (-TOD_CloudCoverage));
          u_xlat16_1.w = (u_xlat0_d.x * TOD_CloudDensity);
          u_xlat16_1.w = clamp(u_xlat16_1.w, 0, 1);
          u_xlat16_1.xyz = (((-u_xlat0_d.xxx) * float3(TOD_CloudAttenuation, TOD_CloudAttenuation, TOD_CloudAttenuation)) + float3(1, 1, 1));
          u_xlat0_d = (u_xlat16_1 * in_f.texcoord);
          out_f.color = u_xlat0_d;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
