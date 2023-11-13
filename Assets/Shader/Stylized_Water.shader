Shader "Stylized/Water" {
	Properties {
		[Header(Densities)] _DepthDensity ("Depth", Range(0, 1)) = 0.5
		_DistanceDensity ("Distance", Range(0, 1)) = 0.1
		_Transparent ("Transparent", Range(0, 1)) = 0.1
		_ColorStreng ("ColorStreng", Float) = 10
		[Header(Waves)] [NoScaleOffset] _WaveNormalMap ("Normal Map", 2D) = "bump" {}
		_WaveNormalScale ("Scale", Float) = 10
		_WaveNormalSpeed ("Speed", Float) = 1
		[Header(Base Color)] _DeepColor ("Deep", Vector) = (0,0.05,0.19,1)
		_FarColor ("Far", Vector) = (0.04,0.27,0.75,1)
		[Header(Foam)] _FoamContribution ("Contribution", Range(0, 1)) = 1
		[NoScaleOffset] _FoamTexture ("Texture", 2D) = "black" {}
		_FoamScale ("Scale", Float) = 1
		_FoamSpeed ("Speed", Float) = 1
		_FoamNoiseScale ("Noise Contribution", Range(0, 1)) = 0.5
		[Header(Sparkle)] [NoScaleOffset] _SparklesNormalMap ("Normal Map", 2D) = "bump" {}
		_SparkleScale ("Scale", Float) = 10
		_SparkleSpeed ("Speed", Float) = 0.75
		_SparkleColor ("Color", Vector) = (1,1,1,1)
		_SparkleExponent ("Exponent", Float) = 10000
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
}