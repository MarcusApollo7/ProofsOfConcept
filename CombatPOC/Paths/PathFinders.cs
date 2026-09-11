using System;
using System.Linq;
using System.Collections.Generic;
using POCLibrary.Graphics;
using CombatPOC.Entities;
using System.Diagnostics;
using CombatPOC.Managers;
using CombatPOC.Classes;

namespace CombatPOC.Paths;

public class AStar
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
    public static List<Location> FindPath(Location Start, Location target)
    {
        open.Add(Start);
        Debug.WriteLine($"Start: {Start.X}, {Start.Y}");
        while (open.Count > 0)
        {
            var lowest = open.Min(l => l.F);
            current = open.First(l => l.F == lowest);

            // add the current square to the closed list
            closed.Add(current);
            // remove it from the open list
            open.Remove(current);
            if (closed.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                return closed;
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
        return null;
    }
    public static int ComputeHScore(int x, int y, int targetX, int targetY)
    {
        return Math.Abs(targetX - x) + Math.Abs(targetY - y);
    }
    public static List<Location> GetAdjacentTiles(int x, int y)
    {
        
        return
            [
                _map.GetLocation(x, y - 1),
                _map.GetLocation(x, y + 1),
                _map.GetLocation(x - 1, y),
                _map.GetLocation(x + 1, y)
            ];
    }
    public static void ResetPathFinder()
    {
        current = null;
        open = [];
        closed = [];
        g = 0;
    }
    public static void ResetMap()
    {
        
    }
}