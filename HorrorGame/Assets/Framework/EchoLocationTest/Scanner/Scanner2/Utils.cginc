#ifndef UTILS_INCLUDED
#define UTILS_INCLUDED

struct Input
{
	float2 uv_MainTex;
	float2 uv_BumpMap;
	float3 worldPos;
	float3 stripeUvw;
};
inline fixed4 LightingUnlit (SurfaceOutput s, fixed3 lightDir, fixed atten)
{
	return fixed4(s.Albedo, s.Alpha);
}

sampler2D _MainTex, _BumpMap;
float _Metallic, _Glossiness;
float4 _LightSweepVector, _LightSweepColor, _LightSweepAddColor, _Color;
float  _LightSweepAmp, _LightSweepExp, _LightSweepInterval, _LightSweepSpeed;
float _LightSweepRange;

#define EMITTER_COUNT 5

float4 _LightEmitters[EMITTER_COUNT];
float _LightEmitterRange[EMITTER_COUNT];

float4 LightSweepColor(in float4 v)
{
#ifdef ALS_DIRECTIONAL
    float w = dot(v, _LightSweepVector);
#endif
#ifdef ALS_SPHERICAL
    float w = length(v - _LightSweepVector);
#endif
    //w = min(w, 3.0f);

    /* Range Check */
    if (w >= _LightSweepRange)
        return float4(0, 0, 0, 0);

    w -= _Time.y * _LightSweepSpeed;
    w /= _LightSweepInterval;
    w = w - floor(w);

    float p = _LightSweepExp;
    w = (pow(w, p) + pow(1 - w, p * 4)) * 0.5;
    w *= _LightSweepAmp;
    return _LightSweepColor * w + _LightSweepAddColor;
}

// add another item

float4 SingleLightSweepColor(in float4 v, in float4 emitterPosition, in float emitterRange)
{
#ifdef ALS_DIRECTIONAL
    float w = dot(v, emitterPosition);
#endif
#ifdef ALS_SPHERICAL
    float w = length(v - emitterPosition);
#endif
    //w = min(w, 3.0f);

    /* Range Check */
    if (w >= emitterRange)
        return float4(0, 0, 0, 0);

    w -= _Time.y * _LightSweepSpeed;
    w /= _LightSweepInterval;
    w = w - floor(w);

    float p = _LightSweepExp;
    w = (pow(w, p) + pow(1 - w, p * 4)) * 0.5;
    w *= _LightSweepAmp;
    return _LightSweepColor * w + _LightSweepAddColor;
}


float4 MultiLightSweepColor(in float4 v)
{
    float4 result;
    for (int i = 0; i < EMITTER_COUNT; ++i)
    {
        result += SingleLightSweepColor(v, _LightEmitters[i], _LightEmitterRange[i]);
    }
    return saturate(result);
}




#endif