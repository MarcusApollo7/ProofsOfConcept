using System;
using System.Runtime.InteropServices;
using CombatPOC.Classes;
using CombatPOC.Enum;

namespace CombatPOC.Interfaces;

public interface ICombatant
{
    // Properties
    string Name {get; }
    float Health {get; set;}
    int Attack {get; set;}
    int Defense {get; set;}
    bool IsDowned {get; } // True if Combatant's health is less than or equal to 0
    IActor Actor {get; } // AI routine for the Combatant
    IAction[] Actions {get; } // Actions the Combatant can take
    CharacterDirection FacingDirection {get; set;}
    PositionComponent CombatantPosition {get; set;} // Where the character is on the map
    // Methods
    IAction GetAction()
    {
        return Actor.ChooseAction(Actions);
    }
    void ChangeHealth(float amount)
    {
        Health += amount;
    }
}

