Shader "Scanner/Intersection2" {
	Properties {
		_IntersectionMax        ("Intersection Max", Float) = 1 
		_IntersectionIntenstiy  ("Intersection Intensity", Range(0, 1)) = 1
		[HDR]_MainColor         ("Main", Color) = (1, 1, 1, 0.25)
		_MainTex                ("Main Texture", 2D) = "white" {}
		[HDR]_IntersectionColor ("Intersection Color", Color) = (1, 1, 1, 1)
		_IntersectionTex        ("Intersection Texture", 2D) = "white" {}
		_TextureScroll          ("Texture Scroll", Vector) = (0, 0, 0, 0)

		_RimPower       ("Rim Power", Range(1, 10)) = 3
		_RimIntensity   ("Rim Intensity", Range(0, 1)) = 1
		_BasicOpacity   ("Basic Opacity", Range(0.0, 1.0)) = 0.08

		[Header(Wave)]
		_CollisionPos   ("Collision", Vector) = (0, 0, 0, 0)
		_WaveScale      ("Wave Scale", Range(0, 1)) = 0
		
		[Header(Distortion)]
		_BumpTex        ("Bump", 2D) = "bump" {}
		_BumpUvScroll   ("Bump Uv Scroll", Vector) = (0, 0, 0, 0)
		_BumpIntensity  ("Bump Intensity", Float) = 1
	}
	SubShader {
		Tags { "Queue" = "Transparent-1" "RenderType" = "Transparent" }
		Blend Off ZWrite Off
//		Blend SrcAlpha OneMinusSrcAlpha ZWrite Off Cull Off
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
#if UNITY_VERSION < 540
			#define COMPUTESCREENPOS ComputeScreenPos
#else
			#define COMPUTESCREENPOS ComputeNonStereoScreenPos
#endif
			sampler2D _CameraDepthTexture, _MainTex, _BumpTex, _IntersectionTex, _Global_GrabTex;
			fixed4 _MainColor, _IntersectionColor, _RimColor, _TextureScroll, _BumpUvScroll;
			half _IntersectionMax, _IntersectionIntenstiy, _BasicOpacity, _RimPower, _RimIntensity, _WaveScale, _BumpIntensity;
			float4 _MainTex_ST, _IntersectionTex_ST, _BumpTex_ST, _Global_GrabTex_TexelSize, _CollisionPos;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 scrpos : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
				float fresnel : TEXCOORD3;
				float3 wldpos : TEXCOORD4;
				float2 uv3 : TEXCOORD5;
			};
			v2f vert (appdata_base v)
			{
				float4 wp = mul(unity_ObjectToWorld, v.vertex);
				float3 vdir = normalize(ObjSpaceViewDir(v.vertex));

				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.texcoord, _IntersectionTex);
				o.uv3 = TRANSFORM_TEX(v.texcoord, _BumpTex);
				o.scrpos = COMPUTESCREENPOS(o.pos);
				o.scrpos.z = lerp(o.pos.w, mul(UNITY_MATRIX_V, wp).z, unity_OrthoParams.w);
				o.fresnel = 1.0 - saturate(dot(vdir, v.normal));
				o.wldpos = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			fixed4 frag (v2f i) : SV_Target
			{
				// ripple wave distortion
				float2 scruv = i.scrpos.xy / i.scrpos.w;
				float d = distance(i.wldpos, _CollisionPos.xyz);
				half o2r = pow(1.0 - _WaveScale, 1.0 / 2.2) * 0.8;
				half w = 0.2 * _WaveScale;
				float pr = smoothstep(o2r, o2r + w, d) * (1.0 - smoothstep(o2r + w, o2r + 2.0 * w, d));
				float wave = pr * sin(d * 50.0) * 30.0 * _WaveScale;
				
				// bump wave distortion
				float3 bump = UnpackNormal(tex2D(_BumpTex, i.uv3 + _BumpUvScroll.xy * _Time.x));
				scruv += _Global_GrabTex_TexelSize.xy * bump.xy * _BumpIntensity;
				
				fixed3 scrcol = tex2D(_Global_GrabTex, scruv + _Global_GrabTex_TexelSize.xy * wave).rgb;

				// depth intersection
				float sceneZ = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.scrpos));
				float perpectiveZ = LinearEyeDepth(sceneZ);
#if defined(UNITY_REVERSED_Z)
				sceneZ = 1 - sceneZ;
#endif
				float orthoZ = sceneZ * (_ProjectionParams.y - _ProjectionParams.z) - _ProjectionParams.y;
				sceneZ = lerp(perpectiveZ, orthoZ, unity_OrthoParams.w);
				float dist = saturate(sqrt(pow(sceneZ - i.scrpos.z, 1)));

				fixed4 iscol = tex2D(_IntersectionTex, i.uv2 + _TextureScroll.xy * _Time.x);

				half mask = max(0, sign(_IntersectionMax - dist));
				mask *= 1.0 - dist / _IntersectionMax;
				mask *= iscol.a * _IntersectionColor.a;
				mask *= _IntersectionIntenstiy;

				fixed4 c = tex2D(_MainTex, i.uv + _TextureScroll.xy * _Time.x);
				c *= _MainColor * (1.0 - mask);
				c += iscol * _IntersectionColor * mask;

				// fresnel
				float fsl = pow(i.fresnel, _RimPower) * _RimIntensity + _BasicOpacity;

				c.rgb = (fsl + mask) * (c.rgb - scrcol) + scrcol;
				return c;
			}
			ENDCG
		}
	}
	FallBack "Unlit/Color"
}
