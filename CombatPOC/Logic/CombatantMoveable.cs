using System;
using System.Collections.Generic;
using CombatPOC.Entities;
using CombatPOC.Interfaces;
using CombatPOC.Managers;

namespace CombatPOC.Logic;

public class CombatantMoveable: IMoveable, IComponent
{
    public int EntityID {get; }
    public int ComponentID {get; }
    public TileLocation TileLocation {get; set;}
    private int _tilesPerMove;
    public int TilesPerMove {get => _tilesPerMove; set => _tilesPerMove = value; }
    public CombatantMoveable(int entityID, TileLocation location)
    {
        EntityID = entityID;
        ComponentID = EntityManager.CreateNewComponentID();
        TileLocation = location;

    }
    public void Move(TileLocation tileLocation)
    {
        if (CombatManager._EntityManager.GetEntity(EntityID) is Combatant combatant)
        {
            combatant.Move(tileLocation);
        }
    }
    
    public event OnSetPathHandler<PathEventArgs> OnSetPath;

    public void MovePath(Act path)
    {
        IEntity entity = CombatManager._EntityManager.GetEntity(EntityID);
        OnSetPath?.Invoke(this, new() { Entity = entity, PathAct = path});
    }
    public List<TileLocation> GetMovesFromPathfinder(TileLocation end)
    {
        TileLocation start = TileLocation;
        List<TileLocation> Moves = CombatManager._pathFinder.FindPath(start, end);
        int maxSteps = Math.Min(Moves.Count - 1, TilesPerMove + 100000);
        List<TileLocation> movesFromPathfinder = [];
        for(int i = 0; i <= maxSteps; i++)
        {
            movesFromPathfinder.Add(Moves[i]);
        }
        return movesFromPathfinder;   
    }
}