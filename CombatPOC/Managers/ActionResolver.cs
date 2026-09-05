using System;
using CombatPOC.Classes;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using Microsoft.Xna.Framework;

namespace CombatPOC.Managers;

public sealed class ActionResolver
{
    private DamageCalculator _damageCalculator;
    public ActionResolver(DamageCalculator damageCalculator = null)
    {
        _damageCalculator = damageCalculator ?? new DamageCalculator();
    }
    public void ResolveAction(Act act, Party combatants)
    {
        foreach(ActionEffect ae in act.action.ActionEffects)
        {
            foreach(Vector2 position in act.PositionsActedUpon)
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
    public void ResolvePhysical(Combatant attacker, Vector2 position, Party combatants)
    {
        foreach(Combatant combatant in combatants)
        {
            if (combatant.CheckSamePosition(position))
            {
                float dmg = _damageCalculator.CalculateDamage(attacker, combatant);
                combatant.ChangeCurHealth(-dmg); 
            }
        }
    }
    public void ResolveDefault()
    {
        return;
    }
}