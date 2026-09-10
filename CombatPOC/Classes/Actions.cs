using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using CombatPOC.Entities;
using CombatPOC.Logic;
using Microsoft.Xna.Framework;
using POCLibrary;
using CombatPOC.Managers;

namespace CombatPOC.Classes;

public abstract class Attack: IAction
{
    public abstract string ActionName {get; }
    public float BaseStrength {get; set;} = 10;
    public abstract IActionPattern ActionPattern {get; }
    public abstract List<ActionEffect> ActionEffects {get; }
    public Act Execute(int CombatantID)
    {
        Combatant source = (Combatant)ActorManager.GetActor(CombatantID);
        List<TileLocation> positionsactedupon = [];
        TileLocation positionTile = source._stats._tileLocation;
        foreach (TileLocation position in ActionPattern.RotatePattern(source._stats.characterDirection))
        {
            TileLocation newPos = positionTile+position;
            positionsactedupon.Add(positionTile+position);
        }
        return new(this, source, positionsactedupon, TurnManager.TurnNum);
    }
}

public abstract class DirectAttack: Attack, IAction
{
    
}

public class BasicAttack: DirectAttack
{
    public override string ActionName {get; } = "Basic Attack";
    public override List<ActionEffect> ActionEffects {get; } = [ActionEffect.physical];
    public override IActionPattern ActionPattern {get; } = new BasicActionPattern();
    }

public class DiagonalAttack: DirectAttack
{
    public override string ActionName {get; } = "Diagonal Attack";
    public override List<ActionEffect> ActionEffects {get; } = [ActionEffect.physical];
    public override IActionPattern ActionPattern {get; } = new DiagonalActionPattern();
}