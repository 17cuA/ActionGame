#ifndef AMBIENTLIGHT_INCLUDED
#define AMBIENTLIGHT_INCLUDED

#include "UnityCG.cginc"
#include "Lighting.cginc"

#pragma vertex   vert
#pragma fragment frag

sampler2D _MainTex;
fixed4  _MainTex_ST;
float _Brightness;
float _DarknessShadow;


struct VertexInput
{
	float4 vertex : POSITION;
	float3 normal : NORMAL;
	float2 uv : TEXCOORD0;
};

struct VertexOutput
{
	float4 vertex : SV_POSITION;
	float2 uv : TEXCOORD0;
	float3 normal : TEXCOORD1;
};

VertexOutput vert(VertexInput v)
{
	VertexOutput o;

	o.vertex = UnityObjectToClipPos(v.vertex);
	o.normal = UnityObjectToWorldNormal(v.normal);
	o.uv = v.uv;

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
	float  diffuse = min(max((dot(normal, light)), -_DarknessShadow), 1);
	
	//�@������������v�Z����iShadeSH9�́AForwardBace�̂ݓ��삷��j
	float3 ambient = ShadeSH9(half4(normal, 1));

	//�F���v�Z
	fixed4 color = diffuse * _MainTex_Color * _LightColor0 * _Brightness;
    color.rgb += ambient * _MainTex_Color;
	return color;
}


#endif 