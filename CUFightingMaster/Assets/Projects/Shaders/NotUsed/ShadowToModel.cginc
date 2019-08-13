#ifndef SAMPLE07PLIB_INCLUDED
#define SAMPLE07PLIB_INCLUDED

#include "UnityCG.cginc"
#include "Lighting.cginc"

//RGB‚©‚çHSV‚É•ÏŠ·
float3 RGBToHSV(float3 rgb)
{
	float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
	float4 p = rgb.g < rgb.b ? float4(rgb.bg, K.wz) : float4(rgb.gb, K.xy);
	float4 q = rgb.r < p.x ? float4(p.xyw, rgb.r) : float4(rgb.r, p.yzx);

	float d = q.x - min(q.w, q.y);
	float e = 1.0e-10;
	return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}
//RGB‚©‚çHSV‚É•ÏŠ·
float3 HSVToRGB(float3 hsv)
{
	float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	float3 p = abs(frac(hsv.xxx + K.xyz) * 6.0 - K.www);
	return hsv.z * lerp(K.xxx, saturate(p - K.xxx), hsv.y);
}

VertexOutput vert(VertexInput v)
{
	VertexOutput o;

	o.vertex = UnityObjectToClipPos(v.vertex);
	o.normal = UnityObjectToWorldNormal(v.normal);
	o.uv = v.uv;

	return o;
}

y

fixed4 frag(VertexOutput i) : SV_Target
{
	float4 _MainTex_Color = tex2D(_MainTex, TRANSFORM_TEX(i.uv, _MainTex));

	float3 normal = normalize(i.normal);
	float3 light = normalize(_WorldSpaceLightPos0.xyz);

	float  diffuse = saturate(dot(normal, light));
	float3 ambient = ShadeSH9(half4(normal, 1));

	fixed4 color = diffuse * _MainTex_Color * _LightColor0;
		   color.rgb += ambient * _MainTex_Color;

	return color;
}

#endif