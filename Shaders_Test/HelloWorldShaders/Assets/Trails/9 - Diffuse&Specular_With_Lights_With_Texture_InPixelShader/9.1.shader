// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
//#define UNITY_SHADER_NO_UPGRADE 1 
Shader "8" {
   Properties {
      _EnableAmbientColor ("Enable Ambient Color" , int ) = 1
      _EnableDiffuseColor ("Enable Diffuse Color" , int ) = 1
      _EnableSpecularColor ("Enable Specular Color" , int ) = 1
      _DiffuseColor ("Diffuse Material Color", Color) = (1,1,1,1) 
      _SpecColor ("Specular Material Color", Color) = (1,1,1,1) 
      _Shininess ("Shininess", Float) = 10
      _MainTex ("Texture", 2D) = "black" {}
      _DistanceScale("Scale", Float) = 0.1
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
 
         uniform float4 _DiffuseColor; // define shader property for shaders
         uniform sampler2D _MainTex;
         uniform float4 _SpecColor; 
         uniform float _Shininess;
         uniform int _EnableAmbientColor;
         uniform int _EnableDiffuseColor;
         uniform int _EnableSpecularColor;
         uniform float _DistanceScale;
 
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float2 uv : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 vertex : POSITION1;
            float3 normal : NORMAL;
            //float4 col : COLOR;
            float2 uv : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;  

            output.pos = mul ( UNITY_MATRIX_MVP, input.vertex );
            output.vertex = normalize ( mul(unity_ObjectToWorld, input.vertex ) ) ;  // converting vertex to world space 
            output.normal = normalize ( mul(unity_ObjectToWorld, float4 ( input.normal, 0) ).xyz);  // converting normal to world space 
            output.uv = input.uv;

            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float3 normalizedDirectionToLight;
            float attenuation;                  // to know the brightness of point light based on its distance from the vertex 
            float3 viewDirection = normalize( _WorldSpaceCameraPos 
                                                - input.vertex).xyz;

            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1;  // directional lights do not have a brightness radius
               normalizedDirectionToLight = normalize(_WorldSpaceLightPos0.xyz);
            } 
            else // point or spot light
            {
               normalizedDirectionToLight = normalize(_WorldSpaceLightPos0.xyz 
                                                      - input.vertex.xyz);

               float distance = length (  _WorldSpaceLightPos0.xyz - input.vertex.xyz );
               attenuation = 1*_DistanceScale / distance; 
            }

            float3 ambientLighting = float3(0,0,0);
            if ( _EnableAmbientColor == 1 )
               ambientLighting = 
                   UNITY_LIGHTMODEL_AMBIENT.rgb // environment's ambient color 
                   * _DiffuseColor.rgb;         // diffuse color of the material 
 
            float3 diffuseReflection = float3( 0,0,0);
            if ( _EnableDiffuseColor == 1 )
               diffuseReflection =
                  _LightColor0.rgb // light's color
                  * _DiffuseColor.rgb // the material's color 
                  * attenuation  // include brightness of the light based on its distance 
                  * max( 0, 
                        dot( input.normal, normalizedDirectionToLight ) // if find the how intense the reflection is 
                     ); // if its negative, the vertex is facing the opposite side of the light , so we are setting it to zero. 

            
            float3 specularReflection = float3( 0,0,0 );
            if (dot(input.normal, normalizedDirectionToLight) < 0.0 || _EnableSpecularColor != 1 ) 
               // light source on the wrong side?
            {
               specularReflection = float3(0.0, 0.0, 0.0); 
                  // no specular reflection
            }
            else // light source on the right side
            {
               specularReflection = 
                     attenuation       // include brightness of the light based on its distance 
                     * _LightColor0.rgb   // light color 
                     * _SpecColor.rgb     // specular shine color . usually white as shines are usually white. 
                     * pow(
                           max   (0.0, dot   (
                                             reflect(-normalizedDirectionToLight, input.normal), 
                                             viewDirection
                                          )
                                 ), 
                           _Shininess
                           );
            }

            float4 col = float4( diffuseReflection + specularReflection + ambientLighting, 1 );

            return col * tex2D(_MainTex, input.uv);
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
 
         uniform float4 _DiffuseColor; // define shader property for shaders
         uniform sampler2D _MainTex;
         uniform float4 _SpecColor; 
         uniform float _Shininess;
         uniform int _EnableAmbientColor;
         uniform int _EnableDiffuseColor;
         uniform int _EnableSpecularColor;
         uniform float _DistanceScale;

 
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float2 uv : TEXCOORD0;
         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 vertex : POSITION1;
            float3 normal : NORMAL;
            //float4 col : COLOR;
            float2 uv : TEXCOORD0;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;  

            output.pos = mul ( UNITY_MATRIX_MVP, input.vertex );
            output.vertex = normalize ( mul(unity_ObjectToWorld, input.vertex ) ) ;  // converting vertex to world space 
            output.normal = normalize ( mul(unity_ObjectToWorld, float4 ( input.normal, 0) ).xyz);  // converting normal to world space 
            output.uv = input.uv;

            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
            float3 normalizedDirectionToLight;
            float attenuation;                  // to know the brightness of point light based on its distance from the vertex 
            float3 viewDirection = normalize( _WorldSpaceCameraPos 
                                                - input.vertex).xyz;

            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1;  // directional lights do not have a brightness radius
               normalizedDirectionToLight = normalize(_WorldSpaceLightPos0.xyz);
            } 
            else // point or spot light
            {
               normalizedDirectionToLight = normalize(_WorldSpaceLightPos0.xyz 
                                                      - input.vertex.xyz);

               float distance = length (  _WorldSpaceLightPos0.xyz - input.vertex.xyz );
               attenuation = 1 *_DistanceScale/ distance; 
               //attenuation = 0.8;
            }

            float3 ambientLighting = float3(0,0,0);
            // if ( _EnableAmbientColor == 1 )
            //    ambientLighting = 
            //        UNITY_LIGHTMODEL_AMBIENT.rgb // environment's ambient color 
            //        * _DiffuseColor.rgb;         // diffuse color of the material 
 
            float3 diffuseReflection = float3( 0,0,0);
            if ( _EnableDiffuseColor == 1 )
               diffuseReflection =
                  _LightColor0.rgb // light's color
                  * _DiffuseColor.rgb // the material's color 
                  * attenuation  // include brightness of the light based on its distance 
                  * max( 0, 
                        dot( input.normal, normalizedDirectionToLight ) // if find the how intense the reflection is 
                     ); // if its negative, the vertex is facing the opposite side of the light , so we are setting it to zero. 

            
            float3 specularReflection = float3( 0,0,0 );
            if (dot(input.normal, normalizedDirectionToLight) < 0.0 || _EnableSpecularColor != 1 ) 
               // light source on the wrong side?
            {
               specularReflection = float3(0.0, 0.0, 0.0); 
                  // no specular reflection
            }
            else // light source on the right side
            {
               specularReflection = 
                     attenuation       // include brightness of the light based on its distance 
                     * _LightColor0.rgb   // light color 
                     * _SpecColor.rgb     // specular shine color . usually white as shines are usually white. 
                     * pow(
                           max   (0.0, dot   (
                                             reflect(-normalizedDirectionToLight, input.normal), 
                                             viewDirection
                                          )
                                 ), 
                           _Shininess
                           );
            }

            float4 col = float4( diffuseReflection + specularReflection + ambientLighting, 1 );

            return col * tex2D(_MainTex, input.uv);
         }
 
         ENDCG
      }
   }
   //Fallback "Diffuse"
}
