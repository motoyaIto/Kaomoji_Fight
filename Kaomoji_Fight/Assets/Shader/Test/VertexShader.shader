Shader "Custom/Test/VertexShader" {
	Properties {
		_Text1("Textuere1", 2D) = "black"{}
		_Text2("Textuere2", 2D) = "black"{}
	}
		SubShader{
		Tags{
		"Queue" = "Geometry"
		"RenderType" = "Opaque"
	}

		Pass{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

		sampler2D _Tex1;
	sampler2D _Tex2;

	//uvを取得
	struct UV {
		float4 vertex : POSITION;
		float2 coord1 : TEXCOORD0;
		float2 coord2 : TEXCOORD1;
	};

	//実際に使うための変数
	struct OUTPUT_DATA {
		float4 vertex : SV_POSITION;
		float2 uv1    : TEXCOORD0;
		float2 uv2    : TEXCOORD1;
	};

	/// <summary>
	/// VertexShaderを作成
	/// </summary>
	OUTPUT_DATA vert(UV uv) {
		OUTPUT_DATA output_data;

		output_data.vertex = UnityObjectToClipPos(uv.vertex);//頂点座標を元に、画面での頂点位置を計算
		output_data.uv1 = uv.coord1;
		output_data.uv2 = uv.coord2;

		return output_data;
	}

	/// <summary>
	/// Fragment Shaderを作成
	/// </summary>
	fixed4 frag(OUTPUT_DATA i) : SV_Target{
		fixed4 tex1 = tex2D(_Tex1, i.uv1);
	fixed4 tex2 = tex2D(_Tex2, i.uv2);

	return tex1 + tex2;
	}
		ENDCG
	}
	}
}
