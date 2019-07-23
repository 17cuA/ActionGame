//
//色を鮮やかにするシェーダ
//
//実装済み : 


Shader "Unlit/ShadowToModel"
{
	Properties
	{
		_MainTex("MainTex",2D) = "black"{}

		_Hue("Hue", Range(0.0,1.0)) = 0
		_Sat("Saturation", Range(0.0,1.0)) = 0
		_Val("Value", Range(0.0,1.0)) = 0
		_Brightness("Brightness",Range(1.0,10.0)) = 1
		_DarknessShadow("DarknessShadow",Range(-0.1,1.0)) = 0
	}

		SubShader
		{
			Pass
			{
				Tags
			{
				"LightMode" = "ForwardBase"
			}

			CGPROGRAM
			#pragma vertex   vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "cginc/AmbientLight.cginc"

			ENDCG
		}

		Pass
		{
			Tags
			{
				"LightMode" = "ForwardAdd"
			}
			Blend One One
		
			CGPROGRAM

			#pragma vertex   vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "cginc/AmbientLight.cginc"

			ENDCG
		}

			Pass
			{
				Tags
				{
					"RenderType" = "Opaque"
					"Ligting" = "ForwardBase"
				}

				Blend One One

				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag

				#include "UnityCG.cginc"
				#include "AutoLight.cginc"

				sampler2D _MainTex;
				fixed4 _MainTex_ST;

				half _Hue;
				half _Sat;
				half _Val;

				//RGBからHSVに変換
				float3 RGBToHSV(float3 rgb)
				{
					float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
					float4 p = rgb.g < rgb.b ? float4(rgb.bg, K.wz) : float4(rgb.gb, K.xy);
					float4 q = rgb.r < p.x ? float4(p.xyw, rgb.r) : float4(rgb.r, p.yzx);

					float d = q.x - min(q.w, q.y);
					float e = 1.0e-10;
					return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
				}
				//RGBからHSVに変換
				float3 HSVToRGB(float3 hsv)
				{
					float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
					float3 p = abs(frac(hsv.xxx + K.xyz) * 6.0 - K.www);
					return hsv.z * lerp(K.xxx, saturate(p - K.xxx), hsv.y);
				}

				struct VertexInput
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				struct VertexOutput
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
				};

				VertexOutput vert(VertexInput v)
				{
					VertexOutput o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				float4 frag(VertexOutput i) : SV_Target
				{
					float3 color = tex2D(_MainTex , TRANSFORM_TEX(i.uv, _MainTex));
					float3 hsv = RGBToHSV(color);
					hsv.x += _Hue;
					hsv.y += _Sat;
					hsv.z += _Val;

					color = HSVToRGB(hsv);

					return float4(color, 1);
				}

			ENDCG
			}

	}
}
