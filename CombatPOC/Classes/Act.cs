using System.Collections.Generic;
using Microsoft.Xna.Framework;
using CombatPOC.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using CombatPOC.Entities;
using CombatPOC.Logic;

namespace CombatPOC.Classes;

public record class Act(IAction action, Combatant actorcombatant, List<TileLocation> positionsactedupon, int turnnum, Rectangle spriteRectangle)
{
    private readonly IAction Action = action;
    private readonly Combatant ActorCombatant = actorcombatant;
    private readonly List<TileLocation> PositionsActedUpon = positionsactedupon;
    private readonly int TurnNum = turnnum;
    public Rectangle _spriteRectangle =spriteRectangle;
    public void Draw(SpriteBatch spriteBatch, Texture2D _actTexture)
    {
        spriteBatch.Draw(_actTexture, _spriteRectangle, Color.Red * 0.5f);
    }
    public List<TileLocation> ReturnPositions()
    {
        return PositionsActedUpon;
    }
    public virtual bool Equals(Act other)
    {
        if (other == null) return false;
        if (EqualityContract != other.EqualityContract) return false;
        return _spriteRectangle == other._spriteRectangle;
    }
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}