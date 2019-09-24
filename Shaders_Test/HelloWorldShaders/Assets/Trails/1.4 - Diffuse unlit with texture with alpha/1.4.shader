Shader "1.4"
{
	Properties
	{
		_Color ( "Color", Color ) = (1, 1, 1, 1)
		_MainTex ("Texture", 2D) = "black" {}
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" } 
		LOD 100

		Pass
		{
			ZWrite Off // don't write to depth buffer 
            // in order not to occlude other objects

			Blend SrcAlpha OneMinusSrcAlpha // use alpha blending

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

			//sampler2D _MainTex;
			//float4 _MainTex_ST;
			float4 _Color; 
			sampler2D _MainTex;

			float ConvertToNewRange ( float OldMin, float OldMax, float NewMin, float NewMax, float OldValue ){
				OldValue = clamp ( OldValue, OldMin, OldMax);
				float OldRange = (OldMax - OldMin)  ;
				float NewRange = (NewMax - NewMin)  ;
				return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
			}

			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.color = _Color;

				//o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				//UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 colUV = tex2D(_MainTex, i.uv);
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				i.color.w =  ConvertToNewRange ( -1, 1, 0, 1, cos(_Time.w*10) );
				fixed4 col = colUV * i.color;
				return col;
			}
			ENDCG
		}
	}
}
