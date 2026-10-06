using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CombatPOC.Logic;
using CombatPOC.Managers;
using CombatPOC.UI;
using CombatPOC.Enum;
using CombatPOC.UI.Animation;
using System;
using CombatPOC.UI.Basics;

namespace CombatPOC.Entities;

public abstract class Act: ScreenElement
{ 
    public Timer _animationTimer;
    public readonly int TurnNum;
    public IAction Action {get; }
    public BaseCombatant Actor {get; }
    public List<TileLocation> TilesActedUpon {get; set;}
    private Color ActColor;
    private Rectangle[] _rectangles;
    public int TileNumber {get => TilesActedUpon.Count; }
    public Act(IAction action, BaseCombatant actorcombatant, List<TileLocation> positionsactedupon, Color color)
    {
        Action = action;
        Actor = actorcombatant;
        TilesActedUpon = positionsactedupon;
        _rectangles = new Rectangle[positionsactedupon.Count];
        UpdateRectangles();
        ActColor = color;
    }
    public void UpdateRectangles()
    {
        if (_rectangles.Length != TilesActedUpon.Count)
            _rectangles = new Rectangle[TilesActedUpon.Count];

        for(int i = 0; i < TileNumber; i++)
        {
            float left = TilesActedUpon[i].X * Helper.RenderedTileWidth;
            float top = TilesActedUpon[i].Y * Helper.RenderedTileHeight;
            float right = (TilesActedUpon[i].X + 1) * Helper.RenderedTileWidth;
            float bottom = (TilesActedUpon[i].Y + 1) * Helper.RenderedTileHeight;
            int x = (int)MathF.Floor(left);
            int y = (int)MathF.Floor(top);
            _rectangles[i] = new(
                x,
                y,
                (int)MathF.Ceiling(right) - x,
                (int)MathF.Ceiling(bottom) - y);
        }
    }
    public override void Draw(SpriteBatch spriteBatch)
    {
        UpdateRectangles();
        for(int i = 0; i < TileNumber; i++)
        {
            spriteBatch.Draw(CombatManager._CombatUI._whiteRectangle, _rectangles[i], null, ActColor * 0.4f, 0f, new(0, 0), SpriteEffects.None, Constants.ActDepth);
        }
    }
    public void StartAnimationTimer()
    {
        _animationTimer.Start();
    }
    public abstract bool Animate(IAnimatable animatable, GameTime gameTime);
}

public class MoveAct: Act
{
    public MoveAction Move {get; set;}
    public MoveAct(IAction action, BaseCombatant actorcombatant, List<TileLocation> positionsactedupon, Color color): base(action, actorcombatant, positionsactedupon, color)
    {
        if (action is MoveAction move)
            Move = move;
        else
            throw new ArgumentException("MoveAct action must be a Move");
        _animationTimer = new(1f);
        
    }
    public override bool Animate(IAnimatable animatable, GameTime gameTime)
    {
        if (TilesActedUpon.Count >= 1)
        {
            if (_animationTimer.Update(gameTime))
            {
                animatable.MoveToNewLocation(TilesActedUpon[0]);
                TilesActedUpon.RemoveAt(0);
                _animationTimer.Start();  
            }
            return false; 
        } 
        else
        {
            _animationTimer.Stop();
            return true;
        }
    }
}

public class AttackAct: Act
{
    public Attack Attack {get; set;}
    public AttackAct(IAction action, BaseCombatant actorcombatant, List<TileLocation> positionsactedupon, Color color): base(action, actorcombatant, positionsactedupon, color)
    {
        if (action is Attack attack)
            Attack = attack;
        else
            throw new ArgumentException("AttackAct action must be an Attack");
        _animationTimer = new(1f);
    }
    public override bool Animate(IAnimatable animatable, GameTime gameTime)
    {
        return _animationTimer.Update(gameTime);
    }
    public void RotateAttack(CharacterDirection direction)
    {
        for(int i = 0; i < TileNumber; i++)
            TilesActedUpon[i] = Attack.ActionPattern.RotatePattern(direction)[i] + Actor.TileLocation;
        UpdateRectangles();
    }
}