using CombatPOC.Logic;

namespace CombatPOC.Paths;

public record Location(int x, int y, bool walkable)
{
    public bool Walkable = walkable;
    public int X = x;
    public int Y = y;
    public int F;
    public int G;
    public int H;
    public Location Parent;
    public TileLocation ToTileLocation()
    {
        return new(X, Y);
    }
    public static Location TileLocationToLocation(TileLocation tileLocation)
    {
        return new(tileLocation.X, tileLocation.Y, tileLocation.Walkable);
    }
}