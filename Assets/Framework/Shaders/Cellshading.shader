Shader "Sarasa/CellShading" {
	Properties {
		_Texture("Texture", 2D) = "white"{}
		_InfRange ("Inflate", range(0, 20)) 		= 0
		_BorderColor ("Border Color", Color) 	= (0,0,0,0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }

		Pass
		{
			Cull Front 
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float _InfRange;
			float4 _BorderColor;

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;	
				float2 uv_Normal : TEXCOORD0;	
			};
			
			struct VertexOutput
			{
				float4 vertex : SV_POSITION;
			};

			VertexOutput vert(VertexInput vp)
			{
				VertexOutput o;
				float4 inflatePos = vp.vertex;
				inflatePos.xyz += vp.normal * _InfRange * 0.05f;
				o.vertex = mul(UNITY_MATRIX_MVP, inflatePos);
				return o;
			}

			float4 frag(VertexOutput vp):COLOR
			{
				return _BorderColor;
			}

			ENDCG
		}

		/*Pass
		{	
			CGPROGRAM
				
				#pragma vertex vert
				#pragma fragment frag
				#include "UnityCG.cginc"
				
				sampler2D _Texture;
				float4 _Texture_ST;

				struct v2f
				{
					float4 pos : SV_POSITION;
					float2 uv_Texture : TEXCOORD0;
				};
				
				v2f vert(appdata_base v)
				{
					v2f o;
					o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
					o.uv_Texture = TRANSFORM_TEX(v.texcoord, _Texture);
					return o;
				}
				
				float4 frag(v2f IN) : COLOR
				{
					float4 result = tex2D(_Texture, IN.uv_Texture);
					return result;
				}
				
			ENDCG
		}*/
	} 
	FallBack "Diffuse"
}

