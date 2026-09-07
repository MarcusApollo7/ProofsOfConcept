using System.Collections.Generic;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;

namespace CombatPOC.Interfaces;

public interface IActionPattern
{
    List<TileLocation> Pattern {get; }
}