Shader "Naxeex/Glass" {
	Properties {
		_Color ("Color", Vector) = (0,0,0,1)
		_Cube ("Reflection Cubemap", Cube) = "_Skybox" {}
		_StaticReflectionIntensity ("Static Reflection Intensity", Float) = 1
		_CubeReflectionMin ("Cube Reflection Min", Float) = 0.05
		[Toggle(FREEZE_EFFECT)] _Freeze_effect ("Use freeze effect", Float) = 0
		_FreezeTex ("Freeze Effect Texture", 2D) = "black" {}
		_FreezeAmount ("Freeze Effect Amount", Range(0, 1)) = 0
		_FreezeDiffMin ("Freeze Diffuse min", Range(0, 1)) = 0.25
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}