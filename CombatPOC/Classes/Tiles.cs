using CombatPOC.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.Classes;

public class BasicTile: ITile
{
    public bool Walkable {get; } = true;
    public bool Standable {get; } = true;
    public PositionComponent Position {get; }
    public Texture2D TileTexture {get; } = null;
    public BasicTile(int x, int y)
    {
        Position = new(x, y);
    }
}