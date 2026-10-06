using System;
using Microsoft.Xna.Framework;

namespace CombatPOC.Logic;

public record class TileLocation(int x, int y, bool walkable = true)
{
    public int X {get; set; } = x;
    public int Y {get; set; } = y;
    public bool Walkable {get; set; } = walkable;
    public static TileLocation operator +(TileLocation a, TileLocation b)
    {
        return new(a.X + b.X, a.Y + b.Y); // returns a component-wise sum
    }
    public static TileLocation operator -(TileLocation a, TileLocation b)
    {
        return new(a.X - b.X, a.Y - b.Y); // returns a component-wise difference
    }
    public Vector2 ToScreenPosition()
    {
        return new(X * Helper.RenderedTileWidth, Y * Helper.RenderedTileHeight);
    }
    public Vector2 GetCenter()
    {
        return new(
            X * Helper.RenderedTileWidth + Helper.RenderedTileWidth / 2,
            Y * Helper.RenderedTileHeight + Helper.RenderedTileHeight / 2);
    }
    public Vector2 GetCenter(int offset, bool alongX)
    {
        int offsetTerm = (int)Math.Floor((float)offset/2);
        if (!alongX)
            return new(
                X * Helper.RenderedTileWidth + Helper.RenderedTileWidth / 2 - offsetTerm,
                Y * Helper.RenderedTileHeight + Helper.RenderedTileHeight / 2);
        else
            return new(
                X * Helper.RenderedTileWidth + Helper.RenderedTileWidth / 2,
                Y * Helper.RenderedTileHeight + Helper.RenderedTileHeight / 2 - offsetTerm);
    }
}