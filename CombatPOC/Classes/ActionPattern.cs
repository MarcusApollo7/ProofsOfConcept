using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CombatPOC.Interfaces;
using CombatPOC.Logic;

namespace CombatPOC.Classes;

public class BasicActionPattern: IActionPattern
{
    // Properties
    public TileLocation[] Pattern {get; }
    // Constructor
    public BasicActionPattern()
    {
        Pattern = [new(0,- 1)];
    }
}

public class DiagonalActionPattern: IActionPattern
{
    public TileLocation[] Pattern {get;}
    public DiagonalActionPattern()
    {
        Pattern = [new(-1, -1), new(1, -1)];
    }
}