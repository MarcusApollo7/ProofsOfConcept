using CombatPOC.Entities;
using CombatPOC.Logic;
using CombatPOC.Managers;

namespace CombatPOC.Item;

public interface IWeapon
{
    Attack[] Attacks {get; }
    float[] WeaponStats {get; }
}


public class Weapon: ItemBase, IWeapon
{
    public Attack[] Attacks {get; set;}
    public float[] WeaponStats {get; }
    public Weapon()
    {
        WeaponStats = Constants.BasicSwordStats;
    }
    public void Equip()
    {
        
    }
}