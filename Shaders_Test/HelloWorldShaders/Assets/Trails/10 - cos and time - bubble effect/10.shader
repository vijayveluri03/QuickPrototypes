// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
//#define UNITY_SHADER_NO_UPGRADE 1 
Shader "10"
{
   Properties 
   {
      _Color ( "Color 1", Color) = (0, 0, 0, 0)
      _MainTex ( "Main Texture" , 2D) = "white" {}
      _NoiseTex ( "Noise Texture" , 2D) = "white" {}

      [Toggle] _ToggleVertexMovementUsingNormal("Toggle Vertex Movement Using Normal", Float) = 0
      [Toggle] _ToggleVertexMovementUsingCustomValue("Toggle Vertex Movement Using Custom Value", Float) = 0
      
      _PeekDistortionValue ( "peekDistortionValue", float ) = 1
      _DistortionRangeValue ( "DistortionRangeValue", float ) = 1

      _CustomValue ( "Custom Value ", float )  = 0
 
   }
   Subshader 
   {
      Pass 
      {
         Tags { "RenderType"="Opaque" }

         CGPROGRAM

         #include "UnityCG.cginc"

         #pragma vertex vert  
         #pragma fragment frag 

         float4 _Color;
         sampler2D _MainTex;
         float4 _MainTex_ST; //TRANSFORM_TEX using this variable. we had to declare it 
         sampler2D _NoiseTex;
         float4 _NoiseTex_ST;//TRANSFORM_TEX using this variable. we had to declare it 

         float _ToggleVertexMovementUsingNormal;
         float _ToggleVertexMovementUsingCustomValue;
         float _PeekDistortionValue;
         float _DistortionRangeValue;
         float _CustomValue;


         struct VertexInput
         {
            float4 position : POSITION;
            float2 uv : TEXCOORD0;
            float4 normal : NORMAL0;
            //float4 color : COLOR0;
            //importt normals
            
         };
         struct FragInput 
         {
            float4 position : POSITION;
            float2 uv : TEXCOORD0;
            //
         };
         float ConvertToNewRange ( float OldMin, float OldMax, float NewMin, float NewMax, float OldValue )
         {
            OldValue = clamp( OldValue, OldMin, OldMax );
            float OldRange = (OldMax - OldMin)  ;
            float NewRange = (NewMax - NewMin)  ;
            return (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;
         }

         bool IsOn ( float value ) { return value > 0;  }

         FragInput vert ( VertexInput vi )
         {
            FragInput fi;
            
            fi.uv = TRANSFORM_TEX ( vi.uv, _MainTex );   // this will use texture offsets and stuff like that 
            if ( IsOn (_ToggleVertexMovementUsingNormal ) )
            {
               fi.position = mul(UNITY_MATRIX_MVP , vi.position + normalize(vi.normal));
            }
            else if ( IsOn ( _ToggleVertexMovementUsingCustomValue ) )
            {
               fi.position = mul(unity_ObjectToWorld , vi.position);
               float vertexToCustomValueDist = abs ( fi.position.x - _CustomValue );
               vertexToCustomValueDist = ConvertToNewRange ( 0, _DistortionRangeValue, _PeekDistortionValue, 0, vertexToCustomValueDist );

               fi.position = mul(UNITY_MATRIX_MVP , vi.position + normalize(vi.normal) * vertexToCustomValueDist);
            }
            else 
               fi.position = mul(UNITY_MATRIX_MVP , vi.position);

            
            return fi;
         }

         fixed4 frag ( FragInput fi ) : SV_Target 
         {
            return tex2D(_MainTex, fi.uv) * _Color;
            //return tex2D(_MainTex, fi.uv) * _Color *  ConvertToNewRange ( -1, 1, 0.5, 1 , cos(_Time.y * 5 ) ) ;
            //return _Color;
         }

         ENDCG
      }
   }
}
