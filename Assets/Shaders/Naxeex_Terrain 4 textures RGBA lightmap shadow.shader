Shader "Naxeex/Terrain 4 textures RGBA lightmap shadow"
{
  Properties
  {
    [NoScaleOffset] _MainTex0 ("Color 1 - R", 2D) = "black" {}
    [NoScaleOffset] _MainTex1 ("Color 2 - G", 2D) = "black" {}
    [NoScaleOffset] _MainTex2 ("Color 3 - B", 2D) = "black" {}
    [NoScaleOffset] _MainTex3 ("Color 4 - A", 2D) = "black" {}
    _Tiling ("Tiling", Vector) = (100,100,100,100)
    [NoScaleOffset] _Control ("Control 1 (RGBA)", 2D) = "white" {}
    [NoScaleOffset] _Light ("LightMap", 2D) = "white" {}
    [Toggle(ENABLE_LM)] _EnableLM ("Enable LM?", float) = 0
    _DinamicShadowMin ("Hide Dinamic shadow min", Range(0, 1)) = 0.4
  }
  SubShader
  {
    Tags
    { 
      "RenderType" = "Opaque"
    }
    LOD 800
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "LIGHTMODE" = "FORWARDBASE"
        "RenderType" = "Opaque"
      }
      LOD 800
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _Tiling;
      uniform sampler2D _Control;
      uniform sampler2D _MainTex0;
      uniform sampler2D _MainTex1;
      uniform sampler2D _MainTex2;
      uniform sampler2D _MainTex3;
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
          out_v.texcoord.xy = in_v.texcoord.xy;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float3 u_xlat10_0;
      float3 u_xlat1_d;
      float3 u_xlat10_1;
      float4 u_xlat2;
      float3 u_xlat10_2;
      float3 u_xlat10_3;
      float3 u_xlat10_4;
      float u_xlat16_5;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d = (in_f.texcoord.xyxy * _Tiling.zzww);
          u_xlat10_1.xyz = tex2D(_MainTex2, u_xlat0_d.xy).xyz;
          u_xlat10_0.xyz = tex2D(_MainTex3, u_xlat0_d.zw).xyz;
          u_xlat2 = (in_f.texcoord.xyxy * _Tiling.xxyy);
          u_xlat10_3.xyz = tex2D(_MainTex0, u_xlat2.xy).xyz;
          u_xlat10_2.xyz = tex2D(_MainTex1, u_xlat2.zw).xyz;
          u_xlat10_4.xyz = tex2D(_Control, in_f.texcoord.xy).xyz;
          u_xlat2.xyz = (u_xlat10_2.xyz * u_xlat10_4.yyy);
          u_xlat2.xyz = ((u_xlat10_3.xyz * u_xlat10_4.xxx) + u_xlat2.xyz);
          u_xlat1_d.xyz = ((u_xlat10_1.xyz * u_xlat10_4.zzz) + u_xlat2.xyz);
          u_xlat16_5 = ((-u_xlat10_4.x) + 1);
          u_xlat16_5 = ((-u_xlat10_4.y) + u_xlat16_5);
          u_xlat16_5 = ((-u_xlat10_4.z) + u_xlat16_5);
          u_xlat0_d.xyz = ((u_xlat10_0.xyz * float3(u_xlat16_5, u_xlat16_5, u_xlat16_5)) + u_xlat1_d.xyz);
          out_f.color.xyz = (u_xlat0_d.xyz * _LightColor0.www);
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
      "RenderType" = "Opaque"
    }
    LOD 700
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "LIGHTMODE" = "FORWARDBASE"
        "RenderType" = "Opaque"
      }
      LOD 700
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _LightColor0;
      uniform float4 _Tiling;
      uniform sampler2D _Control;
      uniform sampler2D _MainTex0;
      uniform sampler2D _MainTex1;
      uniform sampler2D _MainTex2;
      uniform sampler2D _MainTex3;
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
          out_v.texcoord.xy = in_v.texcoord.xy;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float3 u_xlat10_0;
      float3 u_xlat1_d;
      float3 u_xlat10_1;
      float4 u_xlat2;
      float3 u_xlat10_2;
      float3 u_xlat10_3;
      float3 u_xlat10_4;
      float u_xlat16_5;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d = (in_f.texcoord.xyxy * _Tiling.zzww);
          u_xlat10_1.xyz = tex2D(_MainTex2, u_xlat0_d.xy).xyz;
          u_xlat10_0.xyz = tex2D(_MainTex3, u_xlat0_d.zw).xyz;
          u_xlat2 = (in_f.texcoord.xyxy * _Tiling.xxyy);
          u_xlat10_3.xyz = tex2D(_MainTex0, u_xlat2.xy).xyz;
          u_xlat10_2.xyz = tex2D(_MainTex1, u_xlat2.zw).xyz;
          u_xlat10_4.xyz = tex2D(_Control, in_f.texcoord.xy).xyz;
          u_xlat2.xyz = (u_xlat10_2.xyz * u_xlat10_4.yyy);
          u_xlat2.xyz = ((u_xlat10_3.xyz * u_xlat10_4.xxx) + u_xlat2.xyz);
          u_xlat1_d.xyz = ((u_xlat10_1.xyz * u_xlat10_4.zzz) + u_xlat2.xyz);
          u_xlat16_5 = ((-u_xlat10_4.x) + 1);
          u_xlat16_5 = ((-u_xlat10_4.y) + u_xlat16_5);
          u_xlat16_5 = ((-u_xlat10_4.z) + u_xlat16_5);
          u_xlat0_d.xyz = ((u_xlat10_0.xyz * float3(u_xlat16_5, u_xlat16_5, u_xlat16_5)) + u_xlat1_d.xyz);
          out_f.color.xyz = (u_xlat0_d.xyz * _LightColor0.www);
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
      "RenderType" = "Opaque"
    }
    LOD 600
    Pass // ind: 1, name: FORWARD
    {
      Name "FORWARD"
      Tags
      { 
        "LIGHTMODE" = "FORWARDBASE"
        "RenderType" = "Opaque"
      }
      LOD 600
      // m_ProgramMask = 6
      CGPROGRAM
      //#pragma target 4.0
      
      #pragma vertex vert
      #pragma fragment frag
      
      #include "UnityCG.cginc"
      
      
      #define CODE_BLOCK_VERTEX
      //uniform float4x4 unity_ObjectToWorld;
      //uniform float4x4 unity_MatrixVP;
      uniform float4 _Tiling;
      uniform sampler2D _Control;
      uniform sampler2D _MainTex0;
      uniform sampler2D _MainTex1;
      uniform sampler2D _MainTex2;
      uniform sampler2D _MainTex3;
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
          out_v.texcoord.xy = in_v.texcoord.xy;
          return out_v;
      }
      
      #define CODE_BLOCK_FRAGMENT
      float4 u_xlat0_d;
      float3 u_xlat10_0;
      float3 u_xlat1_d;
      float3 u_xlat10_1;
      float4 u_xlat2;
      float3 u_xlat10_2;
      float3 u_xlat10_3;
      float3 u_xlat10_4;
      float u_xlat16_5;
      OUT_Data_Frag frag(v2f in_f)
      {
          OUT_Data_Frag out_f;
          u_xlat0_d = (in_f.texcoord.xyxy * _Tiling.zzww);
          u_xlat10_1.xyz = tex2D(_MainTex2, u_xlat0_d.xy).xyz;
          u_xlat10_0.xyz = tex2D(_MainTex3, u_xlat0_d.zw).xyz;
          u_xlat2 = (in_f.texcoord.xyxy * _Tiling.xxyy);
          u_xlat10_3.xyz = tex2D(_MainTex0, u_xlat2.xy).xyz;
          u_xlat10_2.xyz = tex2D(_MainTex1, u_xlat2.zw).xyz;
          u_xlat10_4.xyz = tex2D(_Control, in_f.texcoord.xy).xyz;
          u_xlat2.xyz = (u_xlat10_2.xyz * u_xlat10_4.yyy);
          u_xlat2.xyz = ((u_xlat10_3.xyz * u_xlat10_4.xxx) + u_xlat2.xyz);
          u_xlat1_d.xyz = ((u_xlat10_1.xyz * u_xlat10_4.zzz) + u_xlat2.xyz);
          u_xlat16_5 = ((-u_xlat10_4.x) + 1);
          u_xlat16_5 = ((-u_xlat10_4.y) + u_xlat16_5);
          u_xlat16_5 = ((-u_xlat10_4.z) + u_xlat16_5);
          u_xlat0_d.xyz = ((u_xlat10_0.xyz * float3(u_xlat16_5, u_xlat16_5, u_xlat16_5)) + u_xlat1_d.xyz);
          out_f.color.xyz = u_xlat0_d.xyz;
          out_f.color.w = 1;
          return out_f;
      }
      
      
      ENDCG
      
    } // end phase
  }
  FallBack "Mobile/VertexLit"
}
