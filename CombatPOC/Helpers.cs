using System;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;

namespace CombatPOC;

public class Helper
{
    public static float RenderedTileWidth => Scale.X * 20;
    public static float RenderedTileHeight => Scale.Y * 20;
    public static int TileWidth => (int)RenderedTileWidth;
    public static int TileHeight => (int)RenderedTileHeight;
    public static Vector2 Scale {get; set;}
    public static TileLocation ConvertScreenPositionToTileLocation(Vector2 screenPosition)
    {
        return new(
            (int)MathF.Floor(screenPosition.X / RenderedTileWidth),
            (int)MathF.Floor(screenPosition.Y / RenderedTileHeight));
    }
    public static TileLocation TraverseTiles2D(
    Vector2 origin,
    Vector2 direction,
    int gridWidthInTiles,
    int gridHeightInTiles,
    Func<TileLocation, bool> visit)
    {
        if (gridWidthInTiles <= 0 || gridHeightInTiles <= 0 || visit is null)
            return null;

        float tileW = RenderedTileWidth;
        float tileH = RenderedTileHeight;
        if (tileW <= 0 || tileH <= 0)
            return null;

        int stepX = direction.X > 0 ? 1 : (direction.X < 0 ? -1 : 0);
        int stepY = direction.Y > 0 ? 1 : (direction.Y < 0 ? -1 : 0);

        if (stepX == 0 && stepY == 0)
        {
            int stationaryTileX = (int)MathF.Floor(origin.X / tileW);
            int stationaryTileY = (int)MathF.Floor(origin.Y / tileH);
            if (stationaryTileX < 0 || stationaryTileX >= gridWidthInTiles ||
                stationaryTileY < 0 || stationaryTileY >= gridHeightInTiles)
                return null;

            TileLocation stationaryTile = new(stationaryTileX, stationaryTileY);
            return visit(stationaryTile) ? stationaryTile : null;
        }

        float tStart = 0f;
        float tEnd = 1f;
        float mapWidth = gridWidthInTiles * tileW;
        float mapHeight = gridHeightInTiles * tileH;

        if (!ClipSegmentToAxis(origin.X, direction.X, mapWidth, ref tStart, ref tEnd) ||
            !ClipSegmentToAxis(origin.Y, direction.Y, mapHeight, ref tStart, ref tEnd) ||
            tStart > tEnd)
            return null;

        float startX = origin.X + direction.X * tStart;
        float startY = origin.Y + direction.Y * tStart;
        float entryEpsilon = 0.00001f;
        startX += direction.X * entryEpsilon;
        startY += direction.Y * entryEpsilon;

        int tileX = (int)MathF.Floor(startX / tileW);
        int tileY = (int)MathF.Floor(startY / tileH);

        if (tileX < 0 || tileX >= gridWidthInTiles ||
            tileY < 0 || tileY >= gridHeightInTiles)
            return null;

        float nextBoundaryX = stepX > 0 ? (tileX + 1) * tileW : tileX * tileW;
        float nextBoundaryY = stepY > 0 ? (tileY + 1) * tileH : tileY * tileH;
        float tMaxX = stepX != 0 ? (nextBoundaryX - origin.X) / direction.X : float.PositiveInfinity;
        float tMaxY = stepY != 0 ? (nextBoundaryY - origin.Y) / direction.Y : float.PositiveInfinity;
        float tDeltaX = stepX != 0 ? tileW / Math.Abs(direction.X) : float.PositiveInfinity;
        float tDeltaY = stepY != 0 ? tileH / Math.Abs(direction.Y) : float.PositiveInfinity;
        float tCurrent = tStart;

        while (tileX >= 0 && tileX < gridWidthInTiles &&
            tileY >= 0 && tileY < gridHeightInTiles &&
            tCurrent <= tEnd)
        {
            TileLocation tile = new(tileX, tileY);
            if (visit(tile))
                return tile;

            if (tMaxX < tMaxY)
            {
                tCurrent = tMaxX;
                tileX += stepX;
                tMaxX += tDeltaX;
            }
            else if (tMaxY < tMaxX)
            {
                tCurrent = tMaxY;
                tileY += stepY;
                tMaxY += tDeltaY;
            }
            else
            {
                tCurrent = tMaxX;
                tileX += stepX;
                tileY += stepY;
                tMaxX += tDeltaX;
                tMaxY += tDeltaY;
            }
        }
        return null;
    }

    private static bool ClipSegmentToAxis(float origin, float direction, float extent, ref float tStart, ref float tEnd)
    {
        if (direction == 0)
            return origin >= 0 && origin < extent;

        float t1 = -origin / direction;
        float t2 = (extent - origin) / direction;
        tStart = MathF.Max(tStart, MathF.Min(t1, t2));
        tEnd = MathF.Min(tEnd, MathF.Max(t1, t2));
        return tStart <= tEnd;
    }
}