Shader "Time of Day/Cloud Billboard Near"
{
  Properties
  {
    _MainTex ("Density Map (RGBA)", 2D) = "white" {}
    _BumpMap ("Normal Map", 2D) = "bump" {}
  }
  SubShader
  {
    Tags
    { 
      "IGNOREPROJECTOR" = "true"
      "QUEUE" = "Geometry+540"
      "RenderType" = "Background"
    }
    Pass // ind: 1, name: 
    {
      Tags
      { 
        "IGNOREPROJECTOR" = "true"
        "QUEUE" = "Geometry+540"
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
      #define conv_mxt4x4_0(mat4x4) float4(mat4x4[0].x,mat4x4[1].x,mat4x4[2].x,mat4x4[3].x)
      #define conv_mxt4x4_1(mat4x4) float4(mat4x4[0].y,mat4x4[1].y,mat4x4[2].y,mat4x4[3].y)
      #define conv_mxt4x4_2(mat4x4) float4(mat4x4[0].z,mat4x4[1].z,mat4x4[2].z,mat4x4[3].z)
      #define conv_mxt4x4_3(mat4x4) float4(mat4x4[0].w,mat4x4[1].w,mat4x4[2].w,mat4x4[3].w)
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float3 TOD_SunSkyColor;
      uniform float3 TOD_MoonSkyColor;
      uniform float3 TOD_SunCloudColor;
      uniform float3 TOD_MoonCloudColor;
      uniform float3 TOD_FogColor;
      uniform float3 TOD_GroundColor;
      uniform float3 TOD_SunDirection;
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
      uniform float3 TOD_kBetaMie;
      uniform float4 TOD_kSun;
      uniform float4 TOD_k4PI;
      uniform float4 TOD_kRadius;
      uniform float4 TOD_kScale;
      uniform float4 _MainTex_ST;
      uniform float4 _BumpMap_ST;
      uniform float TOD_CloudAttenuation;
      uniform sampler2D _MainTex;
      struct appdata_t
      {
          float4 vertex :POSITION0;
          float4 tangent :TANGENT0;
          float3 normal :NORMAL0;
          float4 texcoord :TEXCOORD0;
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
      float4 u_xlat2;
      float3 u_xlat3;
      float3 u_xlat4;
      float3 u_xlat5;
      float4 u_xlat6;
      float u_xlat7;
      float3 u_xlat11;
      float u_xlat12;
      float u_xlat14;
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
          u_xlat0 = (in_v.vertex.yyyy * conv_mxt4x4_1(unity_ObjectToWorld));
          u_xlat0 = ((conv_mxt4x4_0(unity_ObjectToWorld) * in_v.vertex.xxxx) + u_xlat0);
          u_xlat0 = ((conv_mxt4x4_2(unity_ObjectToWorld) * in_v.vertex.zzzz) + u_xlat0);
          u_xlat1 = (u_xlat0 + conv_mxt4x4_3(unity_ObjectToWorld));
          out_v.vertex = mul(unity_MatrixVP, u_xlat1);
          u_xlat0.xyz = ((conv_mxt4x4_3(unity_ObjectToWorld).xyz * in_v.vertex.www) + u_xlat0.xyz);
          u_xlat0.xyz = normalize(u_xlat0.xyz);
          out_v.texcoord1.xy = TRANSFORM_TEX(in_v.texcoord.xy, _MainTex);
          out_v.texcoord1.zw = TRANSFORM_TEX(in_v.texcoord.xy, _BumpMap);
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
          u_xlat25 = ((TOD_SunDirection.y * 4) + 1);
          u_xlat25 = clamp(u_xlat25, 0, 1);
          u_xlat26 = dot(u_xlat0.xyz, TOD_SunDirection.xyz);
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
          u_xlat3.xyz = ((float3(TOD_Brightness, TOD_Brightness, TOD_Brightness) * u_xlat3.xyz) + (-u_xlat2.xyz));
          u_xlat2.xyz = ((float3(float3(TOD_CloudColoring, TOD_CloudColoring, TOD_CloudColoring)) * u_xlat3.xyz) + u_xlat2.xyz);
          u_xlat2.xyz = (u_xlat2.xyz * float3(float3(TOD_CloudBrightness, TOD_CloudBrightness, TOD_CloudBrightness)));
          u_xlat3.xyz = normalize(in_v.normal.xyz);
          u_xlat4.xyz = normalize(in_v.tangent.xyz);
          u_xlat5.xyz = (u_xlat3.xyz * u_xlat4.xyz);
          u_xlat3.xyz = ((u_xlat3.zxy * u_xlat4.yzx) + (-u_xlat5.xyz));
          u_xlat3.xyz = (u_xlat3.xyz * in_v.tangent.www);
          u_xlat4.x = dot(in_v.tangent.xyz, u_xlat0.xyz);
          u_xlat4.y = dot(u_xlat3.xyz, u_xlat0.xyz);
          u_xlat4.z = dot(in_v.normal.xyz, u_xlat0.xyz);
          u_xlat0.x = dot(u_xlat4.xyz, u_xlat4.xyz);
          u_xlat0.x = rsqrt(u_xlat0.x);
          out_v.texcoord2.xyz = (u_xlat0.xxx * u_xlat4.xyz);
          u_xlat0.x = dot(in_v.tangent.xyz, TOD_SunDirection.xyz);
          u_xlat0.y = dot(u_xlat3.xyz, TOD_SunDirection.xyz);
          u_xlat0.z = dot(in_v.normal.xyz, TOD_SunDirection.xyz);
          out_v.texcoord3.xyz = normalize(u_xlat0.xyz);
          u_xlat0.xyz = (u_xlat2.xyz * (-float3(TOD_Brightness, TOD_Brightness, TOD_Brightness)));
          u_xlat0.xyz = exp2(u_xlat0.xyz);
          u_xlat0.xyz = ((-u_xlat0.xyz) + float3(1, 1, 1));
          u_xlat1.xyz = (u_xlat1.xyz * (-float3(TOD_Brightness, TOD_Brightness, TOD_Brightness)));
          u_xlat1.xyz = exp2(u_xlat1.xyz);
          u_xlat1.xyz = ((-u_xlat1.xyz) + float3(1, 1, 1));
          out_v.texcoord.xyz = sqrt(u_xlat0.xyz);
          out_v.texcoord4.xyz = sqrt(u_xlat1.xyz);
          out_v.texcoord.w = TOD_CloudOpacity;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float2 u_xlat10_0;
      float4 u_xlat16_1;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat10_0.xy = tex2D(_MainTex, in_f.texcoord1.xy).xw;
          u_xlat16_1.w = u_xlat10_0.x;
          u_xlat16_1.w = clamp(u_xlat16_1.w, 0, 1);
          u_xlat16_1.xyz = (((-u_xlat10_0.yyy) * float3(TOD_CloudAttenuation, TOD_CloudAttenuation, TOD_CloudAttenuation)) + float3(1, 1, 1));
          u_xlat0_d = (u_xlat16_1 * in_f.texcoord);
          out_f.color = u_xlat0_d;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack Off
}
