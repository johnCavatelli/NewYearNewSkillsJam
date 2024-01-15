#ifndef DITHER_INCLUDED
#define DITHER_INCLUDED

void Dither_float(float3 Norm, float2 uv, float DitherScale, float Spread, float3 InColor, float RedColCount, float GrnColCount, float BluColCount,out float3 OutColor)
    {           
        static const int bayer8[8 * 8] = {
            0, 32, 8, 40, 2, 34, 10, 42,
            48, 16, 56, 24, 50, 18, 58, 26,  
            12, 44,  4, 36, 14, 46,  6, 38, 
            60, 28, 52, 20, 62, 30, 54, 22,  
            3, 35, 11, 43,  1, 33,  9, 41,  
            51, 19, 59, 27, 49, 17, 57, 25, 
            15, 47,  7, 39, 13, 45,  5, 37, 
            63, 31, 55, 23, 61, 29, 53, 21
        };
        
        float sp = 0.026f;
        float sc = 0.004f;

        float2 pixelCoord = floor(uv.xy / sc);
        float spread = sp * (1 + Norm);
        // OutColor = InColor + spread * GetBayer8(pixelCoord.x,pixelCoord.y);
        OutColor = InColor + spread * ( float(bayer8[(pixelCoord.x % 8) + (pixelCoord.y % 8) * 8]) * (1.0f / 64.0f) - 0.5f);
        OutColor.r = floor((RedColCount - 1.0f) * OutColor.r + 0.5) / (RedColCount - 1.0f);
        OutColor.g = floor((GrnColCount - 1.0f) * OutColor.g + 0.5) / (GrnColCount - 1.0f);
        OutColor.b = floor((BluColCount - 1.0f) * OutColor.b + 0.5) / (BluColCount - 1.0f);
        // OutColor.r = Norm.x;
        // OutColor.g = 0.5;
        // OutColor.b = Norm.z;
    }
#endif