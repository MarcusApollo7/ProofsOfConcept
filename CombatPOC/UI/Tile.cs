using Microsoft.Xna.Framework;
using POCLibrary.Graphics;
using CombatPOC.Logic;
using CombatPOC.UI.Basics;
using static POCLibrary.Core;

namespace CombatPOC.UI;

public class Tile: ScreenElement
{
    public TileLocation TileLocation {get; }
    public Tile(TileLocation tileLocation, TextureRegion textureRegion): base(tileLocation.ToScreenPosition(), textureRegion)
    {
        Sprite.LayerDepth = Constants.MapDepth;
        TileLocation = tileLocation;
    }
    public override void OnHover()
    {
        if (SpriteRectangle.Contains(Input.Mouse.Position))
            Sprite.Color = Color.Green;
        else
            Sprite.Color = Color.White;
    }
}