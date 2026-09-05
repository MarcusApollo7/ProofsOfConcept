using CombatPOC.Interfaces;
using CombatPOC.Enum;
using CombatPOC.stats_skills;
using CombatPOC.Managers;
using POCLibrary.Graphics;
using POCLibrary.Interfaces;
using POCLibrary;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System;
using POCLibrary.Input;

namespace CombatPOC.Classes;

public class Combatant : IClickable, IHoverable
{
    public string Name;
    public Stat Attack;
    public Stat Defense;
    public Stat MaxHealth;
    public float CurHealth;
    public bool IsDowned => CurHealth > 0;
    public AnimatedSprite _sprite;
    public Vector2 _positionScreen; // Position on the screen in Pixels
    public Rectangle _spriteRectangle {get; set; }
    public IActor _actor;
    public IAction[] _actions;
    public bool Selected {get; set; } = false;
    public bool Hovered {get; set; } = false;
    public Color _selectedColor;
    public TeamEnum _team;
    public Combatant(float attack, float defense, float health)
    {
        Attack = new(attack);
        Defense = new(defense);
        MaxHealth = new(health);
        CurHealth = health;
    }
    public IAction GetAction()
    {
        return _actor.ChooseAction(_actions);
    }
    public void JumpToNewPosition(Vector2 newposition)
    {
        
        int snapedX = (int)Math.Floor(newposition.X / Helper._tileDim) * Helper._tileDim;
        int snapedY = (int)Math.Floor(newposition.Y / Helper._tileDim) * Helper._tileDim;
        _positionScreen = new(snapedX, snapedY);
        UpdateRectangle();
    }
    public void Move(CharacterDirection characterDirection)
    {
        switch (characterDirection)
        {
            case CharacterDirection.Up:
                _positionScreen.Y -= Helper._tileDim;
                break;
            case CharacterDirection.Right:
                _positionScreen.X += Helper._tileDim;
                break;
            case CharacterDirection.Down:
                _positionScreen.Y += Helper._tileDim;
                break;
            case CharacterDirection.Left:
                _positionScreen.X -= Helper._tileDim;
                break;
        }
        UpdateRectangle();
    }
    public void UpdateRectangle()
    {
        _spriteRectangle = new((int)_positionScreen.X, (int)_positionScreen.Y, Helper._tileDim, Helper._tileDim);
    }
    public bool CheckSamePosition(Vector2 position)
    {
        return _positionScreen == position;
    }
    public void ChangeCurHealth(float amount)
    {
        CurHealth += amount;
    }
    // basic methods
    public void Initialize(Vector2 initalPosition)
    {
        _positionScreen = initalPosition;
        _spriteRectangle = new((int)_positionScreen.X, (int)_positionScreen.Y, Helper._tileDim, Helper._tileDim);
    }
    public void LoadSpriteFromAtlas(TextureAtlas atlas, string spriteName, Vector2 scale)
    {
        _sprite = atlas.CreateAnimatedSprite(spriteName);
        _sprite.Scale = scale;
    }
    public void Update(GameTime gameTime, MouseInfo mouseInfo)
    {
        OnHover(mouseInfo);
        OnClick(mouseInfo);
        _sprite.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        if (Hovered == true)
            _sprite.Color = Color.Green;
        _sprite.Draw(spriteBatch, _positionScreen);
    }
    public void OnClick(MouseInfo mouseInfo)
    {
        if (_spriteRectangle.Contains(mouseInfo.CurrentState.Position) && mouseInfo.WasButtonJustPressed(MouseButton.Left))
            Selected = true;
    }
    public void OnHover(MouseInfo mouseInfo)
    {
        if(_spriteRectangle.Contains(mouseInfo.CurrentState.Position))
            Hovered = true;
    }
    public void ClearSelect()
    {
        Selected = false;
    }
    public void ClearHover()
    {
        Hovered = false;
        _sprite.Color = Color.White;
    }

}


