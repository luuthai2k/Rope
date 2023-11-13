Shader "Naxeex/NaxeexBRDF" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[HideInInspector] [NoScaleOffset] _Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
		[HideInInspector] _StaticReflectionIntensity ("Static Reflection Intensity", Float) = 1
		[HideInInspector] _CubeReflectionMin ("Cube Reflection Min", Float) = 0.05
		[HideInInspector] _DiffColor ("Main Color For Replace", Vector) = (1,1,1,1)
		[HideInInspector] _DiffColor1 ("Second Color For Replace", Vector) = (1,1,1,1)
		[HideInInspector] _ColorVar0 ("Color 0", Vector) = (0,0,0,1)
		[HideInInspector] _ColorVar1 ("Color 1", Vector) = (0,0,0,1)
		[NoScaleOffset] _BRDFTex ("NdotL NdotH (RGBA)", 2D) = "white" {}
		[NoScaleOffset] _MaskTex ("MASK (R - Main Color G - Second Color, B - Gloss, A - Reflect)", 2D) = "black" {}
		[HideInInspector] _Rimlightcolor ("Rimlight color", Vector) = (0,0.8344827,1,1)
		[HideInInspector] _Rimlightscale ("Rimlight scale", Float) = 1
		[HideInInspector] _Rimlightbias ("Rimlight bias", Float) = 0
		[HideInInspector] _FreezeTex ("Freeze Effect Texture", 2D) = "black" {}
		[HideInInspector] _FreezeAmount ("Freeze Effect Amount", Range(0, 1)) = 0
		[HideInInspector] _FreezeDiffMin ("Freeze Diffuse min", Range(0, 1)) = 0.25
		[HideInInspector] _UVOffsetX ("UV Offset X", Float) = 0
		[HideInInspector] _UVOffsetY ("UV Offset Y", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Unlit/Texture"
	//CustomEditor "Naxeex.Shaders.Editor.NaxeexBRDFShaderGUI"
}