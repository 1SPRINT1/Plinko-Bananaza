// Simplified Additive Particle shader. Differences from regular Additive Particle one:
// - no Tint color
// - no Smooth particle support
// - no AlphaTest
// - no ColorMask

// Cartoon FX difference:
// - uses Alpha8 monochrome textures to save up on texture memory size

Shader "Cartoon FX/Particles Additive Alpha8 Animated"
{
Properties
{
	_MainTex ("Texture", 2D) = "white" {}
	        _ScrollXSpeed("X scroll speed", Range(-10, 10)) = 0
        _ScrollYSpeed("Y scroll speed", Range(-10, 10)) = -0.4 
}

Category
{
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha OneMinusSrcAlpha
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	
	BindChannels
	{
		Bind "Color", color
		Bind "Vertex", vertex
		Bind "TexCoord", texcoord
	}
	
	SubShader
	{              
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

        sampler2D _MainTex;
        fixed _ScrollXSpeed;
        fixed _ScrollYSpeed;

        struct Input 
        {
            float2 uv_MainTex;
			float2 mainUV;
        };
      
        void surf(Input IN, inout SurfaceOutput o) 
        {
            fixed offsetX = _ScrollXSpeed * _Time;
            fixed offsetY = _ScrollYSpeed * _Time;
            fixed2 offsetUV = fixed2(offsetX, offsetY);
            float2 mainUV = IN.uv_MainTex + offsetUV;
			half4 c = tex2D (_MainTex, IN.mainUV);

            o.Albedo = c.rgb;
        }

      }
            // o.Alpha = c.a;
                ENDCG
}


}