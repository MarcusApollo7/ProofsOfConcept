using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public class BasicActionPattern: IActionPattern
{
    // Properties
    public List<Vector2> Pattern {get; }
    // Constructor
    public BasicActionPattern()
    {
        Pattern = [new(0,- 1)];
    }
}