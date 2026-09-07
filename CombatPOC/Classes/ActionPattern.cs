using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CombatPOC.Interfaces;
using CombatPOC.Logic;

namespace CombatPOC.Classes;

public class BasicActionPattern: IActionPattern
{
    // Properties
    public List<TileLocation> Pattern {get; }
    // Constructor
    public BasicActionPattern()
    {
        Pattern = [new(0,- 1)];
    }
}