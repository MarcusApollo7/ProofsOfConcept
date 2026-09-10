using CombatPOC.Paths;

namespace CombatPOC.Logic;

public record class TileLocation(int x, int y)
{
    public int X {get; set; } = x;
    public int Y {get; set; } = y;
    public static TileLocation operator +(TileLocation a, TileLocation b)
    {
        return new(a.X + b.X, a.Y + b.Y); // returns a component-wise sum
    }
    public Location ToWalkableLocation()
    {
        return new(X, Y, true);
    }
}