// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Tutorial/5"
{
	Properties
	{
		_maxCutOutDistX ("Max X to cut out", Range (-1000,1000)) = 1000
		_maxCutOutDistY ("Max Y to cut out", Range (-1000,1000)) = 1000
		_maxCutOutDistZ ("Max Z to cut out", Range (-1000,1000)) = 1000

	}
	SubShader
	{
		Cull Off
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
			float _maxCutOutDistX;
			float _maxCutOutDistY;
			float _maxCutOutDistZ;



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
				if ( i.globalVertexPosition.x > _maxCutOutDistX )
					discard;

				if ( i.globalVertexPosition.y > _maxCutOutDistY )
					discard;
					
				if ( i.globalVertexPosition.z > _maxCutOutDistZ )
					discard;

				return float4 ( 0.5, 0.5, 0.5, 1.0f);
			}
			ENDCG
		}
	}
}
