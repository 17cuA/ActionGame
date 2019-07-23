Shader "Unlit/PointLight"
{
	Properties
	{
		_MainTex("MainTex",2D) = "black"{}
		_MainColor("Main Color", Color) = (1, 1, 1, 1)
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
			#include "cginc/PointLight.cginc"

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
			#include "cginc/PointLight.cginc"

			ENDCG
		}
	}
}