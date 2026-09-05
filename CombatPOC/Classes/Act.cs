using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public sealed record Act(IAction action, Combatant actorcombatant, List<Vector2> positionsactedupon, int turnnum)
{
    public readonly IAction Action = action;
    public readonly Combatant ActorCombatant = actorcombatant;
    public readonly List<Vector2> PositionsActedUpon = positionsactedupon;
    public readonly int TurnNum = turnnum;  
}