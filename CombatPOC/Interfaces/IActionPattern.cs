using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace CombatPOC.Interfaces;

public interface IActionPattern
{
    List<Vector2> Pattern {get; }
}