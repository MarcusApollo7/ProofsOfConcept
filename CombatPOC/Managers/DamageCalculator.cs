using System;
using CombatPOC.Interfaces;

namespace CombatPOC.Managers;

public class DamageCalculator
{
    public float CalculateDamage(ICombatant attacker, ICombatant defender)
    {
        float attackDmg = attacker.Attack;
        float defendedDmg = defender.Defense;
        float dmgDealt = Math.Max(0, attackDmg - defendedDmg);
        return dmgDealt;
    }
}