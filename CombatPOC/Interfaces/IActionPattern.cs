using System.Collections.Generic;

namespace CombatPOC.Interfaces;

public interface IActionPattern
{
    List<PositionComponent> Pattern {get; }
}