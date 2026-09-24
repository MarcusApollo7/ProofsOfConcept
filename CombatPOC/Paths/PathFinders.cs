using System;
using System.Linq;
using System.Collections.Generic;
using POCLibrary.Graphics;
using CombatPOC.Entities;
using CombatPOC.Managers;
using CombatPOC.Logic;

namespace CombatPOC.Paths;

public interface IPathfinder
{
    int? DistanceBetween(TileLocation start, TileLocation end);
    List<TileLocation> FindPath(TileLocation start, TileLocation end);
    List<TileLocation> FindLocationsWithinDistance(int distance, TileLocation start);

}

public class AStar: IPathfinder
{
    public static Map _map;
    static Location current = null;
    static List<Location> open = [];
    static List<Location> closed = [];
    static int g = 0;
    public void Initialize(Tilemap tilemap)
    {
        _map = new(tilemap);
    }
    public int? DistanceBetween(Combatant source, Combatant target)
    {
        return DistanceBetween(source.TileLocation, target.TileLocation);
    }
    public int? DistanceBetween(TileLocation start, TileLocation end)
    {
        List<TileLocation> path = FindPath(start, end);
        int? output = null;
        if (path != null)
        {
            output = path.Count - 1;
            ResetPathFinder();
            return output;
        }
        return output;
    }
    public List<TileLocation> FindPath(Combatant source, Combatant target)
    {
        return FindPath(source.TileLocation, target.TileLocation);
    }
    public List<TileLocation> FindPath(TileLocation Start, TileLocation target)
    {
        open.Add(ConvertTileLocationToLocation(Start));
        while (open.Count > 0)
        {
            var lowest = open.Min(l => l.F);
            current = open.First(l => l.F == lowest);

            // add the current square to the closed list
            closed.Add(current);
            // remove it from the open list
            open.Remove(current);
            if (closed.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
            {
                List<TileLocation> output = [];
                foreach(Location tile in closed)
                {
                    output.Add(tile.ToTileLocation());
                }
                ResetPathFinder();
                return output;
            }
            List<Location> adjacentTiles = GetAdjacentTiles(current.X, current.Y);
            foreach(Location tile in adjacentTiles)
            {
                if (tile == null)
                    continue;
                else if (closed.Contains(tile))
                    continue;
                else if (!open.Contains(tile))
                {
                    tile.G = g;
                    tile.H = ComputeHScore(tile.X, tile.Y, target.X, target.Y);
                    tile.F = tile.H + tile.G;
                    tile.Parent = current;
                    
                    open.Insert(0, tile);
                }
            }
            g++;
        }
        ResetPathFinder();
        return null;
    }
    public List<TileLocation> FindLocationsWithinDistance(int distance, Combatant combatant)
    {
        return FindLocationsWithinDistance(distance, combatant.TileLocation);
    }
    public List<TileLocation> FindLocationsWithinDistance(int distance, TileLocation start)
    {
        List<TileLocation> reachableTiles = [];
        Queue<TileLocation> searchQueue = new();
        List<TileLocation> visitedLocations = [];
        searchQueue.Enqueue(start);
        visitedLocations.Add(start);
        while (searchQueue.Count > 0)
        {
            TileLocation currentLocation = searchQueue.Dequeue();
            if (!reachableTiles.Contains(currentLocation))
                reachableTiles.Add(currentLocation);
            if (!visitedLocations.Contains(currentLocation))
                visitedLocations.Add(currentLocation);
            foreach(TileLocation tile in GetAdjacentTiles(currentLocation))
            {
                if (CombatManager._pathFinder.DistanceBetween(start, tile) <= distance && !visitedLocations.Contains(tile))
                {
                    searchQueue.Enqueue(tile);
                }
            }
        }
        return reachableTiles;
    }
    public static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Math.Abs(targetX - x) + Math.Abs(targetY - y);
    }
    public List<Location> GetAdjacentTiles(int x, int y)
    {
        
        return
            [
                _map.GetLocation(x, y - 1),
                _map.GetLocation(x, y + 1),
                _map.GetLocation(x - 1, y),
                _map.GetLocation(x + 1, y)
            ];
    }
    public List<TileLocation> GetAdjacentTiles(TileLocation tileLocation)
    {
        List<TileLocation> output = [];
        foreach(Location location in GetAdjacentTiles(tileLocation.X, tileLocation.Y))
        {
            if (location != null)
                output.Add(location.ToTileLocation());
        }
        return output;
    }
    public void ResetPathFinder()
    {
        current = null;
        open = [];
        closed = [];
        g = 0;
    }
    public void ResetMap(Tilemap newTileMap)
    {
        _map = new(newTileMap);
    }
    public Location ConvertTileLocationToLocation(TileLocation location)
    {
        return new(location.x, location.y, location.Walkable);
    }
}