#ifndef POINTLIGHT_INCLUDED
#define POINTLIGHT_INCLUDED

#include "UnityCG.cginc"
#include "Lighting.cginc"

#pragma vertex   vert
#pragma fragment frag

sampler2D _MainTex;
fixed4  _MainTex_ST;
float  _PointLightRange;

struct VertexInput
{
	float4 vertex  : POSITION;
	float2 uv	   : TEXCOORD0;
	float3 vertexw : TEXCOORD1;
	float3 normal  : NORMAL;
};

struct VertexOutput
{
	float4 vertex  : SV_POSITION;
	float2 uv      : TEXCOORD0;
	float3 vertexW : TEXCOORD1;
	float3 normal  : TEXCOORD2;
};

float4 _MainColor;

VertexOutput vert(VertexInput v)
{
	VertexOutput o;

	o.vertex = UnityObjectToClipPos(v.vertex);
	o.vertexW = mul(unity_ObjectToWorld, v.vertex);
	o.normal = UnityObjectToWorldNormal(v.normal);
	o.uv = v.uv;

	return o;
}

fixed4 frag(VertexOutput i) : SV_Target
{
	// return fixed4(_WorldSpaceLightPos0.xyz, 1);

	//êFÇéÊìæ
	float4 _MainTex_Color = tex2D(_MainTex, TRANSFORM_TEX(i.uv , _MainTex));

	//ñ@ê¸ÇÃê≥ãKâª
	float3 normal = normalize(i.normal);
	
	
	float3 light = _WorldSpaceLightPos0.w == 0 ?
					_WorldSpaceLightPos0.xyz :
					_WorldSpaceLightPos0.xyz - i.vertexW;

	float range = _PointLightRange;
	float llength = length(light);
		  light = normalize(light);

	float attenuation = saturate(lerp(1, 0, llength / range));

		  attenuation = 1 / (1 + llength * llength);

		  attenuation = saturate(lerp(1, 0, llength / range));
		  attenuation = attenuation * attenuation;

	float  diffuse = saturate(dot(normal, light));
	float3 ambient = ShadeSH9(half4(normal, 1));

	fixed4 color = diffuse * _MainTex_Color * _LightColor0 * attenuation;
		   color.rgb += ambient * _MainTex_Color;



	return color;
}

#endif