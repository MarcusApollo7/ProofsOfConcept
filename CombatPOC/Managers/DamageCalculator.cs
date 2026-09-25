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
            for(int i = 0; i < attack.DmgToTiles.Length; i++)
            {
                Debug.WriteLine(defender.TileLocation);
                if (defender.TileLocation == act.TilesActedUpon[i])
                {
                    float dmg = (attacker.AttackRating - defender.DefenseRating) * attack.DmgToTiles[i];
                    defender.ChangeHealth(dmg);
                }
            }
        }
        else
            throw new ArgumentException("act must be an Attack");
    }
}