using CombatPOC.Interfaces;
using CombatPOC.Enum;
using CombatPOC.Classes;
using CombatPOC.Logic;
using POCLibrary;
using POCLibrary.Graphics;
using POCLibrary.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace CombatPOC.Entities;

public abstract class Combatant
{
    public abstract string Name {get; }
    public CombatantStats _stats = new(10, 10, 10, [new BasicAttack()]);
    public CombatantSprite _sprite = new();
    public IAction GetAction()
    {
        return _stats.ChooseAction();
    }
    public void JumpToNewPosition(Vector2 newposition)
    {
        int snapedX = (int)Math.Floor(newposition.X / Helper._tileDim) * Helper._tileDim;
        int snapedY = (int)Math.Floor(newposition.Y / Helper._tileDim) * Helper._tileDim;
        _sprite._screenPosition = new(snapedX, snapedY);
        _sprite.UpdateRectangle();
    }
    public void Move(CharacterDirection characterDirection)
    {
        switch (characterDirection)
        {
            case CharacterDirection.Up:
                _sprite._screenPosition.Y -= Helper._tileDim;
                break;
            case CharacterDirection.Right:
                _sprite._screenPosition.X += Helper._tileDim;
                break;
            case CharacterDirection.Down:
                _sprite._screenPosition.Y += Helper._tileDim;
                break;
            case CharacterDirection.Left:
                _sprite._screenPosition.X -= Helper._tileDim;
                break;
        }
        _sprite.UpdateRectangle();
    }
    public void ChangeCurHealth(float amount)
    {
        _stats.CurHealth += amount;
    }
    // basic methods
    public void Initialize(Vector2 initalPosition)
    {
        _sprite.Initialize(initalPosition);
    }
    public void LoadSpriteFromAtlas(TextureAtlas atlas, string spriteName, Vector2 scale)
    {
        _sprite._animatedSprite = atlas.CreateAnimatedSprite(spriteName);
        _sprite._animatedSprite.Scale = scale;
    }
    public void Update(GameTime gameTime, MouseInfo mouseInfo)
    {
        _sprite.Update(gameTime, mouseInfo);
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch);
    }
}

public class Hero(string name): Combatant
{
    public override string Name {get; } = name; 
}