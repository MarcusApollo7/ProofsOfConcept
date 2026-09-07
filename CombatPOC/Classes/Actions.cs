using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using CombatPOC.Entities;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;
using POCLibrary;

namespace CombatPOC.Classes;

public class BasicAttack: IAction
{
    public string ActionName {get; }
    public float BaseStrength {get; set;}
    public IActionPattern ActionPattern {get; } = new BasicActionPattern();
    public List<ActionEffect> ActionEffects {get; } = [ActionEffect.physical];
    public Act Execute(Combatant source, int turnnum)
    {
        List<TileLocation> positionsactedupon = [];
        TileLocation positionTile = new(source._stats._tileLocation.X, source._stats._tileLocation.Y);
        foreach (TileLocation position in ActionPattern.Pattern)
        {
            positionsactedupon.Add(positionTile+position);
        }
        return new(this, source, positionsactedupon, turnnum, new((int)positionsactedupon[0].X, (int)positionsactedupon[0].Y, Helper._tileDim, Helper._tileDim));
    }

}