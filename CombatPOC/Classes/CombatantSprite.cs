using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using POCLibrary.Graphics;
using CombatPOC.Interfaces;
using POCLibrary.Interfaces;
using POCLibrary;
using POCLibrary.Input;

namespace CombatPOC.Classes;

public class CombatantSprite: IClickable, IHoverable
{
    public AnimatedSprite _animatedSprite;
    public Vector2 _screenPosition; // in pixels
    public Rectangle _spriteRectangle {get; set; }
    public bool Selected {get; set; } = false;
    public Color _selectedColor;
    public bool Hovered {get; set; } = false;
    // Methods
    public void Initialize(Vector2 initalPosition)
    {
        _screenPosition = initalPosition;
        _spriteRectangle = new((int)_screenPosition.X, (int)_screenPosition.Y, Helper._tileDim, Helper._tileDim);
    }
    public void LoadContent(TextureAtlas atlas, string spritename, Vector2 scale)
    {
        _animatedSprite = atlas.CreateAnimatedSprite(spritename);
        _animatedSprite.Scale = scale;
    }
    public void Update(GameTime gameTime, MouseInfo mouseInfo)
    {
        OnHover(mouseInfo);
        OnClick(mouseInfo);
        _animatedSprite.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        _animatedSprite.Draw(spriteBatch, _screenPosition);
    }
    public void UpdateRectangle()
    {
        _spriteRectangle = new((int)_screenPosition.X, (int)_screenPosition.Y, Helper._tileDim, Helper._tileDim);
    }
        public void OnClick(MouseInfo mouseInfo)
    {
        if (_spriteRectangle.Contains(mouseInfo.CurrentState.Position) && mouseInfo.WasButtonJustPressed(MouseButton.Left))
            Selected = true;
    }
    public void OnHover(MouseInfo mouseInfo)
    {
        if(_spriteRectangle.Contains(mouseInfo.CurrentState.Position))
        {
            Hovered = true;
            _animatedSprite.Color = Color.Green;
        }
        else
        {
            Hovered = false;
            _animatedSprite.Color = Color.White;
        }
    }
    public void ClearSelect()
    {
        Selected = false;
    }
    public void ClearHover()
    {
        Hovered = false;
        _animatedSprite.Color = Color.White;
    }
}