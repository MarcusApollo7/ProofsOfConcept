using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using POCLibrary.Input;
using POCLibrary.Interfaces;

namespace POCLibrary.Graphics;

public class Tile: IHoverable
{
    public int TilesetIndex;
    public TextureRegion Texture;
    public Vector2 _screenPosition {get; set;}
    public Rectangle _spriteRectangle {get; set;}
    public bool Hovered {get; set;}

    public void OnHover(MouseInfo mouseInfo)
    {
        if (_spriteRectangle.Contains(mouseInfo.CurrentState.Position))
            Hovered = true;
        else
            Hovered = false;
    }
    public void ClearHover()
    {
        Hovered = false;
    }
    public void Draw(SpriteBatch spriteBatch, Vector2 scale)
    {
        if (Hovered)
            Texture.Draw(spriteBatch, _screenPosition, Color.Green, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 1.0f);
        else
            Texture.Draw(spriteBatch, _screenPosition, Color.White, 0.0f, Vector2.Zero, scale, SpriteEffects.None, 1.0f);
    }
    public void Update(MouseInfo mouseInfo)
    {
        OnHover(mouseInfo);
    }

}