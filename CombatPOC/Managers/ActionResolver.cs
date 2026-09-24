using System;
using CombatPOC.Classes;
using CombatPOC.Enum;
using CombatPOC.Entities;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;
using CombatPOC.Interfaces;

namespace CombatPOC.Managers;

public sealed class ActionResolver
{
    public ActionResolver(DamageCalculator damageCalculator = null)
    {
        
    }
    public void ResolveAction(Act act, Party combatants)
    {
        foreach(ActionEffect ae in act.Action.ActionEffects)
        {
            foreach(TileLocation position in act.TilesActedUpon)
            {
                switch (ae)
                {
                    case ActionEffect.physical:
                        ResolvePhysical(act.ActorCombatant, position, combatants);
                    break;
                    default:
                        ResolveDefault();
                    break;
                }
            }
        }
    }
    public void ResolvePhysical(IActor attacker, TileLocation position, Party combatants)
    {
        return;
    }
    public void ResolveDefault()
    {
        return;
    }
}