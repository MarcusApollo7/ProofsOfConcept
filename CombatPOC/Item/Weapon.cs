using System.Collections.Generic;
using CombatPOC.Logic;

namespace CombatPOC.Item;

public interface IWeapon
{
    Attack[] Attacks {get; }
    Dictionary<string, float> WeaponStats {get; }
}


public class Weapon: ItemBase, IWeapon
{
    public Attack[] Attacks {get; set;}
    public Dictionary<string, float> WeaponStats {get; }
    public Weapon()
    {
        WeaponStats = Constants.BasicSwordStats;
    }
}