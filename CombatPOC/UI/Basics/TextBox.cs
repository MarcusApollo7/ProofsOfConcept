using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static POCLibrary.Core;

namespace CombatPOC.UI.Basics;

public class TextBox: ScreenElement
{
    private SpriteFont Font {get; set; }
    private Color TextColor {get; set; } = Color.Black;
    private Color BackgroundColor {get; set; } = Color.White;
    private int BoxWidth {get; }
    private int BoxHeight {get; }
    public override Rectangle SpriteRectangle => new((int)ScreenPosition.X, (int)ScreenPosition.Y, BoxWidth, BoxHeight);
    public TextBox(Vector2 position, string text, SpriteFont font, int width, int height): base(position, text)
    {
        Font = font;
        BoxWidth = width;
        BoxHeight = height;
    }
    public TextBox(Vector2 position, string text, SpriteFont font): base(position, text)
    {
        Font = font;
        BoxWidth = (int)font.MeasureString(SpriteName).X;
        BoxHeight = (int)font.MeasureString(SpriteName).Y;

    }
    public override void OnHover()
    {
        if (SpriteRectangle.Contains(Input.Mouse.Position))
        {
            TextColor = Color.White;
            BackgroundColor = Color.Black;
        }
        else
        {
            TextColor = Color.Black;
            BackgroundColor = Color.White;
        }
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(Font, SpriteName, ScreenPosition, TextColor);
    }
}