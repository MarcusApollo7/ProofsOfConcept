using System.Collections.Generic;
using CombatPOC.Interfaces;

namespace CombatPOC.Logic;

public class BasicActionPattern: IActionPattern
{
    // Properties
    public TileLocation[] Pattern {get; } = [new(0,- 1)];
    public float[] DmgModPerTile {get; }  = [1f];
}

public class CornerActionPattern: IActionPattern
{
    private TileLocation[] _pattern = [new(0, -1), new(1, -1), new(1, 0)];
    public TileLocation[] Pattern {get=> _pattern; }
    public float[] DmgModPerTile {get; }  = [.66f, .75f, .66f];
    private void FlipPattern()
    {
        List<TileLocation> output =[];
        foreach(TileLocation tile in _pattern)
        {
            output.Add(new(-tile.X, tile.Y));
        }
        _pattern = [.. output];
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