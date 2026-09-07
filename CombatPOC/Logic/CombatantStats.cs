using CombatPOC.Interfaces;
using CombatPOC.stats_skills;

namespace CombatPOC.Logic;
public class CombatantStats(float attack, float defense, float health, IAction[] actions)
{
    public Stat Attack = new(attack);
    public Stat Defense = new(defense);
    public Stat MaxHealth = new(health);
    public float CurHealth = health;
    public bool IsDowned => CurHealth > 0; // True if Combatant's health is greater than 0
    public IActor _actor;
    public IAction[] _actions = actions;
    public TeamEnum _team = TeamEnum.Player;
    public TileLocation _tileLocation;
    public IAction ChooseAction()
    {
        return _actor.ChooseAction(_actions);
    }
    public bool CheckSamePosition(TileLocation position)
    {
        return _tileLocation == position;
    }

}