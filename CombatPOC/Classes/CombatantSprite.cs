using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using POCLibrary.Graphics;
using CombatPOC.Interfaces;
using POCLibrary.Interfaces;
using POCLibrary;
using POCLibrary.Input;
using CombatPOC.Entities;
using System.Diagnostics;

namespace CombatPOC.Classes;

public class CombatantSprite(string spritename, Vector2 spritePosition): IClickable, IHoverable, IRenderable
{
    public string Name {get => _spriteName;}
    public string _spriteName = spritename;
    public AnimatedSprite _animatedSprite;
    public Vector2 _screenPosition = spritePosition; // in pixels
    public Rectangle _spriteRectangle {get; set; }
    public bool Selected {get; set; } = false;
    public Color _selectedColor = Color.Green;
    public bool Hovered {get; set; } = false;
    // Methods
    public void Initialize()
    {
        _spriteRectangle = new((int)_screenPosition.X, (int)_screenPosition.Y, Helper._tileDim, Helper._tileDim);
    }
    public void LoadContent(TextureAtlas atlas, string spritename, Vector2 scale)
    {
        _animatedSprite = atlas.CreateAnimatedSprite(spritename);
        _animatedSprite.Scale = scale;
    }
    public void Update(GameTime gameTime)
    {
        _animatedSprite.Update(gameTime);
    }
    public void CheckClickHover(MouseInfo mouseInfo)
    {
        OnHover(mouseInfo);
        OnClick(mouseInfo);
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
            _animatedSprite.Color = _selectedColor;
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