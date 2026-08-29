using CombatPOC.Interfaces;
using CombatPOC.Enum;

namespace CombatPOC.Classes;

public class PlayerCharacter: ICombatant
{
    public string Name {get; } = "PlayerCharacter";
    public float Health {get; set; } = 100;
    public int Attack {get; set; } = 10;
    public int Defense {get; set; } = 5;
    public bool IsDowned {get; } = false;
    public IActor Actor {get; } = new PlayerActor();
    public IAction[] Actions {get; } = [new BasicAttack()];
    public CharacterDirection FacingDirection { get; set; } = CharacterDirection.Up;
    public PositionComponent CombatantPosition {get; set;} = new(0,0);
}

public class Grunt(string name, int health, IAction[] actions, CharacterDirection facingdirection) : ICombatant
{
    // Properties
    public string Name { get; } = name;
    public float Health { get; set; } = health;
    public int Attack {get; set;}
    public int Defense {get; set;}
    public bool IsDowned { get; } = false;
    public IActor Actor { get; } = new GruntActor();
    public IAction[] Actions { get; } = actions;
    public CharacterDirection FacingDirection { get; set; } = facingdirection;
    public PositionComponent CombatantPosition {get; set;} = new(1,0);

}