Shader "Unlit/CellShaded"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = (0.4,0.4,0.4,1)
        [HDR]
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
        [HDR]
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
        _Glossiness("Glossiness", Float) = 32

        [HDR]
        _RimColor("Rim Color", Color) = (1,1,1,1)
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1

        _LightIntensityUpperThreshold("LightIntensityUpperThreshold", Range(-1, 1)) = 0.01
        _LightIntensityLowerThreshold("LightIntensityLowerThreshold", Range(-1, 1)) = 0.0
    }
    SubShader
    {
        Tags
        {
	        "LightMode" = "ForwardBase"
	        "PassFlags" = "OnlyDirectional"
			"RenderType" = "Opaque" 
        }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 pos : SV_POSITION;
                float3 worldNormal : NORMAL;
                float3 viewDir : TEXCOORD1;
                SHADOW_COORDS(2)
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _AmbientColor;
            float _Glossiness;
            float4 _SpecularColor;
            float4 _Color;

            float4 _RimColor;
            float _RimAmount;
            float _RimThreshold;
            float _LightIntensityUpperThreshold;
            float _LightIntensityLowerThreshold;
            v2f vert (appdata v)
            {
                v2f o;
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.viewDir = WorldSpaceViewDir(v.vertex);
                TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 viewDir = normalize(i.viewDir);

                float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
                float3 normal = normalize(i.worldNormal);
                float NdotH = dot(normal, halfVector);

                float shadow = SHADOW_ATTENUATION(i);
                float NdotL = dot(_WorldSpaceLightPos0, normal);
                float lightIntensity = smoothstep(_LightIntensityLowerThreshold, _LightIntensityUpperThreshold, NdotL * shadow);

                float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                float4 specular = specularIntensitySmooth * _SpecularColor;

                float4 rimDot = 1 - dot(viewDir, normal);
                float rimIntensity = rimDot * pow(NdotL, _RimThreshold);
                rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                float4 rim = rimIntensity * _RimColor;

                float4 light = lightIntensity * _LightColor0;
                fixed4 col = tex2D(_MainTex, i.uv);
                return col * _Color * (_AmbientColor + light + specular + rim);
            }
            ENDCG
        }

        /*
        Shadow pass
        */
        Pass
		{		
			Tags{ "LightMode" = "ShadowCaster" }		
			CGPROGRAM
			#pragma vertex vertex_shader
			#pragma fragment pixel_shader
			#pragma target 3.0
			
			float hash(float n)
			{
				return frac(sin(n)*43758.5453123);
			}
					
			float4 vertex_shader (float4 vertex:POSITION,uint id:SV_VertexID, float3 normal:NORMAL) : SV_POSITION
			{
				//vertex.xyz-=(sin(_Time.g)*0.5+0.5)*normal*hash(float(id));
				return UnityObjectToClipPos(vertex);								
			}

			float4 pixel_shader (void) : COLOR
			{
				return (0.5,0.5,0.5,1.0);
			}
			ENDCG
		}

        //UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
    }
}
