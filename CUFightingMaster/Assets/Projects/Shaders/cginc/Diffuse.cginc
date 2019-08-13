#ifndef DIFFUSE_INCLUDED
#define DIFFUSE_INCLUDED

#include "UnityCG.cginc"
#include "Lighting.cginc"

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

VertexOutput vert(VertexInput v)
{
	VertexOutput o;

	//座標変換
	o.pos = UnityObjectToClipPos(v.pos);
	o.uv = TRANSFORM_TEX(v.uv, _MainTex);

	//ベクトル取得
	o.normal = UnityObjectToWorldNormal(v.normal);
	o.lightDir = WorldSpaceLightDir(v.pos);
	o.viewDir = WorldSpaceViewDir(v.pos);

	return o;
}

fixed4 frag(VertexOutput i) : SV_Target
{
	float4 _BaceColor = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));

	Diffuse(_albedo, i.normal, i.lightDir, i.viewDir, 1);

	return Diffuse(_albedo, i.normal, i.lightDir, i.viewDir, 1) * _BaceColor;
}
#endif 