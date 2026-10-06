using System.Collections.Generic;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using CombatPOC.Entities;
using Microsoft.Xna.Framework;
using System;
using CombatPOC.Logic.Paths;

namespace CombatPOC.Logic;

// Action Interface

public interface IAction
{
    // Properties
    string ActionName {get; }
    float Cost {get; }
}

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
    private Color MoveColor = Constants.MoveColor;
    public List<ActionEffect> ActionEffects {get; } = [];
    public MoveAction(int maxmoves)
    {
        _maxMoves = maxmoves;
    }
    public MoveAct GetFullMove(BaseCombatant source, IPathfinder pathfinder)
    {
        return GetFullMove(source, source.TileLocation, pathfinder);
    }
    public MoveAct GetFullMove(BaseCombatant mover, TileLocation origin, IPathfinder pathfinder)
    {
        List<TileLocation> travelableTiles = pathfinder.FindLocationsWithinDistance(_maxMoves, origin);
        return new(this, mover, travelableTiles, MoveColor);
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
    public AttackAct Execute(BaseCombatant source)
    {
        List<TileLocation> positionsactedupon = [];
        TileLocation positionTile = source.TileLocation;
        foreach (TileLocation position in ActionPattern.RotatePattern(source.ActorDirection))
        {
            TileLocation newPos = positionTile+position;
            positionsactedupon.Add(newPos);
        }
        return new(this, source, positionsactedupon, Constants.AttackColor);
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