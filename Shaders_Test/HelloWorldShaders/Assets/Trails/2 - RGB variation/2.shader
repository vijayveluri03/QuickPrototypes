Shader "2"
{
	Properties
	{
		_maxSize ("Edge Distance", vector) = (1,1,1)
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
			//#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float4 color : COLOR0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				//UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 color : COLOR0;

			};
			vector _maxSize;

			float ConvertToNewRange ( float OldMin, float OldMax, float NewMin, float NewMax, float OldValue )
			{
				float OldRange = (OldMax - OldMin)  ;
				float NewRange = (NewMax - NewMin)  ;
				return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
			}


			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float3 vertexNormalized = float3 ( 
					ConvertToNewRange ( - _maxSize.x/2, _maxSize.x/2., 0., 1., v.vertex.x ),
					ConvertToNewRange ( - _maxSize.y/2., _maxSize.y/2., 0., 1., v.vertex.y ),
					ConvertToNewRange ( - _maxSize.z/2., _maxSize.z/2., 0., 1., v.vertex.z )
				);
				o.color = float4( vertexNormalized,
								1.0);

				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f o) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, o.uv);
				
				return o.color;

			}
			ENDCG
		}
	}
}
