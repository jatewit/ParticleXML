//
// Author: Jate Wittayabundit
//
Shader "Custom/Unlit/Icon Loader (Gray Scale - Color)" {
	Properties
	{
		_MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}
	}

	SubShader
	{
		LOD 200

		Tags
		{
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
		}

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off Lighting Off ZWrite Off
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Percent;

			struct appdata_t
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex : POSITION;
				half4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.color = v.color;
				o.texcoord = v.texcoord * _MainTex_ST.xy + _MainTex_ST.zw;
				return o;
			}

			half4 frag (v2f IN) : COLOR
			{
				half4 mainTex = tex2D(_MainTex, IN.texcoord);
				half4 col;
				if (fmod(IN.texcoord.y,1.0) <= IN.color.a) {
					col.rgb = mainTex.rgb * IN.color.rgb; 
				} else {
					col.rgb = (dot(mainTex.rgb, fixed3(.222,.707,.071)) - 0.1) * IN.color.rgb; 	
				}
				col.a = mainTex.a * IN.color.a;
				return col;
			}
			ENDCG
		}
	}
}