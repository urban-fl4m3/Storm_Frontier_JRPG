void ToonLightning_float(in float3 Normal, in float ToonRampSmoothness, in float3 ClipSpacePos, in float3 WorldPos,
    in float4 ToonRampingTinting, in float ToonRampOffset, out float3 ToonRampOutput, out float3 Direction)
{
    #ifdef SHADERGRAPH_PREVIEW

    ToonRampOutput = float3(0.5f, 0.5f, 0);
    Direction = float3(0.5f, 0.5f, 0);

    #else

    #if SHADOW_SCREEN

    half4 shadowCoord = ComputeScreenPos(ClipSpacePos);

    #else

    half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
    
    #endif

    #if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS

    Light light = GetMainLight(shadowCoord);
    
    #else

    Light light = GetMainLight();
    
    #endif

    half d = dot(Normal, light.direction) * 0.5f + 0.5f;
    half toonRamp = smoothstep(ToonRampOffset, ToonRampOffset + ToonRampSmoothness, d);

    toonRamp *= light.shadowAttenuation;

    ToonRampOutput = light.color * (toonRamp + ToonRampingTinting);

    Direction = light.direction;
    
    #endif
}

void ToonLightning_half(in half3 Normal, in half ToonRampSmoothness, in half3 ClipSpacePos, in half3 WorldPos,
    in half4 ToonRampingTinting, in half ToonRampOffset, out half3 ToonRampOutput, out half3 Direction)
{
    #ifdef SHADERGRAPH_PREVIEW

    ToonRampOutput = half3(0.5f, 0.5f, 0);
    Direction = half3(0.5f, 0.5f, 0);

    #else

    #if SHADOW_SCREEN

    half4 shadowCoord = ComputeScreenPos(ClipSpacePos);

    #else

    half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
    
    #endif

    #if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS

    Light light = GetMainLight(shadowCoord);
    
    #else

    Light light = GetMainLight();
    
    #endif

    half d = dot(Normal, light.direction) * 0.5f + 0.5f;
    half toonRamp = smoothstep(ToonRampOffset, ToonRampOffset + ToonRampSmoothness, d);

    toonRamp *= light.shadowAttenuation;

    ToonRampOutput = light.color * (toonRamp, ToonRampingTinting);

    Direction = light.direction;
    
    #endif
}