using Microsoft.Xna.Framework;
using POCLibrary.Graphics;
using CombatPOC.Logic;
using CombatPOC.UI.Basics;
using static POCLibrary.Core;

namespace CombatPOC.UI;

public partial class Tile: ScreenElement
{
    public Tile(TileLocation tileLocation, TextureRegion textureRegion): base(tileLocation.ToScreenPosition(), textureRegion)
    {
        
    }
    public override void OnHover()
    {
        if (SpriteRectangle.Contains(Input.Mouse.Position))
            Sprite.Color = Color.Green;
        else
            Sprite.Color = Color.White;
    }
}