Shader "DemoShader-template"
{
   Properties 
   {

   }
   Subshader 
   {

      Pass 
      {
         Tags { "RenderType"="Opaque" }

         CGPROGRAM

         #pragma vertex vert  
         #pragma fragment frag 

         struct VertexInput
         {
         };
         struct FragInput 
         {
         };


         FragInput vert ( VertexInput vi )
         {
            FragInput fi;
            return fi;
         }

         fixed4 frag ( FragInput fi ) : SV_Target 
         {
            return float4(0,0,0,0);
         }


         ENDCG
      }
   }
}
