using System;

namespace CombatPOC.Logic;


public delegate void OnEquipEventHandler<EquipEventArgs>(IEquipable sender, EquipEventArgs e);

public class EquipEventArgs(Attack[] actions) : EventArgs
{
    public Attack[] ActionsOnEquip {get; init; } = actions;
}
public interface IEquipable
{
    void Equip();
}