using System;
using System.Collections.Generic;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public sealed record Act(IAction action, ICombatant actorcombatant, List<PositionComponent> positionsactedupon, int turnnum)
{
    public readonly IAction Action = action;
    public readonly ICombatant ActorCombatant = actorcombatant;
    public readonly List<PositionComponent> PositionsActedUpon = positionsactedupon;
    public readonly int TurnNum = turnnum;  
}