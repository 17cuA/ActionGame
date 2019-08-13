#ifndef AMBIENTLIGHTDIFFUSE_INCLUDED
#define AMBIENTLIGHTDIFFUSE_INCLUDED

#include "UnityCG.cginc"
#include "Lighting.cginc"

#pragma vertex   vert
#pragma fragment frag

sampler2D _MainTex;
fixed4  _MainTex_ST;
float _Brightness;
float _DarknessShadow;

float4 _baceColor;
float _roughness;

//�t���l������
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
	float Fd90 = 0.5+ 2.0 * dotLH * dotLH * roughness;
	float FL = SchlickFresnel(1.0, Fd90, dotNL);
	float FV = SchlickFresnel(1.0, Fd90, dotNV);
	return ( FL * FV) / 3.141;
}
struct VertexInput
{
	float4 vertex : POSITION;
	float3 normal : NORMAL;
	float2 uv : TEXCOORD0;
	float3 lightDir : TEXCOORD2;
	float3 viewDir  : TEXCOORD3;
	float3 halfDir	: TEXCOORD4;
	float3 reflDir  : TEXCOORD5;
};

struct VertexOutput
{
	float4 vertex : SV_POSITION;
	float2 uv : TEXCOORD0;
	float3 normal : TEXCOORD1;
	float3 lightDir : TEXCOORD2;
	float3 viewDir  : TEXCOORD3;
};

VertexOutput vert(VertexInput v)
{
	VertexOutput o;

	o.vertex = UnityObjectToClipPos(v.vertex);
	o.uv = TRANSFORM_TEX(v.uv, _MainTex);;

	o.normal = UnityObjectToWorldNormal(v.normal);
	o.lightDir = WorldSpaceLightDir(v.vertex);
	o.viewDir = WorldSpaceViewDir(v.vertex);

	return o;
}

fixed4 frag(VertexOutput i) : SV_Target
{
	//�F���擾
	float4 _MainTex_Color = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));
	
	//�@���̐��K��
	float3 normal = normalize(i.normal);
	//��ڂ̌����̈ʒu�̐��K��
	float3 light = normalize(_WorldSpaceLightPos0.xyz);

	//diffuse�̒l��0~1�ɃN�����v����i0�ȉ��̒l��0�ɁA1�ȏ�̒l��1�ɂ���j
	float diffuse =
	min(max((dot(normal, light)), -_DarknessShadow), 1) * Diffuse(_MainTex_Color, i.normal, i.lightDir, i.viewDir, _roughness);
	//�@������������v�Z����iShadeSH9�́AForwardBace�̂ݓ��삷��j
	float3 ambient = ShadeSH9(half4(normal, 1));

	//�F���v�Z
	fixed4 color = diffuse * _MainTex_Color * _LightColor0 * _Brightness;
    color.rgb += ambient * _MainTex_Color;
	return color;
}


#endif 