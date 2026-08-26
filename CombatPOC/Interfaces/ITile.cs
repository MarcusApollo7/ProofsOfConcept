using Microsoft.Xna.Framework.Graphics;

namespace CombatPOC.Interfaces;

public interface ITile
{
    bool Walkable {get; set;} // determines if tile can be walked over
    bool Standable {get; set;} // determines if tile can be stood on
    Texture2D TileTexture {get; set;} // stores the texture of the tile
}