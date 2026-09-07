using System;
using CombatPOC.Classes;
using CombatPOC.Enum;
using CombatPOC.Entities;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;

namespace CombatPOC.Managers;

public sealed class ActionResolver
{
    public ActionResolver(DamageCalculator damageCalculator = null)
    {
        
    }
    public void ResolveAction(Act act, Party combatants)
    {
        foreach(ActionEffect ae in act.action.ActionEffects)
        {
            foreach(TileLocation position in act.ReturnPositions())
            {
                switch (ae)
                {
                    case ActionEffect.physical:
                        ResolvePhysical(act.actorcombatant, position, combatants);
                    break;
                    default:
                        ResolveDefault();
                    break;
                }
            }
        }
    }
    public void ResolvePhysical(Combatant attacker, TileLocation position, Party combatants)
    {
        foreach(Combatant combatant in combatants)
        {
            if (combatant._stats.CheckSamePosition(position))
            {
                float dmg = DamageCalculator.CalculateDamage(attacker, combatant);
                combatant.ChangeCurHealth(-dmg); 
            }
        }
    }
    public void ResolveDefault()
    {
        return;
    }
}