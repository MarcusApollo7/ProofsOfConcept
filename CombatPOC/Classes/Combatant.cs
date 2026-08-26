using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public class Grunt : ICombatant
{
    // Properties
    public string Name {get; }
    public int Health {get; set;}
    public bool IsDowned {get; }
    public IActor Actor {get; }
    public IAction[] Actions {get; }
    public CharacterDirection FacingDirection {get; set;}
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