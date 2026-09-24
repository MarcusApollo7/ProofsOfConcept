using System;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;


namespace CombatPOC;

public class Helper
{
    public static int TileWidth {get; set;}= 80;
    public static int TileHeight {get; set;}= 80;
    public static Vector2 Scale {get; set;}= new(4.0f, 4.0f);
    public static TileLocation ConvertScreenPositionToTileLocation(Vector2 screenPosition)
    {
        return new((int)screenPosition.X / TileWidth, (int)screenPosition.Y / TileHeight);
    }
    public static TileLocation TraverseTiles2D(
    Vector2 origin,
    Vector2 direction,
    int gridWidthInTiles,
    int gridHeightInTiles,
    Func<TileLocation, bool> visit)
    {
        float tileW = TileWidth;
        float tileH = TileHeight;

        // Convert origin → tile index
        int tileX = (int)Math.Floor(origin.X / tileW);
        int tileY = (int)Math.Floor(origin.Y / tileH);

        // Determine step direction
        int stepX = direction.X > 0 ? 1 : (direction.X < 0 ? -1 : 0);
        int stepY = direction.Y > 0 ? 1 : (direction.Y < 0 ? -1 : 0);

        // Compute first boundary intersection
        float nextBoundaryX = stepX > 0 ? (tileX + 1) * tileW : tileX * tileW;
        float nextBoundaryY = stepY > 0 ? (tileY + 1) * tileH : tileY * tileH;

        float tMaxX = stepX != 0 ? (nextBoundaryX - origin.X) / direction.X : float.PositiveInfinity;
        float tMaxY = stepY != 0 ? (nextBoundaryY - origin.Y) / direction.Y : float.PositiveInfinity;

        // Distance between boundaries
        float tDeltaX = stepX != 0 ? tileW / Math.Abs(direction.X) : float.PositiveInfinity;
        float tDeltaY = stepY != 0 ? tileH / Math.Abs(direction.Y) : float.PositiveInfinity;

        while (tileX >= 0 && tileX < gridWidthInTiles &&
            tileY >= 0 && tileY < gridHeightInTiles)
        {
            var tile = new TileLocation(tileX, tileY);
            if (visit(tile))
                return tile;

            if (tMaxX < tMaxY)
            {
                tileX += stepX;
                tMaxX += tDeltaX;
            }
            else
            {
                tileY += stepY;
                tMaxY += tDeltaY;
            }
        }
        return null;
    }
}