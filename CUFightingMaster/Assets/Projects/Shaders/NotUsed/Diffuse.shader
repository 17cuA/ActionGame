Shader "Unlit/Diffuse"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
	 
		_albedo("Albedo",float) = 0.0
		_roughness("Roughness", float) =0.0
    }
    SubShader
    {
        Pass
        {
			Tags {			"RenderType" = "Opaque" }
			LOD 100
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
		
			float4 _baceColor;
			float _albedo;
			float _roughness;

			//フレネル反射
			float SchlickFresnel(float u, float f0, float f90)
			{
				return f0 + (f90 - f0) * pow(1.0 - u, 5.0);
			}

			//Diffuse
			float Diffuse(float albedo, float3 N, float3 L, float3 V, float roughness)
			{
				float3 H = normalize(L + V);
				float dotLH = saturate(dot(L, H));
				float dotNL = saturate(dot(N, L));
				float dotNV = saturate(dot(N, V));
				float Fd90 = 0.5 + 2.0 * dotLH * dotLH * roughness;
				float FL = SchlickFresnel(1.0, Fd90, dotNL);
				float FV = SchlickFresnel(1.0, Fd90, dotNV);
				return (albedo * FL * FV) / 3.141;
			}

			//// フレネル反射率
			//// v:視線ベクトル
			//// b:法線ベクトル
			//// f:反射率
			//float Schlick90(float3 v , float3 n , float f)
			//{
			//	return 1 + (f - 1) * ((1 - f * n)* 5);
			//}		

			////フレネル反射率
			////v:視線ベクトル
			////h:ハーフベクトル
			////f:反射率
			//float Schlick0(float3 v, float3 h , float f)
			//{
			//	return f + (1 - f) * ((1 - v * h) * 5);
			//}

			//FD90
			//float FD90(float d)  = 0.5 + 2 

			struct VertexInput
			{
				float2 uv : TEXCOORD0;
				float4 pos : POSITION;
				float3 normal : NORMAL;
				float3 lightDir : TEXCOORD2;
				float3 viewDir  : TEXCOORD3;
				float3 halfDir	: TEXCOORD4;
				float3 reflDir  : TEXCOORD5;
			};
			struct VertexOutput
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
				float3 normal : NORMAL;
				float3 lightDir : TEXCOORD2;
				float3 viewDir  : TEXCOORD3;
				//float3 halfDir	: TEXCOORD4;
				//float3 reflDir  : TEXCOORD5;
			};

			VertexOutput vert (VertexInput v)
            {
				VertexOutput o;
				
				//座標変換
                o.pos = UnityObjectToClipPos(v.pos);
                o.uv =
                
				//ベクトル取得
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.lightDir = WorldSpaceLightDir(v.pos);
				o.viewDir = WorldSpaceViewDir(v.pos);

                return o;
            }

            fixed4 frag (VertexOutput i) : SV_Target
            {
				float4 _BaceColor = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));
               
				

				return Diffuse(_albedo, i.normal, i.lightDir, i.viewDir, 1) * _BaceColor;
            }
            ENDCG
        }

		//Pass
		//{
		//	Tags
		//	{
		//		"LightMode" = "ForwardBase"
		//	}
		//	Blend One One

		//	CGPROGRAM

		//	#pragma vertex   vert
		//	#pragma fragment frag
		//	#include "UnityCG.cginc"
		//	#include "Lighting.cginc"
		//	#include "cginc/AmbientLight.cginc"

		//	ENDCG
		//}

		//	Pass
		//{
		//	Tags
		//	{
		//		"LightMode" = "ForwardAdd"
		//	}
		//	Blend One One

		//	CGPROGRAM

		//	#pragma vertex   vert
		//	#pragma fragment frag
		//	#include "UnityCG.cginc"
		//	#include "Lighting.cginc"
		//	#include "cginc/AmbientLight.cginc"

		//	ENDCG
		//}


    }
}
