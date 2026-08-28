using CombatPOC.Interfaces;

namespace CombatPOC.Classes;

public class TileMap
{
    int Width;
    int Height;
    ITile[,] Tiles;
    public TileMap(int width, int height)
    {
        Tiles = new ITile[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Tiles[i, j] = new BasicTile(i, j);
            }
        }
    }

}