Shader "Facundo Balboa/TeamFortress" {
	Properties {
		_MainTex("Texture", 2D) = "white"{}
		_RampTex("Ramp Texture", 2D) = "white"{}
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimPower("Rim Power", Range(0.0,6.0)) = 0.0
		_Specular ("Specular", 2D) = "white"{}
		_Normal ("Normal", 2D) = "white"{}
		_SaturateValue ("Saturate", Range(1.0, 10.0)) = 1.0
		_Gloss ("Gloss", Range(0.0, 1.0)) = 0.0
		_InfRange("Inflate", range(0, 5)) = 0
		_BorderColor("Border Color", Color) = (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf TeamFortress

		sampler2D _MainTex;
		sampler2D _RampTex;
		sampler2D _Specular;
		sampler2D _Normal;
		float4 _RimColor;
		float _RimPower;
		float _SaturateValue;
		float _Gloss;

		struct Input 
		{
			float2 uv_MainTex;
			float3 viewDir;
		};

		void surf (Input IN, inout SurfaceOutput o) 
		{
			o.Gloss = _Gloss;
			float4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Specular = tex2D(_Specular, IN.uv_MainTex);
			float rim = 1 - saturate(dot(normalize(IN.viewDir), o.Normal));
			float3 s = tex2D(_Specular,IN.uv_MainTex).rgb;
			o.Emission = _RimColor.rgb * pow(rim, _RimPower) * s.r;
			o.Alpha = o.Emission;
		}

		inline fixed4 LightingTeamFortress (SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			float4 c;
			float NdotL = dot (s.Normal, lightDir);
			float diffWrap = NdotL;
			fixed diff = max (0,diffWrap) * atten;
			float4 rampColor = tex2D(_RampTex, float2(diff,0));
			float specularValue = max (0, dot (s.Normal, lightDir));
			c.rgb = s.Albedo * rampColor.rgb * specularValue * atten * _SaturateValue;
			c.a = s.Alpha;
			return c;
		}
		
		ENDCG
	} 
	FallBack "Diffuse"
}
