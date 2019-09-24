// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "5"
{
	Properties
	{
		_TogglePos ("Displace Position forward", Int) = 0
		_ForwardUnits ("Displacement unit count", Float) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			//ZWrite Off // don't occlude other objects
    	    //Blend SrcAlpha OneMinusSrcAlpha // standard alpha blending

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
				float3 normal : NORMAL;
				//float4 color : COLOR0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 normal : TEXCOORD;
				float3 viewDir : TEXCOORD1;
				float4 color : COLOR0;

			};
			float ConvertToNewRange ( float OldMin, float OldMax, float NewMin, float NewMax, float OldValue ){
				OldValue = clamp ( OldValue, OldMin, OldMax);
				float OldRange = (OldMax - OldMin)  ;
				float NewRange = (NewMax - NewMin)  ;
				return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
			}

			float IgnoreRest ( float MinValue, float MaxValue, float value, float substituteWith ){
				if ( value >= MinValue && value <= MaxValue)
					return substituteWith;
				return value;
			}
			float ClearOffRest ( float MinValue, float MaxValue, float value, float substituteWith ){
				if ( value >= MinValue && value <= MaxValue)
					return value;
				return substituteWith;
			}

			uniform int _TogglePos; // define shader property for shaders
			uniform float _ForwardUnits;

			v2f vert (appdata input){
				v2f output;

				float4x4 modelMatrix = unity_ObjectToWorld;
            	float4x4 modelMatrixInverse = unity_WorldToObject; 

				output.normal = normalize( mul(modelMatrix, input.normal).xyz);
								//normalize( mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
				output.viewDir = -normalize(_WorldSpaceCameraPos 
               							- mul(modelMatrix, input.vertex).xyz);

				

				if ( _TogglePos == 1 )
				{
					output.pos = UnityObjectToClipPos(input.vertex + float4 ( normalize ( input.normal ), 0 ) * _ForwardUnits );
				}
				else
					output.pos = UnityObjectToClipPos(input.vertex);


				return output;
			}
			
			fixed4 frag (v2f output) : SV_Target {
				float direction = dot ( normalize( output.normal.xyz ), normalize ( output.viewDir.xyz ) );
				direction = ConvertToNewRange ( -1, 1, 1, 0, direction );
				direction = ClearOffRest ( 0.9, 1, direction, 0);
				output.color = float4 ( direction, 1 - direction, 1 - direction, 1. );
				return output.color;
			}

			ENDCG
		}
	}
}
