Shader "Custom/Water_2" {
	Properties{
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_WaveShaking("WaveShaking",float) = 0
		_TexScrollSpeed("TexScrollSpeed",float) = -3
		_WavellSpeed("WavellSpeed",int) = 100
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			#pragma surface surf Lambert vertex:vert
			#pragma target 3.0

			sampler2D _MainTex;
			half _WaveShaking;
			half _TexScrollSpeed;
			int _WavellSpeed;

			struct Input {
				float2 uv_MainTex;
			};

			void vert(inout appdata_full v, out Input o)
			{
				UNITY_INITIALIZE_OUTPUT(Input, o);
				//float amp = 0.5*sin(_Time * 100 + v.vertex.x * 100);
				//float amp = _WaveShaking *sin(_Time * 100 + v.vertex.x * 100);
				float amp = _WaveShaking * sin(_Time * 100 + v.vertex.x * _WavellSpeed);
				v.vertex.xyz = float3(v.vertex.x, v.vertex.y + amp, v.vertex.z);
				//v.normal = normalize(float3(v.normal.x+offset_, v.normal.y, v.normal.z));
			}

			void surf(Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
				o.Albedo = c.rgb;
				o.Alpha = c.a;
				fixed2 uv = IN.uv_MainTex;
				uv.x += _TexScrollSpeed * _Time;
				//uv.y += 0.2 * _Time;
				o.Albedo = tex2D(_MainTex, uv);
			}
			ENDCG
	}
		FallBack "Diffuse"
}