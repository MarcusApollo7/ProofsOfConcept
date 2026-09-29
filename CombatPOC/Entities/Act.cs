using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CombatPOC.Logic;
using CombatPOC.Managers;
using CombatPOC.UI;
using CombatPOC.Enum;
using CombatPOC.UI.Animation;
using System;
using System.Linq;
using System.Diagnostics;

namespace CombatPOC.Entities;

public abstract record class Act: IRenderable
{ 
    public Timer _animationTimer;
    public readonly int TurnNum;
    public IAction Action {get; }
    public BaseCombatant Actor {get; }
    public List<TileLocation> TilesActedUpon {get; set;}
    private Color ActColor;
    private readonly Rectangle[] _rectangles;
    public int TileNumber {get => _rectangles.Length; }
    public Act(IAction action, BaseCombatant actorcombatant, List<TileLocation> positionsactedupon, Color color)
    {
        Action = action;
        Actor = actorcombatant;
        TilesActedUpon = positionsactedupon;
        _rectangles = new Rectangle[positionsactedupon.Count];
        for(int i = 0; i < positionsactedupon.Count; i++)
        {
            _rectangles[i] = new(positionsactedupon[i].X * Helper.TileWidth, positionsactedupon[i].Y * Helper.TileHeight, Helper.TileWidth, Helper.TileHeight);
        }
        ActColor = color;
    }
    public void UpdateRectangles()
    {
        for(int i = 0; i < TileNumber; i++)
        {
            _rectangles[i] = new(TilesActedUpon[i].X * Helper.TileWidth, TilesActedUpon[i].Y * Helper.TileHeight, Helper.TileWidth, Helper.TileHeight);
        }
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        for(int i = 0; i < TileNumber; i++)
        {
            spriteBatch.Draw(CombatManager._CombatUI._whiteRectangle, _rectangles[i], ActColor * 0.4f);
        }
    }
    public void StartAnimationTimer()
    {
        _animationTimer.Start();
    }
    public abstract bool Animate(IAnimatable animatable, GameTime gameTime);
    public virtual bool Equals(Act other)
    {
        if (other == null) return false;
        if (EqualityContract != other.EqualityContract) return false;
        return Action == other.Action
            && Actor == other.Actor
            && TilesActedUpon == other.TilesActedUpon
            && TurnNum == other.TurnNum;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public record class MoveAct: Act
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

public record class AttackAct: Act
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