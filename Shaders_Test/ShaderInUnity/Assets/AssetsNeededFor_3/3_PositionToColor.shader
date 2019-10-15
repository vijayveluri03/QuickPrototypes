// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Tutorial/3"
{
	//UNITY_SHADER_NO_UPGRADE
	Properties
	{
		
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
				float4 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				//float4 localVertexPosition : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _tintColor;


			v2f vert (appdata v)
			{
				v2f o;
				 o.vertex =  mul(UNITY_MATRIX_MVP, 
              	 			 v.vertex);
				 o.uv = v.vertex  + float4(0.5, 0.5, 0.5, 0.0);	// instead of UV, we are passing vertex position directly. 
               // Here the vertex shader writes output data
               // to the output structure. We add 0.5 to the 
               // x, y, and z coordinates, because the 
               // coordinates of the cube are between -0.5 and
               // 0.5 but we need them between 0.0 and 1.0. 
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = i.uv;
				return col;
			}
			ENDCG
		}
	}
}
