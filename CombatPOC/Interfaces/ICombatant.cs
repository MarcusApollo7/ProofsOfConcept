using System.Runtime.InteropServices;

namespace CombatPOC.Interfaces;

public interface ICombatant
{
    // Properties
    string Name {get; }
    int Health {get; set;}
    bool IsDowned {get; }
    IActor Actor {get; }
    IAction[] Actions {get; }
    CharacterDirection FacingDirection {get; set;}
    // Methods
    IAction GetAction();
}

public enum CharacterDirection
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3,
}