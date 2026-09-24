using System.Collections.Generic;
using CombatPOC.Entities;

namespace CombatPOC.Classes;

public record class BattleState
{
    public Resolutions Resolution;
    public int TurnNum;
    public List<Combatant> _friends;
    public List<Combatant> _foes; 
    public BattleState(Resolutions resolutions, int turnNum, List<Combatant> friends, List<Combatant> foes)
    {
        Resolution = resolutions;
        TurnNum = turnNum;
        _friends = friends;
        _foes = foes;
    }
    public List<Combatant> GetOpposingSide(TeamEnum side)
    {
        if (side == TeamEnum.Enemy)
            return _friends;
        else
            return _foes;
    }
}