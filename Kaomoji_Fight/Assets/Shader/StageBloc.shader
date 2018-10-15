//グループ名/シェーダー名(/追加してグループ分け可)
Shader "Custom/StageBloc" {

	//インスペクター名表示する
	Properties {
		//変数名("インスペクターに表示する名前", 型) = 初期値
		EdgeSize ("EdgeSize:縁取りのサイズ", Range (0, 1)) = 0.0
		_EmissionColor("EdgeColor:縁取りの色", Color) = (1, 1, 1, 1)

	}

	//上記のプロパティを利用してどのように描画数か
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200//描画levelをコントロール

		//描画処理ENEDCGまで
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		//↑物理的に標準のライティングモデルを使用し、すべてのライトタイプでシャドウを使用可能にする
		//サーフェースシェーダーを使いますよって意味
		//#pragma surface　ライティングモデル　オプション
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		//↑Shader Model 3.0ターゲットを使用して、より見栄えの良い照明を得る
		//数値を上げればテクスチャーの数や機能が変わる：デフォルト3.0
		//#pragma target シェーダーモデル
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		half EdgeSize;
		fixed4 EdgeColor;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}

	//何らかで描画shaderでの描画に失敗したときにここに記述されてるシェーダーを呼び出す
	FallBack "Diffuse"
}
