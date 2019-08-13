Shader "Unlit/DisneyDiffuse"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

			sampler2D _MainTex;
			float4 _MainTex_ST;

			//Parameter
			fixed4 baceColor;
			float subsurface;
			float metallic;
			float specular;
			float specularTint;
			float roughness;
			float anisotropic;
			float sheen;
			float sheenTint;
			float clearcoat;
			float clearcoatGloss;

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

			 //フレネル反射率
			 //v:視線ベクトル
			 //b:法線ベクトル
			 //f:反射率
			float Schlick90(float3 v , float3 n , float f)
			{
				return 1 + (f - 1) * ((1 - f * n)* 5);
			}		

			//フレネル反射率
			//v:視線ベクトル
			//h:ハーフベクトル
			//f:反射率
			float Schlick0(float3 v, float3 h , float f)
			{
				return f + (1 - f) * ((1 - v * h) * 5);
			}

			//D90
			float D90(float doyLH)
			{
				return 0.5 + 2.0 * dotLH * dotLH * roughness;
			}

			//FSS90
			float  SS90(float dotLH)
			{
				return dotLH * dotLH * roughness;
			}

			//Sheen
			float Sheen()
			{
				return lerp(1, tint, sheenTint);
			}

			//Specular
			float Specular()
			{
				return lerp(1, tint, specularTint);
			}

			//tint
			float tint()
			{
				float3 hsv = RGBToHSV(baceColor);
				return baceColor / hsv.y;
			}

			//S0
			float S0()
			{
				return lerp(0.08  * specular * Specular() , baceColor , metallic)
			}

			//specularX
			float SpecularX()
			{
				return max(0.001 , pow(roughness , 2.0) / aspect)
			}

			//specularY
			float SpecularY()
			{
				return max(0.001 , pow(roughness , 2.0) * aspect)
			}

			//aspect
			float Aspect()
			{
				return Mathf.Sign(1 - 0.9 * anisotropic);
			}

			//clearcoat
			float Clearcoat()
			{
				return lerp(0.1, 0.001, clearcoatGloss);
			}

			//

			//Diffuse
			float Diffuse()

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
