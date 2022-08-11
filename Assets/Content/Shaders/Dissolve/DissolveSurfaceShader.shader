Shader "Custom/DissolveSurfaceShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0


        _NoiseTex ("Noise (R)", 2D) = "white" {}
        _NoiseScale ("Noise Scale",Float) = 1.0

        _RimTex ("Rim (RGB)", 2D) = "white" {}

        _RimSize ("Rim Size",Float) = 0.1

        _DissolveProgress ("DissolveProgress", Range(0,1)) = 0.0
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows
        #pragma vertex vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 4.0

        sampler2D _MainTex;
        sampler2D _NoiseTex;
        sampler2D _RimTex;

        struct Input
        {
            float2 uv_MainTex;
            float3 localCoord;
            float3 localNormal;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        half _DissolveProgress;

        half _NoiseScale;

        half _RimSize;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        half4 TriplanarMapping(uniform sampler2D tex,float2 texScale,float3 localCoord,float3 localNormal){
           

            float3 bf = normalize(abs(localNormal));
            bf /= dot(bf, (float3)1);

            // Triplanar mapping
            float2 tx = localCoord.yz * texScale;
            float2 ty = localCoord.zx * texScale;
            float2 tz = localCoord.xy * texScale;

            // Base color
            half4 cx = tex2D(tex, tx) * bf.x;
            half4 cy = tex2D(tex, ty) * bf.y;
            half4 cz = tex2D(tex, tz) * bf.z;
            half4 color = (cx + cy + cz);
             return color;
        }

        float map(float value, float min1, float max1, float min2, float max2){
            // Convert the current value to a percentage
            // 0% - min1, 100% - max1
            float perc = (value - min1) / (max1 - min1);

            // Do the same operation backwards with min2 and max2
            value = perc * (max2 - min2) + min2;

            return value;
        }


        void vert(inout appdata_full v, out Input data)
        {
            UNITY_INITIALIZE_OUTPUT(Input, data);
            data.localCoord = v.vertex.xyz;
            data.localNormal = v.normal.xyz;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

            fixed4 noise = TriplanarMapping(_NoiseTex,(float2)_NoiseScale,IN.localCoord,IN.localNormal);

            float dissolveValue = (1 - _DissolveProgress) * -_RimSize + _DissolveProgress * 1;

            if(noise.r-dissolveValue <= 0){
                discard;
            }

            float k = (noise.r - dissolveValue)/_RimSize;

            fixed4 c;

            if(k >= 1){
                c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            } else {
                c=tex2D(_RimTex,(float2)k);
            }

            

            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }


        ENDCG
    }
    FallBack "Diffuse"
}
