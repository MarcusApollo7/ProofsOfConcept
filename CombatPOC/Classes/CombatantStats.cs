using System.Collections.Generic;
using CombatPOC.Interfaces;
using CombatPOC.stats_skills;
using CombatPOC.Enum;

namespace CombatPOC.Classes;
public class CombatantStats(float attack, float defense, float health)
{
    public Stat Attack = new(attack);
    public Stat Defense = new(defense);
    public Stat MaxHealth = new(health);
    public float CurHealth = health;
    public bool IsDowned => CurHealth > 0; // True if Combatant's health is greater than 0
    public void ChangeCurHealth(float amount)
    {
        CurHealth += amount;
    }

}