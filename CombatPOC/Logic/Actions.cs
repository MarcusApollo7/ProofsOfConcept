using System.Collections.Generic;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using CombatPOC.Entities;
using Microsoft.Xna.Framework;
using CombatPOC.Managers;
using System;
using System.Diagnostics;

namespace CombatPOC.Logic;

public class ActionList
{
    private List<IAction> _actions;
    public ActionList()
    {
        _actions = [];
    }
    public ActionList(List<IAction> actions)
    {
        _actions = actions;
    }
    public T Get<T>() where T : IAction
    {
        foreach (IAction action in _actions)
        {
            if (action is T output)
            {
                return output;
            }
        }
        throw new InvalidOperationException($"Component of type {typeof(T).Name} was not found.");
    }
    public List<T> GetAll<T>() where T: IAction
    {
        List<T> output = [];
        foreach (IAction action in _actions)
        {
            if (action is T outputAction)
            {
                output.Add(outputAction);
            }
        }
        return output;
        throw new InvalidOperationException($"No components of type {typeof(T).Name} were found.");
    }
    public void Add(IAction action)
    {
        if (!_actions.Contains(action))
            _actions.Add(action);
    }
}
public class MoveAction: IAction
{
    public string ActionName {get; } = "Move";
    public float Cost {get; } = 1;
    private int _maxMoves;
    private Combatant targetCombatant;
    private Color MoveColor = Constants.MoveColor;
    public List<ActionEffect> ActionEffects {get; } = [];
    public MoveAction(int maxmoves)
    {
        _maxMoves = maxmoves;
    }
    public Act GetFullMove(Combatant source)
    {
        return GetFullMove(source, source.TileLocation);
    }
    public Act GetFullMove(Combatant mover, TileLocation origin)
    {

        List<TileLocation> travelableTiles = CombatManager._pathFinder.FindLocationsWithinDistance(_maxMoves, origin);
        return new(this, mover, travelableTiles, TurnManager.TurnNum, MoveColor);
    }
    public Act Execute(Combatant source)
    {
        if (targetCombatant != null)
        {
            return MoveToTarget(source, targetCombatant);
        }
        else
        {
            throw new NullReferenceException("targetCombatant not set to reference");
        }
    }
    private Act MoveToTarget(Combatant source, Combatant target)
    {
        List<TileLocation> path = CombatManager._pathFinder.FindPath(source, target);
        return new(this, source, path[..^1], TurnManager.TurnNum, MoveColor);
    }
    public void SetTarget(Combatant target)
    {
        targetCombatant = target;
    }
}

public abstract class Attack: IAction
{
    public abstract string ActionName {get; }
    public abstract float Cost {get; }
    public abstract float BaseStrength {get; }
    public abstract IActionPattern ActionPattern {get; }
    public abstract List<ActionEffect> ActionEffects {get; }
    public int Count {get => ActionPattern.Count;}
    public float[] DmgToTiles { get=> ActionPattern.DmgModPerTile; }
    public Act Execute(Combatant source)
    {
        List<TileLocation> positionsactedupon = [];
        TileLocation positionTile = source.TileLocation;
        foreach (TileLocation position in ActionPattern.RotatePattern(source.ActorDirection))
        {
            TileLocation newPos = positionTile+position;
            positionsactedupon.Add(newPos);
        }
        return new(this, source, positionsactedupon, TurnManager.TurnNum, Constants.AttackColor);
    }    
}

public abstract class DirectAttack: Attack
{
    
}

public class BasicAttack: DirectAttack
{
    public override string ActionName {get; } = "Basic Attack";
    public override float Cost {get; } = Constants.BasicAttackCost;
    public override float BaseStrength {get; } = Constants.BasicAttackStrength;
    public override List<ActionEffect> ActionEffects {get; } = [ActionEffect.physical];
    public override IActionPattern ActionPattern {get; } = new BasicActionPattern();
}

public class SwordHeavyAttack: DirectAttack
{
    public override string ActionName {get; } = "Sword Heavy Attack";
    public override float Cost {get; } = Constants.HeavyAttackCost;
    public override float BaseStrength {get; } = Constants.HeavyAttackStrength;
    public override List<ActionEffect> ActionEffects {get; } = [ActionEffect.physical];
    public override IActionPattern ActionPattern {get; } = new BasicActionPattern();
}