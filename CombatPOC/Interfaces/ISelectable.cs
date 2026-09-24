using System;
using CombatPOC.Enum;

namespace CombatPOC.Interfaces;


public delegate void OnSelectEventHandler<SelectEventArgs>(ISelectable sender, SelectEventArgs e);
public class SelectEventArgs(IEntity entity) : EventArgs
{
    public IEntity Entity {get; init; } = entity;
}
public interface ISelectable
{
    void Select();
}