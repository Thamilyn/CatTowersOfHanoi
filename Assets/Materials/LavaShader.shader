Shader "Custom/LavaShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FlowSpeed ("Flow Speed", Range(0.1, 10)) = 1
        _SparkDensity ("Spark Density", Range(0, 1)) = 0.1
        _SparkTexture ("Spark Texture", 2D) = "white" {}
        _SparkTiling ("Spark Tiling", Vector) = (1, 1, 1, 1)
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        float _FlowSpeed;
        float _SparkDensity;
        sampler2D _SparkTexture;
        float4 _SparkTiling;

        struct Input
        {
            float2 uv_MainTex;
        };

        float pseudoRandom(float2 uv)
        {
            return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453);
        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            // Lava Flow
            float2 flowOffset = float2(IN.uv_MainTex.x * _FlowSpeed, IN.uv_MainTex.y * _FlowSpeed);
            float2 flowUV = IN.uv_MainTex + flowOffset;
            fixed3 lavaColor = tex2D(_MainTex, flowUV).rgb;

            // Sparks
            fixed3 sparkColor = fixed3(0, 0, 0); // Initialize spark color

            if (pseudoRandom(IN.uv_MainTex) < _SparkDensity)
            {
                float2 sparkUV = IN.uv_MainTex * _SparkTiling.xy;
                sparkColor = tex2D(_SparkTexture, sparkUV).rgb;
            }

            // Final Color
            o.Albedo = lavaColor + sparkColor;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
