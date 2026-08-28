using System.Collections.Generic;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public class BasicActionPattern: IActionPattern
{
    // Properties
    public List<PositionComponent> Pattern {get; }
    // Constructor
    public BasicActionPattern()
    {
        Pattern = [new(0,- 1)];
    }
}