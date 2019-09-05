//------------------------------------------------------------------
//トゥーンシェーダ
//-----------------------------------------------------------------
//MEMO 他の人が調節出来るように調整をする必要あり
//-----------------------------------------------------------------4

Shader "Toon"
{
	Properties
	{
		_BaseMap ("テクスチャ",2D) = "white"{}

		[NoScaleOffset]_2st_ShadeMap ("明るい色の影",2D) = "black"{}
		[NoScaleOffset]_1st_ShadeMap ("暗い色の影",2D) = "black"{}

		//__2st_ShadowColor("明るい影の色に上塗り",Color) = (255,255,255,255)
		//__1st_ShadowColor("暗い影の色に上塗り",Color) = (255,255,255,255)

		_Tweak_SystemShadowsLevel ("明るいところの割合", Range (-0.5, 0.5)) = 0
		_BaseColor_Step ("明るい影の割合", Range (0.0,1.0)) = 0.5
		_ShadeColor_Step ("暗い影の割合",Range (0.0,1.0)) = 0.5

		_BaseShade_Feather ("明るい影のなめらかさ",Range (0.0001,1.0)) = 0.1
		_1st2nd_Shades_Feather ("暗い影の割合なめらかさ",Range (0.0001,1.0)) = 0.1

		//[Toggle]_Is_LightColor_Base ("光の色の影響を受けるか" , float) = 0.0
		//[Toggle]_Is_BlendBaseColor ("アウトラインとテクスチャの色とブレンドする", float) = 0.0
		//_BaseColor ("テクスチャの色にブレンドする色",Color) = (0,0,0,0)

		_Color ("アウトラインの色",Color) = (0,0,0,0)
		/*_OutLineColor ("0:Brack 1:Blue 2:Red" , Range (0,2)) = 1*/
		__ModelOutLine_Width ("アウトラインの幅", Range (0.0, 300.0)) = 0.1

		_Offset_Z ("Offset_Z" , float) = 2
		_Farthest_Distance ("Farthest_Distance",float) = 0.5
		_Nearest_Distance ("Nearest_Distance",float) = 0.5

		//__ModelOutLine_Sampler ("_ModelOutLine_Sampler",2D) = "brack"{}
		//_BaseMap("BaseMap",2D) = "White"{}
		//_LightColor0 ("LightColor0",Color) = (0,0,0,0)

		[MaterialToggle]_Is_NoramMapToBase ("IsNormalMap",Float) = 0

		[NoScaleOffset]_NormalMap ("NormalMap", 2D) = "white"{}
	}
		SubShader
	{
		Tags {
			"RenderType" = "Opaque"
			"LightMode" = "ForwardBase"
		}
		LOD 100

		//モデルに影を描写する処理 by hosino ------------------------------------------------
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

		float4 _MainTex_ST;
		sampler2D _BaseMap;
		sampler2D _NormalMap;
		sampler2D _1st_ShadeMap;
		sampler2D _2st_ShadeMap;
		fixed4 _BaseMap_ST;
		fixed4 _NormalMap_ST;
		fixed4 _1st_ShadeMap_ST;
		fixed4 _2st_ShadeMap_ST;
		float _Tweak_SystemShadowsLevel;
		float _BaseColor_Step;
		float _BaseShade_Feather;
		float _ShadeColor_Step;
		float _1st2nd_Shades_Feather;
		float _Is_NormalMapToBase;
		fixed4 _LightColor0;
		float4 __2st_ShadowColor;
		float4 __1st_ShadowColor;


		float3 lerp3 (float3 x, float3 y, float s)
		{
			float3 re;
			re.x = x.x + s * (y.x - x.x);
			re.y = x.y + s * (y.y - x.y);
			re.z = x.z + s * (y.z - x.z);
			return re;
		}
			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float4 tangent : TANGENT;
				float2 texcoord0: TEXCOORD0;
			};

			struct VertexOutput
			{
				float4 pos : SV_POSITION;
				float2 uv0 : TEXCOORD0;
				float4 posWorld : TEXCOORD1;
				float3 normalDir : TEXCOORD2;
				float3 tangentDir : TEXCOORD3;
				float3 bitangentDir : TEXCOORD4;
				LIGHTING_COORDS (5,6)				// ライティングに必要
				UNITY_FOG_COORDS (7)
			};

			VertexOutput vert (VertexInput v)
			{
				VertexOutput o = (VertexOutput)0;

				o.uv0 = v.texcoord0;
				o.normalDir = UnityObjectToWorldNormal (v.normal);
				o.tangentDir = normalize (
					mul (unity_ObjectToWorld,
						float4(v.tangent.xyz, 0.0)).xyz
				);
				o.bitangentDir = normalize (
					cross (o.normalDir, o.tangentDir) * v.tangent.w);
				o.pos = UnityObjectToClipPos (v.vertex);
				UNITY_TRANSFER_FOG (o, o.pos);
				TRANSFER_VERTEX_TO_FRAGMENT (o);	// LIGTHIN_COORDSに値をセットするのに必要
				return o;
			}

			// MEMO : セマンティクスVFACE　＝＞　ポリゴンがカメラに対して表を向いていれば0以上が入る。
			float4 frag (VertexOutput i, float facing : VFACE) : COLOR
			{
				float faceSign = (facing >= 0 ? 1 : -1);
				i.normalDir = normalize (i.normalDir);
				i.normalDir *= faceSign;
				float3x3 tangentTransform = float3x3(i.tangentDir, i.bitangentDir, i.normalDir);		// 行列変換。接空間座標からワールド空間座標に変換する
				float3 viewDirection = normalize (_WorldSpaceCameraPos.xyz - i.normalDir.xyz);

				// Memo : UnpackNormal()は法線マップのハードウェア圧縮有無を吸収する組み込み関数
				float3 _NormalMap_var = UnpackNormal (
					tex2D (_NormalMap, TRANSFORM_TEX (i.uv0, _NormalMap))
				);
				// Memo : 接空間座標をワールド座標に変換
				float3 normalDirection =
					normalize (
						mul (_NormalMap_var.rgb, tangentTransform)
					);	// Perturbed normals

				float3 lightDirection = normalize (_WorldSpaceLightPos0.xyz);
				float3 lightColor = _LightColor0.rgb;
				float3 halfDirection = normalize (viewDirection + lightDirection);
				float attenuation = LIGHT_ATTENUATION (i);		// ライトの減衰率 0～1

				// ベースカラー
				float4 _BaseMap_var = tex2D (_BaseMap, TRANSFORM_TEX (i.uv0, _BaseMap));
				float3 _Set_LightColor = _LightColor0.rgb;
				float3 _BaseColor = _BaseMap_var.rgb * _Set_LightColor;
				// 1影
				float4 _1st_ShadeMap_var = tex2D (_1st_ShadeMap, TRANSFORM_TEX (i.uv0, _1st_ShadeMap));
				float3 _1st_ShadeColor = _1st_ShadeMap_var.rbg  *  _Set_LightColor; //*__1st_ShadowColor.rgb;
				// 2影
				float4 _2st_ShadeMap_var = tex2D (_2st_ShadeMap, TRANSFORM_TEX (i.uv0, _2st_ShadeMap));
				float3 _2st_ShadeColor = _2st_ShadeMap_var.rgb * _Set_LightColor; //*__2st_ShadowColor.rgb;


				float NdotLDIr = 0.5*
					dot (
						lerp3 (
							i.normalDir,
							normalDirection,
							_Is_NormalMapToBase
						),
						lightDirection
					) + 0.5;

				/// FinalShadowSample 
				/// 最終的なカラー値に対して、1影のカラー値を反映する割合を示している。
				/// 1.0の場合はベースカラーがそのまま、0.0の場合は1影のカラー値がそのまま使用される
				float FinalShadowSample =
					saturate (
					1.0 +
						(
							NdotLDIr * saturate (attenuation * 0.5 + 0.5 + _Tweak_SystemShadowsLevel)
							- (_BaseColor_Step - _BaseShade_Feather)

							) / _BaseShade_Feather
					);

				float3 FinalColor = lerp (

					lerp3 (
						_1st_ShadeColor,
						_2st_ShadeColor,
						saturate (
						1.0 +
							(
							 NdotLDIr - _ShadeColor_Step + _1st2nd_Shades_Feather
							 ) /
							 _1st2nd_Shades_Feather
						)
					), _BaseColor,
					FinalShadowSample
				);

				return fixed4 (FinalColor,1.0);
			}
			ENDCG
		}

		//輪郭線を描写する処理 by takano ------------------------------------------------
		Pass
		{
			Name "_ModelOutLine"	//パスの名前

			Cull Front	//ポリゴンを反転させてレンダリング

			CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"
		#include "AutoLight.cginc"

		float _Is_LightColor_Base;
		float _Is_BlendBaseColor;

		float __ModelOutLine_Width;
		float _Farthest_Distance;
		float _Nearest_Distance;
		float _Offset_Z;

		sampler2D __ModelOutLine_Sampler;
		sampler2D _BaseMap;

		fixed4 __ModelOutLine_Sampler_ST;	//Sampler2D
		fixed4 _BaseMap_ST;

		float4 _BaseColor;
		float4 _LightColor0;
		float4 _Color;

		int _OutLineColor;

		//vertexシェーダにインプットする情報
		struct VertexInput
		{
			float4 vertex : POSITION;	//頂点の位置
			float3 normal : NORMAL;		//頂点の法線
			float2 texcoord0 : TEXCOORD0;	//1番目のUV座標
		};

		//vertexシェーダがアウトプットする情報
		struct VertexOutput
		{
			float4 pos : SV_POSITION;
			float2 uv0 : TEXCOORD0;
		};

		//vertex----------------------------------------------------------------------------------------
		VertexOutput vert (VertexInput v)
		{
			VertexOutput o = (VertexOutput)0;
			o.uv0 = v.texcoord0;

			//モデル空間の原点を、ワールド空間へ変換した値を取得 
			//(unity_ObjectToWorld : モデル行列)
			float4 objPos = mul (unity_ObjectToWorld, float4(0, 0, 0, 1));

			//アウトラインの幅
			//(tex2Dlod : UV座標からテクスチャ上のピクセルの色を計算して返す 第一引数:テクスチャ 第二引数:uv座標)
			//(TRANSFORM_TEX : OffsetとTilingを含めたuv座標を返す)
			float4 __ModelOutLine_Sampler_var = tex2Dlod (__ModelOutLine_Sampler, float4(TRANSFORM_TEX (o.uv0, __ModelOutLine_Sampler), 0.0, 0));

			//アウトラインに強弱をつける
			//(smoothstep : 二つの値を重みに基づいて補間する 第一引数,第二引数:最小値と最大値 第三引数:重み)
			//(distance : 二次元の二点（x,y）の間の距離を返す)
			float Set__ModelOutLine_Width = (__ModelOutLine_Width * 0.001 * smoothstep (_Farthest_Distance, _Nearest_Distance,
				distance (objPos.rgb, _WorldSpaceCameraPos))).r;

			//頂点の座標変換
			//(頂点の位置を法線の方向に、Set_ModelOutLine_Widthのぶん押し出す)
			o.pos = UnityObjectToClipPos (float4(v.vertex.xyz + v.normal * Set__ModelOutLine_Width, 1));

			//ワールド空間でのカメラ方向の取得
			float3 viewDirection = normalize (_WorldSpaceCameraPos.xyz - o.pos.xyz);

			//プロジェクション空間でのカメラ方向の取得
			float4 viewDirectoinVP = mul (UNITY_MATRIX_VP, float4(viewDirection.xyz, 1));

			//頂点座標の算出
			o.pos.z = o.pos.z + _Offset_Z * -0.1 * viewDirectoinVP.z;

			return o;
		}

		//frag--------------------------------------------------------------------------------------------
		float4 frag (VertexOutput i, float facting : VFACE) : SV_Target
		{
			//_BaceMapからカラーの値を抽出
			//float4 _BaseMap_var = tex2D (_BaseMap,TRANSFORM_TEX (i.uv0 , _BaseMap));

			//_BaceColorからテクスチャのカラー値を乗算してベースの色を決定
			//_IsLightColor_Baceがオンならライトカラーを反映させる
			//float3 Set_BaseColor = lerp (
			//	_BaseColor.rgb * _BaseMap_var.rgb,
			//	  _BaseColor.rgb * _BaseMap_var.rgb * _LightColor0.rgb,
			//	_Is_LightColor_Base);

			//輪郭線のカラー値を決定
			float4 _SetColor = _Color;

			return _SetColor;
		}
		ENDCG
		}

	}
		FallBack "Diffuse"
}
