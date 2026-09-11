using System.Diagnostics;
using POCLibrary.Graphics;

namespace CombatPOC.Paths;

public class Map
{
    Location[,] _map;
    int x_dim {get => _map.GetLength(0); }
    int y_dim {get => _map.GetLength(1); }

    public Map(Tilemap tilemap)
    {
        _map = new Location[tilemap.Rows, tilemap.Columns];
        for (int i = 0; i < tilemap.Rows; i++)
        {
            for (int j = 0; j < tilemap.Columns; j++)
            {
                _map[i, j] = new(j, i, true);
            }
        }
    }
    public Location GetLocation(int x_index, int y_index)
    {
        if (x_dim > x_index && y_dim > y_index && x_index >= 0 && y_index >= 0)
            return _map[y_index, x_index];
        else 
            return null;
    }
}