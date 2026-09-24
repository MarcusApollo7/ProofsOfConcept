using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Graphics;
using POCLibrary.Input;
using CombatPOC.Logic;
using static POCLibrary.Core;
using CombatPOC.Managers;
using CombatPOC.Interfaces;
using System;
using System.Diagnostics;

namespace CombatPOC.UI;

public class Tile: IHoverable
{
    private TextureRegion Texture {get; }
    public Vector2 ScreenPosition {get; set;}
    public Rectangle _spriteRectangle {get => new((int)ScreenPosition.X, (int)ScreenPosition.Y, Helper.TileWidth, Helper.TileHeight); }
    public bool Hovered {get; set;} = false;
    public Tile(TileLocation tileLocation, TextureRegion textureRegion)
    {
        ScreenPosition = tileLocation.ToScreenPosition();
        Texture = textureRegion;
    }
    public void OnHover()
    {
        if (_spriteRectangle.Contains(Input.Mouse.CurrentState.Position))
            Hovered = true;
        else
            Hovered = false;
    }
    public void ClearHover()
    {
        Hovered = false;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (Hovered)
            Texture.Draw(spriteBatch, ScreenPosition, Color.Green, 0.0f, Vector2.Zero, Helper.Scale, SpriteEffects.None, 1.0f);
        else
            Texture.Draw(spriteBatch, ScreenPosition, Color.White, 0.0f, Vector2.Zero, Helper.Scale, SpriteEffects.None, 1.0f);
    }
    public void Update()
    {
        OnHover();
    }
}