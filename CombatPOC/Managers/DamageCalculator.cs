using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Entities;
using CombatPOC.Interfaces;
using CombatPOC.Logic;

namespace CombatPOC.Managers;

public class DamageCalculator
{
    public void DealDamageToDefender(IAttacker attacker, Act act, IDefender defender)
    {
        if (act.Action is Attack attack)
        {   
            foreach(KeyValuePair<TileLocation, float> TileDmg in attack.DmgToTiles)
            {
                if (defender.TileLocation == TileDmg.Key)
                {
                    float dmg = attacker.AttackRating - defender.DefenseRating;
                    defender.ChangeHealth(dmg);
                }
            }
        }
        else
            throw new ArgumentException("act must be an Attack");
    }
}