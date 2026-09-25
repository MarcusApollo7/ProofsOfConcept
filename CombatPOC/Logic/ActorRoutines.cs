using System;
using System.Collections.Generic;
using CombatPOC.Entities;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using CombatPOC.Managers;

namespace CombatPOC.Logic;


public interface IActorRoutine: IComponent
{
    float BaseActionPoints {get; }
    // Methods
    List<Act> TakeTurn(Combatant self, List<Combatant> possibleTargets);
}
public class PlayerActor: IActorRoutine
{
    public int EntityID {get; }
    public float BaseActionPoints {get; }= 2;
    public int ComponentID {get; }
    public MoveAction Move {get => Actions.Get<MoveAction>(); }
    public List<Attack> Attacks {get=> Actions.GetAll<Attack>(); }
    private ActionList Actions = new();
    private List<Act> _selectedActs = [];
    // Properties
    public PlayerActor(int entityID)
    {
        EntityID = entityID;
        Actions.Add(new MoveAction(Constants.PlayerMaxMoves));
        Actions.Add(new BasicAttack());
    }
    public List<Act> TakeTurn(Combatant self, List<Combatant> possibleTargets)
    {
        return _selectedActs;
    }
    public void SetAct(Act act)
    {
        _selectedActs.Add(act);
    }

}
public class BasicEnemyActor: IActorRoutine
{
    public int EntityID {get; }
    public float BaseActionPoints {get; }
    public int ComponentID {get; } = EntityManager.CreateNewComponentID();
    private ActionList Actions {get; }
    private MoveAction Move {get => Actions.Get<MoveAction>(); }
    private List<Attack> Attacks {get=> Actions.GetAll<Attack>(); }
    public BasicEnemyActor(int maxMoves)
    {
        Actions = new([new MoveAction(maxMoves), new BasicAttack()]);
        BaseActionPoints = 2;
    }
    private CharacterDirection CheckForTurn(Combatant self, Combatant target)
    {
        return CheckForTurn(self.TileLocation, target.TileLocation);
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
    // Methods
    public List<Act> TakeTurn(Combatant self, List<Combatant> possibleTargets)
    {
        bool moved = false;
        bool attacked = false;
        float usedActionPoints = 0;
        List<Act> outputActs = [];
        Combatant target = FindTarget(self, possibleTargets);
        while (usedActionPoints < BaseActionPoints)
        {
            int targetDistance = (int)CombatManager._pathFinder.DistanceBetween(self, target);
            if (targetDistance == 1 && attacked == false)
            {
                CharacterDirection directionToTarget = CheckForTurn(self, target);
                if (self.ActorDirection != directionToTarget)
                    self.ActorDirection = directionToTarget;
                outputActs.Add(Attacks[0].Execute(self));
                usedActionPoints += Attacks[0].Cost;
                attacked = true;
            }
            else if (targetDistance > 1 && moved == false)
            {
                Move.SetTarget(target);
                Act moveAct = Move.Execute(self);
                outputActs.Add(moveAct);
                usedActionPoints += Move.Cost;
                self.TileLocation = moveAct.TilesActedUpon[^1];
                moved = true;
            }
            else
            {
                return outputActs;
            }
        }
        return outputActs;
    }
    public Combatant FindTarget(Combatant self, List<Combatant> potentialTargets)
    {
        Combatant closestActor = null;
        int minCombatantDistance = 10000;
        foreach(Combatant potentialTarget in potentialTargets)
        {
            int? distance = CombatManager._pathFinder.DistanceBetween(self.TileLocation, potentialTarget.TileLocation);
            if (distance != null && distance < minCombatantDistance && distance > 0)
            {
                closestActor = potentialTarget;
                minCombatantDistance = (int)distance;
            }
        }
        return closestActor;
    }

}