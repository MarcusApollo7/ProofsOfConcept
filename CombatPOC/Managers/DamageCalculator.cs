using System;
using CombatPOC.Classes;

namespace CombatPOC.Managers;

public class DamageCalculator
{
    public float CalculateDamage(Combatant attacker, Combatant defender)
    {
        float attackDmg = attacker.Attack.Value;
        float defendedDmg = defender.Defense.Value;
        float dmgDealt = Math.Max(0, attackDmg - defendedDmg);
        return dmgDealt;
    }
}