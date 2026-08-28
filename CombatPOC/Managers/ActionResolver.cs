using System;
using CombatPOC.Classes;
using CombatPOC.Enum;
using CombatPOC.Interfaces;

namespace CombatPOC.Managers;

public sealed class ActionResolver
{
    private DamageCalculator _damageCalculator;
    public ActionResolver(DamageCalculator damageCalculator = null)
    {
        _damageCalculator = damageCalculator ?? new DamageCalculator();
    }
    public void ResolveAction(Act act)
    {
        foreach(ActionEffect ae in act.action.ActionEffects)
        {
            foreach(PositionComponent position in act.PositionsActedUpon)
            {
                switch (ae)
                {
                    case ActionEffect.physical:
                        ResolvePhysical(act.actorcombatant, position);
                    break;
                    default:
                        ResolveDefault();
                    break;
                }
            }
        }
    }
    public void ResolvePhysical(ICombatant attacker, PositionComponent position)
    {
        
        Party Combatants = TurnManager.Combatants();
        foreach(ICombatant combatant in Combatants)
        {
            if (combatant.CombatantPosition == position)
            {
                float dmg = _damageCalculator.CalculateDamage(attacker, combatant);
                combatant.ChangeHealth(dmg); 
            }
        }
    }
    public void ResolveDefault()
    {
        return;
    }
}