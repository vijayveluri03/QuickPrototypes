// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
//#define UNITY_SHADER_NO_UPGRADE 1 
Shader "8" {
   Properties {
      _Color ("Diffuse Material Color", Color) = (1,1,1,1) 
      _MainTex ("Texture", 2D) = "black" {}
   }
   SubShader {
      Pass {	
         Tags { "LightMode" = "ForwardBase" } 
            // make sure that all uniforms are correctly set
 
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
 
         uniform float4 _LightColor0; 
            // color of light source (from "Lighting.cginc")
 
         uniform float4 _Color; // define shader property for shaders
         sampler2D _MainTex;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float2 uv : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 col : COLOR;
            float2 uv : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;  

            //float3 normalizedNormalInWS = normalize ( mul ( float4( input.normal, 0) , unity_WorldToObject ).xyz );   // converting normal to world space 
            float3 normalizedNormalInWS = normalize ( mul(unity_ObjectToWorld, float4 ( input.normal, 0) ).xyz);  // converting normal to world space 
            float3 normalizedDirectionToLight;
            float attenuation;                  // to know the brightness of point light based on its distance from the vertex 

            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1;  // directional lights do not have a brightness radius
               normalizedDirectionToLight = normalize(_WorldSpaceLightPos0.xyz);
            } 
            else // point or spot light
            {
               normalizedDirectionToLight = normalize(_WorldSpaceLightPos0.xyz 
                                                      - mul(unity_ObjectToWorld, input.vertex).xyz);

               float distance = length (  _WorldSpaceLightPos0.xyz - mul(unity_ObjectToWorld, input.vertex).xyz );
               attenuation = 1 / distance; 
            }


            float3 diffuseColor = 
                  _LightColor0.rgb // light's color
                  * _Color.rgb // the material's color 
                  * attenuation  // include brightness of the light based on its distance 
                  * max( 0, 
                        dot( normalizedNormalInWS, normalizedDirectionToLight ) // if find the how intense the reflection is 
                     ); // if its negative, the vertex is facing the opposite side of the light , so we are setting it to zero. 

            output.pos = mul ( UNITY_MATRIX_MVP, input.vertex );
            output.col = float4( diffuseColor, 1 );
            output.uv = input.uv;
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {

            return input.col * tex2D(_MainTex, input.uv);
         }
 
         ENDCG
      }
      // second pass for lights 
      Pass {	
         Tags { "LightMode" = "ForwardAdd" } 
            // make sure that all uniforms are correctly set
         Blend One One  // this is needed for lighting 
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag 
 
         #include "UnityCG.cginc"
 
         uniform float4 _LightColor0; 
            // color of light source (from "Lighting.cginc")
 
         uniform float4 _Color; // define shader property for shaders
         sampler2D _MainTex;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float2 uv : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 col : COLOR;
            float2 uv : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;  

            //float3 normalizedNormalInWS = normalize ( mul ( float4( input.normal, 0) , unity_WorldToObject ).xyz );   // converting normal to world space 
            float3 normalizedNormalInWS = normalize ( mul(unity_ObjectToWorld, float4 ( input.normal, 0) ).xyz);  // converting normal to world space 
            float3 normalizedDirectionToLight;
            float attenuation;                  // to know the brightness of point light based on its distance from the vertex 

            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1;  // directional lights do not have a brightness radius
               normalizedDirectionToLight = normalize(_WorldSpaceLightPos0.xyz);
            } 
            else // point or spot light
            {
               normalizedDirectionToLight = normalize(_WorldSpaceLightPos0.xyz 
                                                      - mul(unity_ObjectToWorld, input.vertex).xyz);

               float distance = length (  _WorldSpaceLightPos0.xyz - mul(unity_ObjectToWorld, input.vertex).xyz );
               attenuation = 1 / distance; 
            }


            float3 diffuseColor = 
                  _LightColor0.rgb // light's color
                  * _Color.rgb // the material's color 
                  * attenuation  // include brightness of the light based on its distance 
                  * max( 0, 
                        dot( normalizedNormalInWS, normalizedDirectionToLight ) // if find the how intense the reflection is 
                     ); // if its negative, the vertex is facing the opposite side of the light , so we are setting it to zero. 

            output.pos = mul ( UNITY_MATRIX_MVP, input.vertex );
            output.col = float4( diffuseColor, 1 );
            output.uv = input.uv;
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {

            return input.col * tex2D(_MainTex, input.uv);
         }
 
         ENDCG
      }
   }
   //Fallback "Diffuse"
}
