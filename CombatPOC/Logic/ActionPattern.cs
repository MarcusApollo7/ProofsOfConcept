using CombatPOC.Interfaces;

namespace CombatPOC.Logic;

public class BasicActionPattern: IActionPattern
{
    // Properties
    public TileLocation[] Pattern {get; }
    public float[] DmgModPerTile {get; }
    // Constructor
    public BasicActionPattern()
    {
        Pattern = [new(0,- 1)];
        DmgModPerTile = [1f];
    }
}

public class DiagonalActionPattern: IActionPattern
{
    // Properties
    public TileLocation[] Pattern {get;}
    public float[] DmgModPerTile {get; }
    public DiagonalActionPattern()
    {
        Pattern = [new(-1, -1), new(1, -1)];
        DmgModPerTile = [.5f, .5f];
    }
}