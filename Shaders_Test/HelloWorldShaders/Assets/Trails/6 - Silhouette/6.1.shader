// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "6.1"
{
Properties {

      _MainTex ("Texture", 2D) = "black" {}
      _BorderColor ("BorderColor", Color) = (1, 1, 1, 0.5) 
      _Width ("BorderWidth", Range( 0.0, 5.0 ))  = 1.1
         // user-specified RGBA color including opacity
   }
   SubShader {
      Tags { "Queue" = "Transparent" } 
         // draw after all opaque geometry has been drawn
      Pass 
      { 
         //Cull Front
         ZWrite Off // don't occlude other objects
         Blend SrcAlpha OneMinusSrcAlpha // standard alpha blending
 
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
 
         uniform float4 _BorderColor; // define shader property for shaders
         uniform float _Width; // define shader property for shaders
 
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float2 uv : TEXCOORD0;
         };
         struct vertexOutput 
         {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
            float3 normal : NORMAL;
            float3 viewDir : TEXCOORD1;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            float4x4 modelMatrix = unity_ObjectToWorld;
            float4x4 modelMatrixInverse = unity_WorldToObject; 
 
            output.normal = normalize(
               mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
            output.viewDir = normalize(_WorldSpaceCameraPos 
               - mul(modelMatrix, input.vertex).xyz);
 
            //input.vertex *= _Width;
             input.vertex.xyz += _Width * normalize(input.vertex.xyz);
				// input.vertex.x -= _Width/4;
				// input.vertex.y += _Width/4;


            //output.pos = UnityObjectToClipPos(input.vertex + normalize(input.normal) * _Width);
            output.pos = UnityObjectToClipPos(input.vertex );
            output.uv = input.uv;

            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float3 normalDirection = normalize(input.normal);
            float3 viewDirection = normalize(input.viewDir);
 
            float newOpacity = min(1.0, _BorderColor.a 
               / abs(dot(viewDirection, normalDirection)));
			   
     
               return float4(_BorderColor.rgb, newOpacity);
            
         }
 
         ENDCG
      }
      Pass 
      { 
         ZWrite Off // don't occlude other objects
         //Blend SrcAlpha OneMinusSrcAlpha // standard alpha blending
 
         CGPROGRAM 
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
 
         uniform float4 _BorderColor; // define shader property for shaders
         uniform float _Width; // define shader property for shaders
         sampler2D _MainTex;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float2 uv : TEXCOORD0;
         };
         struct vertexOutput 
         {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
            output.pos = UnityObjectToClipPos(input.vertex);
            output.uv = input.uv;
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {

            fixed4 colUV = tex2D(_MainTex, input.uv);
            return colUV;
         }
 
         ENDCG
      }

    }
}