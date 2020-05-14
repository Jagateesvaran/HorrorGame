Shader "Scanner/Scanner Ground Halo" {
	Properties {
		_MainTex   ("Main", 2D) = "white" {}
		_Color     ("Color", Color) = (1,1,1,1)
		_Center    ("Center", Vector) = (0,0,0,0)
		_Radius    ("Radius", Float) = 0.5
		_HaloColor ("Halo Color", Color) = (1,0,0,1)
		_HaloWidth ("Halo Width", Float) = 2
		_HaloPower ("Halo Power", Float) = 32
		_HaloSpeed ("Halo Speed", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0
		
		sampler2D _MainTex;
		fixed4 _Color, _Center, _HaloColor;
		float _Radius, _HaloWidth, _HaloPower, _HaloSpeed;

		struct Input {
			float2 uv_MainTex;
			float3 worldPos;
		};
		void surf (Input IN, inout SurfaceOutputStandard o)
		{
			float d = distance(_Center, IN.worldPos);
			float t = _HaloWidth * abs(sin(_Time.y * _HaloSpeed));
			float rng = _Radius + t;

			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color;
			if (d > _Radius && d < rng)
				o.Emission = _HaloColor * pow(d / rng, _HaloPower);
			else
				o.Emission = 0;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
