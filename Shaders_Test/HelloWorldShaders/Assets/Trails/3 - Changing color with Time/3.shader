Shader "3"
{
	Properties
	{
		_speed ("Speed", float) = 1.0
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
			vector _speed;

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
				o.vertex = UnityObjectToClipPos(v.vertex);

				//_CosTime[4];
				float cosTime = cos( _Time.y +  length( v.vertex ) );
				cosTime = ConvertToNewRange ( -1, 1, 0, 1, cosTime);
				//float cosTime = length( v.vertex )/2;
				//cosTime = ConvertToNewRange ( -1, 1, 0, 1, cosTime);

				// if( cosTime > 0.5 )
				// 	cosTime = 0.5;
				o.color = float4( cosTime, cosTime, cosTime ,1.0);

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
