using CombatPOC.Interfaces;
using CombatPOC.Enum;

namespace CombatPOC.Classes;

public class Grunt : ICombatant
{
    // Properties
    public string Name {get; } 
    public float Health {get; set;} 
    public int Attack {get; set;}
    public int Defense {get; set;}
    public bool IsDowned {get; } 
    public IActor Actor {get; } 
    public IAction[] Actions {get; } 
    public CharacterDirection FacingDirection {get; set;} // Stores direction Combatant is facing
    public PositionComponent CombatantPosition {get; set;} = new(0,0);
    // Constructor
    public Grunt(string name, int health, IAction[] actions, CharacterDirection facingdirection)
    {
        Name = name;
        Health = health;
        IsDowned = false;
        Actor = new GruntActor();
        Actions = actions;
        FacingDirection = facingdirection;
    }
    // Methods
    public IAction GetAction()
    {
        return Actor.ChooseAction(Actions);
    }

}