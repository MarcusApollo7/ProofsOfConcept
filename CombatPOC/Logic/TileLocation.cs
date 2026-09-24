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
    public Vector2 ToScreenPosition()
    {
        return new(X * Helper.TileWidth, Y * Helper.TileHeight);
    }
    public Vector2 GetCenter()
    {
        return new(X * Helper.TileWidth + Helper.TileWidth/2, Y * Helper.TileHeight + Helper.TileHeight/2);
    }
}