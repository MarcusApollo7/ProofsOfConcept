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
            if (distance < minCombatantDistance)
            {
                closestCombatant = combatant;
            }
        }
        return closestCombatant;
    }
    public abstract void CheckForPlayerInput(InputManager input);
    public abstract void GetMovesFromPathfinder(Combatant targetCombatant);
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
    public void Initialize(CombatantActorRoutine actorRoutine)
    {
        ActorManager.AddActor(this);
        _sprite.Initialize();
        _stats._actor = actorRoutine;
    }
    public void LoadSpriteFromAtlas(TextureAtlas atlas, Vector2 scale)
    {
        _sprite._animatedSprite = atlas.CreateAnimatedSprite(_sprite._spriteName);
        _sprite._animatedSprite.Scale = scale;
    }
    public void Update(GameTime gameTime, MouseInfo mouseInfo)
    {
        _sprite.Update(gameTime, mouseInfo);
    }
    public void Draw(SpriteBatch spriteBatch, Texture2D texture2D)
    {
        _sprite.Draw(spriteBatch);
        _proposedAct?.actSprite.Draw(spriteBatch, texture2D);
    }
}

public class Hero: Combatant
{
    public Hero(float attack, float defense, float health, string spritename, TileLocation tileLocation) : base(attack, defense, health, spritename, tileLocation)
    {
        
    }
    public override void GetMovesFromPathfinder(Combatant targetCombatant)
    {
        
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
        
    }
    public override void CheckForPlayerInput(InputManager input)
    {
        
    }
    public override void GetMovesFromPathfinder(Combatant targetCombatant)
    {
        List<Location> Moves = AStar.FindPath(_stats._tileLocation.ToWalkableLocation(), targetCombatant._stats._tileLocation.ToWalkableLocation());
        for(int i = 0; i < _stats.TilesPerMove; i++)
        {
            JumpToNewPosition(Moves[i].ToTileLocation());
        }
            

    }
}