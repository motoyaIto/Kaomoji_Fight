Shader "Custom/Test/Shader_VF_Vcolor"
{
	Properties{
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

		//頂点データ
		struct VERTEXDATA {
		float4 vertex : POSITION;
		fixed4 color : COLOR;
	};

	//実際に使うための変数
	struct OUTPUT_DATA {
		float4 vertex : SV_POSITION;
		fixed4 color : COLOR;
	};

	/// <summary>
	/// VertexShaderを作成
	/// </summary>
	OUTPUT_DATA vert(VERTEXDATA v) {
		OUTPUT_DATA output_data;

		output_data.vertex = UnityObjectToClipPos(v.vertex);//頂点座標を元に、画面での頂点位置を計算
		output_data.color = v.color;

		return output_data;
	}

	/// <summary>
	/// Fragment Shaderを作成
	/// </summary>
	fixed4 frag(OUTPUT_DATA i) : SV_Target{
		return i.color;
	}
		ENDCG
	}
	}
}
