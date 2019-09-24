
Shader "1.1"
{

	Properties
	{
		_Color ( "Color", Color ) = (1, 1, 1, 1)
	}
	SubShader
	{
		
		// not ps4 :)
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#define UNITY_SHADER_NO_UPGRADE 1

			#pragma vertex vertexShader
			#pragma fragment fragmentShader

			float4 _Color; 
			struct vertexInput
			{
				float4 vertex : POSITION;
			};
			struct v2f 
			{
				float4 vertex : SV_POSITION;
			};

			v2f vertexShader ( vertexInput v )
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}

			float4 fragmentShader ( v2f o ) : SV_Target
			{
				return _Color;
			}
			ENDCG
		}
	}
}