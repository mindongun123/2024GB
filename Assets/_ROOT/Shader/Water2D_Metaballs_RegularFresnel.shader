Shader "Water2D/Metaballs_RegularFresnel" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_BackgroundTex ("BackgroundTexture", 2D) = "white" {}
		_ComparisonThreshold ("Threshold Comparative", Range(0.5, 1E-05)) = 0.001
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
	Fallback "UnLit"
}