#ifndef DITHER_INCLUDED
#define DITHER_INCLUDED
static const int bayer8[8 * 8] = {
    //0, 8, 32, 40, 34, 2, 10, 42, 48, 16, 56, 24, 50, 18, 58, 26, 12, 44, 4, 36, 46, 14, 38, 6, 52, 60, 20, 62, 28, 54, 22, 30, 3, 35, 11, 43, 1, 33, 9, 59, 41, 51, 27, 49, 19, 57, 17, 15, 47, 25, 7, 39, 13, 5, 37, 63, 45, 31, 55, 23, 53, 29, 61, 21
    //0, 2, 10, 32, 8, 40, 18, 24, 56, 16, 34, 48, 36, 4, 6, 26, 42, 44, 12, 60, 50, 14, 58, 52, 20, 22, 28, 46, 3, 38, 62, 11, 59, 41, 54, 33, 30, 1, 57, 49, 43, 35, 51, 47, 13, 27, 17, 19, 9, 61, 25, 63, 15, 29, 45, 23, 39, 7, 5, 21, 37, 53, 31, 55
    0, 32, 40, 2, 8, 10, 34, 42, 48, 16, 56, 24, 50, 18, 26, 58, 44, 12, 36, 4, 14, 46, 6, 38, 60, 28, 52, 20, 62, 54, 30, 22, 3, 35, 11, 43, 33, 1, 9, 41, 19, 51, 59, 27, 49, 17, 57, 25, 15, 7, 47, 39, 13, 45, 5, 37, 63, 31, 55, 23, 61, 29, 21, 53
};


// static const int bayer8[8 * 8] = {
//                 0, 32, 8, 40, 2, 34, 10, 42,
//                 48, 16, 56, 24, 50, 18, 58, 26,  
//                 12, 44,  4, 36, 14, 46,  6, 38, 
//                 60, 28, 52, 20, 62, 30, 54, 22,  
//                 3, 35, 11, 43,  1, 33,  9, 41,  
//                 51, 19, 59, 27, 49, 17, 57, 25, 
//                 15, 47,  7, 39, 13, 45,  5, 37, 
//                 63, 31, 55, 23, 61, 29, 53, 21
// };

float GetBayer8(int x, int y) {
    return float(bayer8[(x % 8) + (y % 8) * 8]) * (1.0f / 64.0f) - 0.5f;
}

void Dither_float(float3 Norm, float2 uv, float DitherScale, float Spread, float3 InColor, float RedColCount, float GrnColCount, float BluColCount,out float3 OutColor)
    {             
        float2 pixelCoord = floor(uv.xy / DitherScale);
        float spread = Spread * (1 + Norm);
        OutColor = InColor + spread * GetBayer8(pixelCoord.x,pixelCoord.y);
        OutColor.r = floor((RedColCount - 1.0f) * OutColor.r + 0.5) / (RedColCount - 1.0f);
        OutColor.g = floor((GrnColCount - 1.0f) * OutColor.g + 0.5) / (GrnColCount - 1.0f);
        OutColor.b = floor((BluColCount - 1.0f) * OutColor.b + 0.5) / (BluColCount - 1.0f);
        // OutColor.r = Norm.x;
        // OutColor.g = Norm.y;
        // OutColor.b = Norm.z;
    }
#endif