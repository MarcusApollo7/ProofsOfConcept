using System.Collections.Generic;
using CombatPOC.Classes;
using CombatPOC.Enum;
using CombatPOC.Entities;

namespace CombatPOC.Interfaces;

// Action Interface

public interface IAction
{
    // Properties
    string ActionName {get; }
    float BaseStrength {get; set; }
    List<ActionEffect> ActionEffects {get; }
    IActionPattern ActionPattern {get; }
    // Method
    Act Execute(Combatant source, int turnnum);
}