// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'


Shader "Tutorial/2_unlit_red"
{
	//UNITY_SHADER_NO_UPGRADE
	Properties
	{
		_tintColor ("Tint color", Color) = (.34, .85, .92, 1) // color
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
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				//float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _tintColor;


			v2f vert (appdata v)
			{
				v2f o;
				//o.vertex = UnityObjectToClipPos(v.vertex);
				 o.vertex =  mul(UNITY_MATRIX_MVP, 
              	 			 v.vertex);
				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				//fixed4 col = tex2D(_MainTex, i.uv);
                //clip( col.r - _CutoutThresh );   // which is same as if ( col.r < _CutoutThresh) discard;
				fixed4 col = _tintColor;
				return col;
			}
			ENDCG
		}
	}
}
