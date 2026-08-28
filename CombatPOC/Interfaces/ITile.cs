using System;
using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.Interfaces;

public interface ITile
{
    bool Walkable {get; } // determines if tile can be walked over
    bool Standable {get; } // determines if tile can be stood on
    PositionComponent Position {get; } // keeps the position of the tile
    Texture2D TileTexture {get; } // stores the texture of the tile
}

public class PositionComponent(int x, int y)
{
    public int X = x;
    public int Y = y;
    public static PositionComponent operator +(PositionComponent a, PositionComponent b)
    {
        if (a == null || b == null)
        {
            throw new ArgumentNullException("Operands cannot be null");
        }
        return new PositionComponent(a.X+b.X, a.Y+b.Y);
    }
}