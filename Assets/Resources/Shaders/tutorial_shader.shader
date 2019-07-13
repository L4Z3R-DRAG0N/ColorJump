Shader "Unlit/tutorial_shader"
{
    Properties
    {

    }
    SubShader
    {

        Pass
        {
            CGPROGRAM

			#pragma vertex vertexFunction
			#pragma fragment fragmentFunction

			#include "UnityCG.cginc"

			struct a2v {
				
			};

			void vertexFunction(){

			}
			void fragmentFunction() {

			}
            ENDCG
        }
    }
}
