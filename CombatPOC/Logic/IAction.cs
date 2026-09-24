using System.Collections.Generic;
using CombatPOC.Classes;
using CombatPOC.Enum;
using CombatPOC.Entities;
using CombatPOC.Interfaces;

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