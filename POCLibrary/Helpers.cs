using Microsoft.Xna.Framework;

namespace POCLibrary;

public static class Helper
{
    public static int _tileDim = 80;
    public static Vector2 ConvertToTiles(Vector2 positionPixels)
    {
        return positionPixels / _tileDim;
    }
    public static Vector2 ConvertToPixels(Vector2 positionTiles)
    {
        return positionTiles * _tileDim;
    }
}