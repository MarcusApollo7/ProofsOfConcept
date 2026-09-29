using System;
using System.Collections.Generic;
using CombatPOC.Entities;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using CombatPOC.Logic.Paths;
using CombatPOC.Managers;
using Microsoft.Xna.Framework;

namespace CombatPOC.Logic;


public interface IActorRoutine
{
    float BaseActionPoints {get; }
    MoveAction Move {get; }
    List<Act> TakeTurn(List<PlayerCombatant> possibleTargets);
}
public class BasicEnemyActor: IActorRoutine
{
    public NonPlayerCombatant _self;
    public IPathfinder Pathfinder {get=> _self.Pathfinder; }
    public float BaseActionPoints {get; }
    private ActionList Actions {get; }
    public MoveAction Move {get => Actions.Get<MoveAction>(); }
    public List<Attack> Attacks {get=> Actions.GetAll<Attack>(); }
    public BasicEnemyActor(NonPlayerCombatant self, int maxMoves)
    {
        _self = self;
        Actions = new([new MoveAction(maxMoves), new BasicAttack()]);
        BaseActionPoints = 2;
    }
    // Methods
    private CharacterDirection CheckForTurn(BaseCombatant target)
    {
        return CheckForTurn(_self.TileLocation, target.TileLocation);
    }
    private CharacterDirection CheckForTurn(TileLocation selfLocation, TileLocation targetLocation)
    {
        if (selfLocation.X == targetLocation.X + 1 && selfLocation.Y == targetLocation.Y)
        {
            return CharacterDirection.Left;
        }
        else if (selfLocation.X == targetLocation.X - 1 && selfLocation.Y == targetLocation.Y)
        {
            return CharacterDirection.Right;
        }
        else if (selfLocation.X == targetLocation.X && selfLocation.Y == targetLocation.Y + 1)
        {
            return CharacterDirection.Up;
        }
        else if (selfLocation.X == targetLocation.X && selfLocation.Y == targetLocation.Y - 1)
        {
            return CharacterDirection.Down;
        }
        else
            return CharacterDirection.Up;
    }
    public List<Act> TakeTurn(List<PlayerCombatant> possibleTargets)
    {
        bool moved = false;
        bool attacked = false;
        float usedActionPoints = 0;
        List<Act> outputActs = [];
        BaseCombatant target = FindTarget(possibleTargets);
        while (usedActionPoints < BaseActionPoints)
        {
            int targetDistance = _self.Pathfinder.DistanceBetween(_self, target);
            if (targetDistance == 1 && attacked == false)
            {
                CharacterDirection directionToTarget = CheckForTurn(target);
                if (_self.ActorDirection != directionToTarget)
                    _self.ActorDirection = directionToTarget;
                outputActs.Add(Attacks[0].Execute(_self));
                usedActionPoints += Attacks[0].Cost;
                attacked = true;
            }
            else if (targetDistance > 1 && moved == false)
            {
                ;
                MoveAct moveAct = new(Move, _self, Pathfinder.FindPath(_self, target), Color.Green);
                outputActs.Add(moveAct);
                usedActionPoints += Move.Cost;
                _self.TileLocation = moveAct.TilesActedUpon[^1];
                moved = true;
            }
            else
            {
                return outputActs;
            }
        }
        return outputActs;
    }
    private BaseCombatant FindTarget(List<PlayerCombatant> potentialTargets)
    {
        PlayerCombatant closestActor = null;
        int minCombatantDistance = int.MaxValue;
        foreach(PlayerCombatant potentialTarget in potentialTargets)
        {
            int distance = _self.Pathfinder.DistanceBetween(_self, potentialTarget);
            if (distance < minCombatantDistance && distance > 0)
            {
                closestActor = potentialTarget;
                minCombatantDistance = distance;
            }
        }
        return closestActor;
    }
}