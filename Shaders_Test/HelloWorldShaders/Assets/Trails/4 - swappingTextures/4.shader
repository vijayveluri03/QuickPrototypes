Shader "4"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "black" {}
		_SwapTex ("Swap Texture", 2D) = "black" {}
		_MainPoint("Main point", vector ) = (0., 0., 0., 0.)
		_DistanceForSwap ( "Distance for Swap", float ) = 0.0
		_Gap ("Gap", float ) = 0.0
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
				//float4 color : COLOR0;
			};

			struct v2f
			{
				float4 vertex : SV_POSITION;
				float4 color : COLOR0;
				float2 uv : TEXCOORD0;

				float4 vertexWorldPos : COLOR1;
			};
			sampler2D _MainTex;
			sampler2D _SwapTex;
			vector 	_MainPoint;
			float 	_DistanceForSwap;
			float 	_Gap;

			float ConvertToNewRange ( float OldMin, float OldMax, float NewMin, float NewMax, float OldValue )
			{
				OldValue = clamp ( OldValue, OldMin, OldMax);
				float OldRange = (OldMax - OldMin)  ;
				float NewRange = (NewMax - NewMin)  ;
				return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
			}


			v2f vert (appdata v)
			{
				v2f o;
				o.vertexWorldPos = mul ( UNITY_MATRIX_M, v.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;


				// //_CosTime[4];
				// float cosTime = cos( _Time.y +  length( v.vertex ) );
				// cosTime = ConvertToNewRange ( -1, 1, 0, 1, cosTime);
				// //float cosTime = length( v.vertex )/2;
				// //cosTime = ConvertToNewRange ( -1, 1, 0, 1, cosTime);

				// // if( cosTime > 0.5 )
				// // 	cosTime = 0.5;
				// o.color = float4( cosTime, cosTime, cosTime ,1.0);

				// //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				// //UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f o) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, o.uv);

				float distn = distance ( _MainPoint.xyz, o.vertexWorldPos);//  abs(_MainPoint.x * o.vertex.x + _MainPoint.y * o.vertex.y + _MainPoint.z * o.vertex.z);	// distance ( _MainPoint, o.vertex );
				if ( distn <= _DistanceForSwap )
				{
					col = tex2D(_SwapTex, o.uv);
				}
				else if ( distn < ( _DistanceForSwap + _Gap) )
				{
					col = float4 ( 1., 0., 0., 1.);
				}
				else 
					col = tex2D(_MainTex, o.uv);
				
				return col;

			}
			ENDCG
		}
	}
}
