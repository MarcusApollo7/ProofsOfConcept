using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Graphics;
using static POCLibrary.Core;

namespace CombatPOC.UI.Basics;

public interface IScreenElement: IRenderable
{
    Vector2 ScreenPosition {get; set; }
    string SpriteName {get; }
    Sprite Sprite {get; }
    Rectangle SpriteRectangle {get => new((int)ScreenPosition.X, (int)ScreenPosition.Y, Helper.TileWidth, Helper.TileHeight ); }
    void Update(GameTime gameTime);
}
public abstract class ScreenElement: IScreenElement
{
    public Vector2 ScreenPosition {get; set; }
    public string SpriteName {get; set; }
    public Sprite Sprite {get; }
    public virtual Rectangle SpriteRectangle {get => new((int)ScreenPosition.X, (int)ScreenPosition.Y, Helper.TileWidth, Helper.TileHeight ); }
    public ScreenElement()
    {
        Sprite = new();
    }
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
    public ScreenElement(Vector2 position, string text)
    {
        ScreenPosition = position;
        SpriteName = text;
    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        Sprite.Draw(spriteBatch, ScreenPosition);
    }
    public virtual void Update(GameTime gameTime)
    {
        OnHover();
    }
    public virtual void OnHover()
    {
        
    }
}