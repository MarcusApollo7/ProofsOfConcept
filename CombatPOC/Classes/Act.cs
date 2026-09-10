using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CombatPOC.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using CombatPOC.Entities;
using CombatPOC.Logic;
using System;
using POCLibrary;
using System.Diagnostics;

namespace CombatPOC.Classes;

public record class Act(IAction action, Combatant actorcombatant, List<TileLocation> positionsactedupon, int turnnum)
{
    private readonly int TurnNum = turnnum;
    public readonly ActLogic actLogic = new(action, actorcombatant, positionsactedupon);
    public readonly ActSprite actSprite = new(positionsactedupon);
    public IAction Action {get => actLogic.GetAction(); }
    public Combatant ActorCombatant {get => actLogic.GetActorCombatant(); }
    public TileLocation[] tilesActedUpon {get => actLogic.GetTileLocations(); }
    public virtual bool Equals(Act other)
    {
        if (other == null) return false;
        if (EqualityContract != other.EqualityContract) return false;
        return Action == other.Action
            && ActorCombatant == other.ActorCombatant
            && tilesActedUpon == other.tilesActedUpon
            && TurnNum == other.TurnNum;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public record class ActLogic(IAction action, Combatant actorcombatant, List<TileLocation> positionsactedupon)
{
    private readonly IAction Action = action;
    private readonly Combatant ActorCombatant = actorcombatant;
    private readonly TileLocation[] tilesActedUpon = positionsactedupon.ToArray();
    public IAction GetAction()
    {
        return Action;
    }
    public Combatant GetActorCombatant()
    {
        return ActorCombatant;
    }
    public TileLocation[] GetTileLocations()
    {
        return tilesActedUpon;
    }

}

public record class ActSprite
{
    private readonly Rectangle[] _rectangles;
    private int tileNumber {get => _rectangles.Length; }
    public ActSprite(List<TileLocation> tileLocations)
    {
        _rectangles = new Rectangle[tileLocations.Count];
        for(int i = 0; i < tileLocations.Count; i++)
        {
            _rectangles[i] = new(tileLocations[i].X * Helper._tileDim, tileLocations[i].Y * Helper._tileDim, Helper._tileDim, Helper._tileDim);
        }
    }
    public void Draw(SpriteBatch spriteBatch, Texture2D _whiteRectangle)
    {
        for(int i = 0; i < tileNumber; i++)
        {
            spriteBatch.Draw(_whiteRectangle, _rectangles[i], Color.Red * 0.5f);
        }
    }
}