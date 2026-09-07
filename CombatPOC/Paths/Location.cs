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
}