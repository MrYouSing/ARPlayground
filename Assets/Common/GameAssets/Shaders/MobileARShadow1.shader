Shader "Custom/MobileARShadow1"
{
    Properties
    {
    }
		SubShader
		{
		Tags { "RenderType" = "Transparent" "Queue" = "Transparent" }
		Pass
		{
			Blend Zero SrcColor
			Tags
			{
				"LightMode" = "UniversalForward"
			}
			ZWrite On
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS
			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
			#pragma multi_compile _ Anti_Aliasing_ON
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
 
			struct appdata
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
 
			};
 
			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
			};
 
			v2f vert(appdata v)
			{
				v2f o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
 
				return o;
			}
 
			float4 _Color;
 
			float4 frag(v2f i) : SV_Target
			{
				float4 SHADOW_COORDS = TransformWorldToShadowCoord(i.worldPos);
 
				Light light = GetMainLight(SHADOW_COORDS);
				float attenuation = light.shadowAttenuation;
 
				return float4(1.0, 1.0, 1.0, 1.0) * attenuation;
			}
			ENDHLSL
		}
	}
}
