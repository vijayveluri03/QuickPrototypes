// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Tutorial/4"
{
	Properties
	{
		_maxDistance ("Max Distance", Range (0.02,1000)) = 100

	}
	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue"="Geometry" }
		LOD 100
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 globalVertexPosition : TEXCOORD0;
			};

			sampler2D _MainTex;
			float4 _tintColor;
			float _maxDistance;


			v2f vert (appdata v)
			{
				v2f o;
				 o.vertex =  UnityObjectToClipPos(v.vertex);
				 //o.globalVertexPosition = UnityObjectToClipPos(v.vertex);
				o.globalVertexPosition = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float dist = abs(( float4(0.0f, 0.0f, 0.0f, 0.0f), i.globalVertexPosition ) );
				float ratio = dist / _maxDistance;
				//ratio = ratio > 1 ? 1 : ratio < 0 ? 0 : ratio;
				return float4 ( ratio, ratio, ratio, 1.0f);
			}
			ENDCG
		}
	}
}
