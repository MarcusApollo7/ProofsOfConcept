using System;
using CombatPOC.Entities;

namespace CombatPOC.Managers;

public class DamageCalculator
{
    public static float CalculateDamage(Combatant attacker, Combatant defender)
    {
        float attackDmg = attacker._stats.Attack.Value;
        float defendedDmg = defender._stats.Defense.Value;
        float dmgDealt = Math.Max(0, attackDmg - defendedDmg);
        return dmgDealt;
    }
}