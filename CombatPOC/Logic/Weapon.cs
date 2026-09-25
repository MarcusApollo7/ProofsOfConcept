namespace CombatPOC.Logic;

public interface IWeapon: IEquipable
{
    Attack[] Attacks {get; }
    float[] WeaponStats {get; }
}

public class Sword: IWeapon
{
    public Attack[] Attacks {get; } = [new BasicAttack(), new SwordHeavyAttack()];
    public float[] WeaponStats {get; } = Constants.BasicSwordStats;
    public event OnEquipEventHandler<EquipEventArgs> OnEquip;
    public void Equip()
    {
        EquipEventArgs args = new(Attacks);
        OnEquip.Invoke(this, args);
    }
}