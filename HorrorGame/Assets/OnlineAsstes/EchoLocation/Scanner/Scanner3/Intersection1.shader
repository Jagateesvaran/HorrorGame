Shader "Scanner/Intersection1" {
	Properties {
		_IntersectionMax        ("Intersection Max", Float) = 1 
		_IntersectionDamper     ("Intersection Damper", Float) = 0.3
		[HDR]_MainColor         ("Main", Color) = (1, 1, 1, 0.25)
		_MainTex                ("Main Texture", 2D) = "white" {}
		[HDR]_IntersectionColor ("Intersection Color", Color) = (1, 1, 1, 1)
		_IntersectionTex        ("Intersection Texture", 2D) = "white" {}
		_TextureScroll          ("Texture Scroll", Vector) = (0, 0, 0, 0)
		[Header(Rim)]
		[Toggle(RIM)]_RIM       ("Rim", Float) = 1
		_RimPower               ("Rim Power", Range(1, 10)) = 2
		_RimIntensity           ("Rim Intensity", Range(1, 6)) = 2
	}
	SubShader {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off Cull Off
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature RIM
			#pragma target 3.0
			#include "UnityCG.cginc"

#if UNITY_VERSION < 540
			#define COMPUTESCREENPOS ComputeScreenPos
#else
			#define COMPUTESCREENPOS ComputeNonStereoScreenPos
#endif
			sampler2D _CameraDepthTexture, _MainTex, _IntersectionTex;
			fixed4 _MainColor, _IntersectionColor, _RimColor, _TextureScroll;
			half _IntersectionMax, _IntersectionDamper, _RimPower, _RimIntensity;
			float4 _MainTex_ST, _IntersectionTex_ST;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 scrpos : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
				float fresnel : TEXCOORD3;
			};
			v2f vert (appdata_base v)
			{
				float4 wp = mul(unity_ObjectToWorld, v.vertex);
				float3 vdir = normalize(ObjSpaceViewDir(v.vertex));

				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.texcoord, _IntersectionTex);
				o.scrpos = COMPUTESCREENPOS(o.pos);
				o.scrpos.z = lerp(o.pos.w, mul(UNITY_MATRIX_V, wp).z, unity_OrthoParams.w);
				o.fresnel = 1.0 - saturate(dot(vdir, v.normal));
				return o;
			}
			fixed4 frag (v2f i, fixed facing : VFACE) : SV_Target
			{
				float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrpos));
				float perpectiveZ = LinearEyeDepth(sceneZ);
#if defined(UNITY_REVERSED_Z)
				sceneZ = 1 - sceneZ;
#endif
				float orthoZ = sceneZ * (_ProjectionParams.y - _ProjectionParams.z) - _ProjectionParams.y;
				sceneZ = lerp(perpectiveZ, orthoZ, unity_OrthoParams.w);
				float dist = sqrt(pow(sceneZ - i.scrpos.z, 2));

				fixed4 iscol = tex2D(_IntersectionTex, i.uv2 + _TextureScroll.xy * _Time.x);

				half mask = max(0, sign(_IntersectionMax - dist));
				mask *= 1.0 - dist / _IntersectionMax * _IntersectionDamper;
				mask *= iscol.a * _IntersectionColor.a;

				fixed4 col = tex2D(_MainTex, i.uv + _TextureScroll.xy * _Time.x);
				col *= _MainColor * (1.0 - mask);
				col += iscol * _IntersectionColor * mask;
#if RIM
				float f = facing > 0 ? pow(i.fresnel, _RimPower) * _RimIntensity : 1.0;
				col.a *= f;
#endif
				col.a = max(mask, col.a);
				return col;
			}
			ENDCG
		}
	}
	FallBack "Unlit/Color"
}
