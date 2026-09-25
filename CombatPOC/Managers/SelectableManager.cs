using System.Collections.Generic;
using CombatPOC.Interfaces;
using Microsoft.Xna.Framework;

namespace CombatPOC.Managers;

public class SelectableManager
{
    private List<ISelectable> _selectables = [];
    private IEntity ActiveEntity {get => CombatManager._inputHandler.ActiveEntity; set=> CombatManager._inputHandler.SetActiveEntity(value); }
    public void AddSelectable(ISelectable selectable)
    {
        _selectables.Add(selectable);
    }
    public void Update(GameTime gameTime)
    {
        foreach(ISelectable selectable in _selectables)
        {
            selectable.Select();
        }
    }
    public void DoOnSelect(ISelectable sender, SelectEventArgs e)
    {
        if (ActiveEntity != e.Entity)
            ActiveEntity = e.Entity;
    }
}