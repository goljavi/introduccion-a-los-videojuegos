Shader "AltosShadersPapa/8 - Color Aberration"
{
	Properties 
	{
		_MainTex("Main texture", 2D) = "white" {}
		_NoiseAmmount("Noise Ammount", range(0, 0.1)) = 0
	}
	SubShader 
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float2 _MainTex_TexelSize;
			float _NoiseAmmount;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv_MainTex : TEXCOORD0;
				float2 uv : TEXCOORD1;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv_MainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv = v.texcoord.xy;
				return o;
			}

			float4 frag(v2f i) : COLOR
			{
				
				float2 pos = i.uv.xy;
				float4 r = tex2D(_MainTex, pos.xy - _NoiseAmmount);
				float4 g = tex2D(_MainTex, pos.xy + _NoiseAmmount);
				float4 b = tex2D(_MainTex, pos.xy + _NoiseAmmount * 0.5);

				float4 c = float4(r.r, g.g, b.b, 1);
				return c;
			}
			ENDCG
		}
	}
}