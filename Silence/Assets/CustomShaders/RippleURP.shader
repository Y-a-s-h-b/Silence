Shader "Custom/Unlit/RippleURP"
{
    Properties
    {
        _RippleAlpha("Ripple Alpha", Float) = 1
        _RippleIntensity("Ripple Intensity", Float) = 1
        _Hue("Hue", Color) = (1, 1, 1, 1)
        _NormalMap("Normal Map", 2D) = "white" {}
        _MainTex("Normal Map", 2D) = "white" {}
        _Density("Soft Particles Factor", Range(0, 3)) = 1
    }

    SubShader
    {
        Tags
        {
           "Queue" = "Transparent+1" "RenderType" = "Transparent" "RenderPipeline"="UniversalRenderPipeline"
        }
        Zwrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        Pass
        {

            HLSLPROGRAM
            #pragma multi_compile_particles
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderVariablesFunctions.hlsl"
            //#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareOpaqueTexture.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DeclareDepthTexture.hlsl"

            // Properties
            float _RippleAlpha;
            float _RippleIntensity;
            half4 _Hue;
            sampler2D _NormalMap;
            sampler2D _BackgroundTexture;
            float _Density;
            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            TEXTURE2D(_CameraOpaqueTexture);
            SAMPLER(sampler_CameraOpaqueTexture);
            // Vertex structure
            struct Attributes
            {
                float4 position : POSITION;
                float4 color : COLOR;
                float2 texcoord : TEXCOORD0;
            };

            // Varying structure
            struct Varyings
            {
                float4 position : SV_POSITION;
                float4 color : COLOR;
                float2 normalMap : TEXCOORD0;
                float2 grabScreenPosition : TEXCOORD1;
            };

            Varyings vert(Attributes v)
            {
                Varyings o;

                // World to Clip space transformation
                o.position = TransformObjectToHClip(v.position.xyz);
                o.color = v.color;
                o.normalMap = v.texcoord;
                //o.normalMap = o.position.xy/o.position.w;
                o.grabScreenPosition = o.position.xy / o.position.w;
                o.grabScreenPosition = o.grabScreenPosition * 0.5 + 0.5;
                return o;
            }

            half4 frag(Varyings i) : SV_Target
            {
                half3 ripple = UnpackNormal(tex2D(_NormalMap, i.normalMap.xy));
                i.grabScreenPosition.xy += ripple.xy / ripple.z * _RippleIntensity * i.color.a;

                half4 backgroundColor = SAMPLE_TEXTURE2D(_CameraOpaqueTexture,sampler_CameraOpaqueTexture, i.grabScreenPosition);
                //half4 backgroundColor = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex, i.grabScreenPosition);
                //half4 backgroundColor = tex2D(_CameraDepthTexture, i.grabScreenPosition);
                _Hue.a = _RippleAlpha;
                return (backgroundColor * _Hue);
            }

            ENDHLSL
        }
    }

    // Fallback
    Fallback "Universal Forward"
}
