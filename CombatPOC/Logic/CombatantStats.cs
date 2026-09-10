using CombatPOC.Classes;
using CombatPOC.Entities;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using CombatPOC.Managers;
using CombatPOC.stats_skills;

namespace CombatPOC.Logic;
public class CombatantStats(int CombatantID, float attack, float defense, float health, TileLocation tileLocation)
{
    public int _combatantID = CombatantID;
    public CharacterDirection characterDirection = CharacterDirection.Up;
    public Stat Attack = new(attack);
    public Stat Defense = new(defense);
    public Stat MaxHealth = new(health);
    public float CurHealth = health;
    public int TilesPerMove = 3;
    public bool IsDowned => CurHealth > 0; // True if Combatant's health is greater than 0
    public IActorRotuine _actor;
    public TeamEnum _team = TeamEnum.Player;
    public TileLocation _tileLocation = tileLocation;
    public IAction ChooseAction()
    {
        return _actor.ChooseAction();
    }
    public bool CheckSamePosition(TileLocation position)
    {
        return _tileLocation == position;
    }
    public Act ProposeAct(IAction proposedAct)
    {
        return proposedAct.Execute(_combatantID);
    }
    
}