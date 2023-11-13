Shader "Custom/LinearDiffuse" {
	Properties {
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[Toggle(USE_SPECULAR)] _UseTexture ("Use Specular", Float) = 0
		_Lighting ("Lighting", Range(0, 1)) = 0
		[PowerSlider(5.0)] _Shininess ("Shininess", Range(0.03, 1.5)) = 1
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
	Fallback "Diffuse"
}