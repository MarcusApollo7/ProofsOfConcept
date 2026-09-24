using System.Diagnostics;
using CombatPOC.Paths;
using POCLibrary.Graphics;

namespace CombatPOC.Logic;

public class Map
{
    TileLocation[,] _map;
    public int Xdim {get => _map.GetLength(1); }
    public int Ydim {get => _map.GetLength(0); }

    public Map(Tilemap tilemap)
    {
        _map = new TileLocation[tilemap.Rows, tilemap.Columns];
        for (int i = 0; i < tilemap.Rows; i++)
        {
            for (int j = 0; j < tilemap.Columns; j++)
            {
                bool walkable;
                if (tilemap.GetTileWalkable(j, i))
                {
                    walkable = true;
                }
                else
                    walkable = false;
                _map[i, j] = new(j, i, walkable);
            }
        }
    }
    public Location GetLocation(int x_index, int y_index)
    {
        if (Xdim > x_index && Ydim > y_index && x_index >= 0 && y_index >= 0)
        {
            TileLocation location = _map[y_index, x_index];
            if (location.Walkable)
            {
                return Location.TileLocationToLocation(location);
            }
            else
                return null;
        }
        else 
            return null;
    }
}