using System;
using CombatPOC.Logic;
using CombatPOC.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Graphics;
using POCLibrary.Input;
using static POCLibrary.Core;

namespace CombatPOC.UI;

public interface IScreenElement: IRenderable
{
    Vector2 ScreenPosition {get; set; }
    string SpriteName {get; }
    Sprite Sprite {get; }
    Rectangle SpriteRectangle {get => new((int)ScreenPosition.X, (int)ScreenPosition.Y, Helper.TileWidth, Helper.TileHeight ); }
    void OnHover();
    void Update(GameTime gameTime);
}
public abstract partial class ScreenElement: IScreenElement
{
    public Vector2 ScreenPosition {get; set; }
    public string SpriteName {get; set; }
    public Sprite Sprite {get; }
    public Rectangle SpriteRectangle {get => new((int)ScreenPosition.X, (int)ScreenPosition.Y, Helper.TileWidth, Helper.TileHeight ); }
    public ScreenElement(Vector2 position, string name, string atlasName)
    {
        ScreenPosition = position;
        SpriteName = name;
        TextureAtlas atlas = TextureAtlas.FromFile(Content, atlasName);
        Sprite = atlas.CreateAnimatedSprite(SpriteName);
        Sprite.Scale = Helper.Scale;
    }
    public ScreenElement(Vector2 position, TextureRegion textureRegion)
    {
        ScreenPosition = position;
        SpriteName = $"Tile {position}";
        Sprite = new()
        {
            Region = textureRegion,
            Scale = Helper.Scale
        };
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, ScreenPosition);
    }
    public virtual void Update(GameTime gameTime)
    {
        OnHover();
    }
    public virtual void OnHover()
    {
        if (SpriteRectangle.Contains(Input.Mouse.Position))
            Sprite.Color = Color.Green;
        else
            Sprite.Color = Color.White;
    }
}