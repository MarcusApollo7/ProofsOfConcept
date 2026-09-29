using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Graphics;
using CombatPOC.Logic;
using static POCLibrary.Core;

namespace CombatPOC.UI;

public class Tile: ScreenElement
{
    public Tile(TileLocation tileLocation, TextureRegion textureRegion): base(tileLocation.ToScreenPosition(), textureRegion)
    {
        
    }
}