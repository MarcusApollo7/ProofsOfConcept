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
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;
using CombatPOC.Managers;
using CombatPOC.Paths;
using System.Collections.Generic;
using System.Linq;
using POCLibrary.Interfaces;

namespace CombatPOC.Entities;

public abstract class Combatant: IActor
{
    public int ActorID {get; } = ActorManager.CreateNewActorID();
    public CombatantStats _stats;
    public CombatantSprite _sprite;
    public Act _proposedAct;
    public Combatant(float attack, float defense, float health, string spritename, TileLocation tileLocation)
    {
        _stats = new(ActorID, attack, defense, health, tileLocation);
        Vector2 spritePosition = new(tileLocation.X * Helper._tileDim, tileLocation.Y * Helper._tileDim);
        _sprite = new(spritename, spritePosition);
    }
    public void ProposeAction(IAction action)
    {
        _proposedAct = _stats.ProposeAct(action);
    }
    public void UnProposeAction()
    {
        _proposedAct = null;
    }
    public IAction GetAction()
    {
        return _stats.ChooseAction();
    }
    public void JumpToNewPosition(TileLocation newposition)
    {
        _sprite._screenPosition = new(newposition.X * Helper._tileDim, newposition.Y * Helper._tileDim);
        _stats._tileLocation = newposition;
        _sprite.UpdateRectangle();
        if (_proposedAct != null)
            ProposeAction(_proposedAct.Action);
    }
    public void ChangeDirection(CharacterDirection direction)
    {
        if (_stats.characterDirection != direction)
        {
            _stats.characterDirection = direction;
            _sprite.UpdateRectangle();
            if (_proposedAct != null)
                ProposeAction(_proposedAct.Action);
        }
        else
            return;

    }
    public Combatant FindTarget()
    {
        Combatant closestCombatant = null;
        int minCombatantDistance = 10000;
        foreach(Combatant combatant in CombatManager._combatants)
        {
            int distance = CombatManager.DistanceBetweenCombatants(this, combatant);
            if (distance < minCombatantDistance && distance != 0)
            {
                closestCombatant = combatant;
            }
        }
        return closestCombatant;
    }
    public abstract void CheckForPlayerInput(InputManager input);
    public abstract List<TileLocation> GetMovesFromPathfinder(Combatant targetCombatant);
    public void Move()
    {
        switch (_stats.characterDirection)
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
    public void Initialize()
    {
        ActorManager.AddActor(this);
        _sprite.Initialize();
    }
    public void LoadSpriteFromAtlas(TextureAtlas atlas, Vector2 scale)
    {
        _sprite._animatedSprite = atlas.CreateAnimatedSprite(_sprite._spriteName);
        _sprite._animatedSprite.Scale = scale;
    }
    public void Draw(SpriteBatch spriteBatch)
    {
        _sprite.Draw(spriteBatch);
        _proposedAct?.actSprite.Draw(spriteBatch);
    }
}

public class Hero: Combatant
{
    public Hero(float attack, float defense, float health, string spritename, TileLocation tileLocation) : base(attack, defense, health, spritename, tileLocation)
    {
        _stats._team = TeamEnum.Player;
        _stats._actor = new PlayerActor(ActorID);
    }
    public override List<TileLocation> GetMovesFromPathfinder(Combatant targetCombatant)
    {
        return [];
    }
     public override void CheckForPlayerInput(InputManager input)
    {
        if (input.Keyboard.WasKeyJustPressed(Keys.A))
        {
            ChangeDirection(CharacterDirection.Left);
        }
        if (input.Keyboard.WasKeyJustPressed(Keys.W))
        {
            ChangeDirection(CharacterDirection.Up);
        }
        if (input.Keyboard.WasKeyJustPressed(Keys.S))
        {
            ChangeDirection(CharacterDirection.Down);
        }
        if (input.Keyboard.WasKeyJustPressed(Keys.D))
        {
            ChangeDirection(CharacterDirection.Right);
        }
        if (input.Keyboard.WasKeyJustPressed(Keys.Z))
        {
            IAction proposedAction = GetAction();
            ProposeAction(proposedAction);  
        }
        if (input.Keyboard.WasKeyJustPressed(Keys.X))
        {
            UnProposeAction();
        }

        if (input.Mouse.WasButtonJustReleased(MouseButton.Right))
        {
            Vector2 mousePosition = new(input.Mouse.CurrentState.Position.X, input.Mouse.CurrentState.Position.Y);
            JumpToNewPosition(new((int)Helper.ConvertToTiles(mousePosition).X, (int)Helper.ConvertToTiles(mousePosition).Y));
            _sprite.ClearSelect();
        }
        if (input.Mouse.IsButtonDown(MouseButton.Left))
        {
            Vector2 mousePosition = new(input.Mouse.CurrentState.Position.X, input.Mouse.CurrentState.Position.Y);
            JumpToNewPosition(new((int)Helper.ConvertToTiles(mousePosition).X, (int)Helper.ConvertToTiles(mousePosition).Y));
        }
    }

}

public class Grunt: Combatant
{
    public Grunt(float attack, float defense, float health, string spritename, TileLocation tileLocation) : base(attack, defense, health, spritename, tileLocation)
    {
        _stats._team = TeamEnum.Enemy;
        _stats._actor = new GruntActor(ActorID);
    }
    public override void CheckForPlayerInput(InputManager input)
    {
        
    }
    public override List<TileLocation> GetMovesFromPathfinder(Combatant targetCombatant)
    {
        List<TileLocation> potentialTargets = [];
        foreach(Attack attack in _stats._actor.Actions.Cast<Attack>()) //ACTOR IS NULL WHEN CALLED
        {
            potentialTargets.AddRange(attack.DetermineAttackableTiles(targetCombatant));
        }
        int distance = 100000;
        TileLocation target = _stats._tileLocation;
        foreach(TileLocation tile in potentialTargets)
        {
            if (CombatManager.DistanceBetweenTiles(_stats._tileLocation, tile) < distance && distance > 0)
            {
                target = tile;
            }
        }
        List<Location> Moves = AStar.FindPath(_stats._tileLocation.ToWalkableLocation(), target.ToWalkableLocation());
        int maxSteps = Math.Min(Moves.Count - 1, _stats.TilesPerMove + 100000);
        List<TileLocation> movesFromPathfinder = [];
        for(int i = 0; i <= maxSteps; i++)
        {
            movesFromPathfinder.Add(Moves[i].ToTileLocation());
            
        }
        return movesFromPathfinder;   

    }
}