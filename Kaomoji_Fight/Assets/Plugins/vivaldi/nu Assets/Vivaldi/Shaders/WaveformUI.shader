// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/Vivaldi/WaveformUI"
{
	Properties
	{
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Tags {  "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		LOD 100

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull Off
			ZTest LEqual
			ZWrite Off

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			float4 _Color;

			struct appdata_t 
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
			};

			struct v2f 
			{
				fixed4 color : COLOR;
				float4 vertex : SV_POSITION;
			};

			
			v2f vert (appdata_t v)
			{
				v2f o;
				
				#if UNITY_VERSION >= 560 
				o.vertex = UnityObjectToClipPos(v.vertex);
				#else 	
				o.vertex = UnityObjectToClipPos(v.vertex); 	
				#endif

				o.color = v.color * _Color;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return i.color;
			}

			ENDCG
		}
	}
}
