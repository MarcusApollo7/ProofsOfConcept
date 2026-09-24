using System;
using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using Microsoft.Xna.Framework;
using CombatPOC.Entities;

namespace CombatPOC.Managers;

public class SelectableManager
{
    private List<ISelectable> _selectables = [];
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
        CombatManager._inputHandler.SetActiveEntity(e.Entity);
    }
}