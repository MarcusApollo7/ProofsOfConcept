using System;
using System.Linq;
using System.Collections.Generic;
using POCLibrary.Graphics;
using System.Diagnostics;

namespace CombatPOC.Paths;

public class AStar(Tilemap tilemap)
{
    public readonly Map _map = new(tilemap);
    Location current = null;
    Location start = new(0, 0, true);
    Location target = new(2, 2, true);
    List<Location> open = [];
    List<Location> closed = [];
    int g = 0;
    public void Initialize()
    {
        open.Add(start);
    }
    public List<Location> FindPath()
    {
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
                    Debug.WriteLine("New Tile!");
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
}