using System.Collections.Generic;
using CombatPOC.Enum;
using CombatPOC.Entities;

namespace CombatPOC.Logic;

// Action Interface

public interface IAction
{
    // Properties
    string ActionName {get; }
    float Cost {get; }
    List<ActionEffect> ActionEffects {get; }
    // Method
    Act Execute(Combatant source);
}