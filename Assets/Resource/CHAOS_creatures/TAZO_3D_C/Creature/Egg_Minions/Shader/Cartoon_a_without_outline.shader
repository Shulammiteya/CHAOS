Shader "TAZO/Cartoon_a" {
    Properties {
        _Color ("Main Color", Color) = (1,1,1,1)
        _RimColor ("Rim Color", Color) = (0.97,0.88,1,0.75)
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _RimPower ("Rim Power", Range(0.5,8.0)) = 3.0
        _Outline ("Outline Width", Range (-0.1, 0.1)) = .0005
        _MainTex ("Texture", 2D) = "white" {}
        _BumpMap ("Normal Map", 2D) = "bump" {}
        _SpecularTex ("Specular Map", 2D) = "gray" {}
        _RampTex ("Shading Ramp", 2D) = "white" {}
        _CutOff("Alpha Cutoff",Range(0,1)) = 0.3
        //_LineOffset ("Outline Depth Offset", Range(0,-10000)) = -1000
    }
   
    SubShader {
        Tags { "RenderType" = "Opaque" }
       LOD 200
		
		//Offset -3, [_LineOffset]
		Cull Off
        CGPROGRAM
            #pragma surface surf TF2 alphatest:_CutOff //addshadow
            #pragma target 3.0
 
            struct Input
            {
                float2 uv_MainTex;
                float3 worldNormal;
                INTERNAL_DATA
            };
           
            sampler2D _MainTex, _SpecularTex, _BumpMap, _RampTex;
            float4 _RimColor;
            float  _RimPower;
            fixed4 _Color;
 
            inline fixed4 LightingTF2 (SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
            {
                fixed3 h = normalize (lightDir + viewDir);
 
                fixed NdotL = dot(s.Normal, lightDir) * 0.5 + 0.5;
                fixed3 ramp = tex2D(_RampTex, float2(NdotL * atten,NdotL * atten)).rgb;
 
                float nh = max (0, dot (s.Normal, h));
                float spec = pow (nh, s.Gloss * 128) * s.Specular;
 
                fixed4 c;
                c.rgb = ((s.Albedo * _Color.rgb * ramp * _LightColor0.rgb + _LightColor0.rgb * spec) * (atten * 2));
                c.a = 0;
                return c;
            }
   
            void surf (Input IN, inout SurfaceOutput o)
            {
                o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
                o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
                float3 specGloss = tex2D(_SpecularTex, IN.uv_MainTex).rgb;
                o.Specular = specGloss.r;
                o.Gloss = specGloss.g;
 
                half3 rim = pow(max(0, dot(float3(0, 1, 0), WorldNormalVector (IN, o.Normal))), _RimPower) * _RimColor.rgb * _RimColor.a * specGloss.b;
                o.Emission = rim;
                o.Alpha = tex2D(_MainTex, IN.uv_MainTex).a;
            }
   
            ENDCG
      
    
      }
    Fallback "Bumped Specular"
}