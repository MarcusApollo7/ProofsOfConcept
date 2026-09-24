using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CombatPOC.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using CombatPOC.Entities;
using CombatPOC.Logic;
using System;
using POCLibrary;
using System.Diagnostics;
using CombatPOC.Managers;
using CombatPOC.UI;

namespace CombatPOC.Entities;

public record class Act: IRenderable
{ 
    public readonly int TurnNum;
    public IAction Action {get; }
    public Combatant ActorCombatant {get; }
    public List<TileLocation> TilesActedUpon {get; }
    private Color ActColor;
    private readonly Rectangle[] _rectangles;
    private int tileNumber {get => _rectangles.Length; }
    public Act(IAction action, Combatant actorcombatant, List<TileLocation> positionsactedupon, int turnnum, Color color)
    {
        Action = action;
        ActorCombatant = actorcombatant;
        TilesActedUpon = positionsactedupon;
        TurnNum = turnnum;
        _rectangles = new Rectangle[positionsactedupon.Count];
        for(int i = 0; i < positionsactedupon.Count; i++)
        {
            _rectangles[i] = new(positionsactedupon[i].X * Helper.TileWidth, positionsactedupon[i].Y * Helper.TileHeight, Helper.TileWidth, Helper.TileHeight);
        }
        ActColor = color;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        for(int i = 0; i < tileNumber; i++)
        {
            spriteBatch.Draw(CombatManager._UIManager._whiteRectangle, _rectangles[i], ActColor * 0.4f);
        }
    }
    public void Update(GameTime gameTime)
    {
        return;
    }
    public virtual bool Equals(Act other)
    {
        if (other == null) return false;
        if (EqualityContract != other.EqualityContract) return false;
        return Action == other.Action
            && ActorCombatant == other.ActorCombatant
            && TilesActedUpon == other.TilesActedUpon
            && TurnNum == other.TurnNum;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
