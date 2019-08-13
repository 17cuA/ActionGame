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
	//色を取得
	float4 _MainTex_Color = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));
	
	//法線の正規化
	float3 normal = normalize(i.normal);
	//一つ目の光源の位置の正規化
	float3 light = normalize(_WorldSpaceLightPos0.xyz);

	//diffuseの値を0~1にクランプする（0以下の値を0に、1以上の値を1にする）
	float  diffuse = min(max((dot(normal, light)), -_DarknessShadow), 1);
	
	//法線から環境光を計算する（ShadeSH9は、ForwardBaceのみ動作する）
	float3 ambient = ShadeSH9(half4(normal, 1));

	//色を計算
	fixed4 color = diffuse * _MainTex_Color * _LightColor0 * _Brightness;
    color.rgb += ambient * _MainTex_Color;
	return color;
}


#endif 