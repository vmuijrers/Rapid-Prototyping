Shader "Unlit/RenderOnlyInsideCone"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ConeHeight ("Cone height", float) = 1.0
        _ConeAngle ("Cone Angle", float) = 0.0
        _ConeOrigin ("Cone Origin", Vector) = (0.0, 0.0, 0.0)
        _ConeDirection ("Cone Direction", Vector) = (0.0, 0.0, 0.0)
        _LightColor ("light Color", Color) = (0.0, 0.0, 0.0, 0.0)
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "Render"="Transparent" "IgnoreProjector"="True"}
        LOD 200
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _ConeHeight;
            float _ConeAngle;
            float3 _ConeOrigin;
            float3 _ConeDirection;
            float4 _LightColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float IsInCone(float3 p)
            {
                float height = _ConeHeight;
                float radius = tan(((_ConeAngle * 0.5) / 360.0) * 2.0 * 3.1415) * height;
                float3 dir = _ConeDirection;
                float3 origin = _ConeOrigin;

                float coneDistance = dot(p - origin, dir);
                if (coneDistance < 0 || coneDistance > height) return 0;
                float coneRadius = (coneDistance / height) * radius;
                float orthDistance = length((p - origin) - coneDistance * dir);
                return orthDistance < coneRadius ? 1 : 0;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                col.a = IsInCone(i.worldPos);

                return col * _LightColor;
            }
            ENDCG
        }
    }
}
