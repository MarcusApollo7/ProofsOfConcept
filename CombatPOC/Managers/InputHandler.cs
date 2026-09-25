using CombatPOC.Entities;
using Microsoft.Xna.Framework.Input;
using static POCLibrary.Core;
using POCLibrary.Input;
using System.Diagnostics;
using CombatPOC.Enum;
using CombatPOC.Interfaces;
using Microsoft.Xna.Framework;
using CombatPOC.Logic;
using System;
using System.Linq;

namespace CombatPOC.Managers;


public class InputHandler
{
    private IEntity _activeEntity;
    private bool PlayerActionPrimed = false;
    public IEntity ActiveEntity {get => _activeEntity; set=> _activeEntity = value;}
    private Act PlayerAct {get => CombatManager._TurnManager.ReturnPlayerAct(); set => CombatManager._TurnManager.SetPlayerAct(value);}
    public void SetActiveEntity(IEntity entity)
    {
        _activeEntity = entity;
        if (entity is Combatant combatant && entity.GetComponent<IActorRoutine>() is PlayerActor actor)
        {
            MoveAction move = actor.Move;
            Act fullMoveAct = move.GetFullMove(combatant, combatant.TileLocation);
            CombatManager._TurnManager.SetPlayerAct(fullMoveAct);
        }
    }
    public void Update(GameTime gameTime)
    {
        HandleConstantInput();
        if (_activeEntity is Combatant combatant)
        {
            if (combatant.Team == TeamEnum.Player)
            {
                CheckPlayerInput();
            }
            else
            {
                SelectNonPlayerCombatant();
            }
        }
        if (Input.Mouse.WasButtonJustPressed(MouseButton.Right))
            {
                _activeEntity = null;
                PlayerAct = null;
                PlayerActionPrimed = false;
            }
    }
    public void CheckPlayerInput()
    {
        if (_activeEntity is Combatant combatant && _activeEntity.GetComponent<IActorRoutine>() is PlayerActor actor)
        {
            TurnCharacter(combatant);
            ConfirmAction(combatant);
            ChooseAttack(combatant, actor);
        }
    }
    private void SelectNonPlayerCombatant()
    {
        Debug.WriteLine("Non-player Combatant Selected!");
    }
    private void HandleConstantInput()
    {
        Random random = new();
        if (Input.Keyboard.WasKeyJustPressed(Keys.Space))
        {
            if (CombatManager._EntityManager.GetEntity(2) is Combatant enemy && CombatManager._UIManager._animationManager.AnimationFinished())
            {
                enemy.Move(new(random.Next(1, 4), random.Next(1, 4)));
                CombatManager._pathFinder.ResetPathFinder();
                CombatManager._TurnManager.Reset();
                CombatManager._UIManager._animationManager.ResetRenderActs();
            }
        }
    }
    private void TurnCharacter(Combatant combatant)
    {
        if (Input.Keyboard.WasKeyJustPressed(Keys.A))
        {
            combatant.ActorDirection = CharacterDirection.Left;
            PlayerAct = PlayerAct.RotateAct(combatant);
            return;
        }
        else if (Input.Keyboard.WasKeyJustPressed(Keys.W))
        {
            combatant.ActorDirection = CharacterDirection.Up;
            PlayerAct = PlayerAct.RotateAct(combatant);
            return;
        }
        else if (Input.Keyboard.WasKeyJustPressed(Keys.S))
        {
            combatant.ActorDirection = CharacterDirection.Down;
            PlayerAct = PlayerAct.RotateAct(combatant);
            return;
        }
        else if (Input.Keyboard.WasKeyJustPressed(Keys.D))
        {
            combatant.ActorDirection = CharacterDirection.Right;
            PlayerAct = PlayerAct.RotateAct(combatant);
            return;
        }
    }
    private void ChooseAttack(Combatant combatant, PlayerActor actor)
    {
        if (Input.Keyboard.WasKeyJustPressed(Keys.Z))
        {
            Attack attack = actor.Attacks[0];
            CombatManager._TurnManager.SetPlayerAct(attack.Execute(combatant));
            PlayerActionPrimed = true;
            return;
        }
        else if (Input.Keyboard.WasKeyJustPressed(Keys.X))
        {
            Attack attack = actor.Attacks[0];
            CombatManager._TurnManager.SetPlayerAct(attack.Execute(combatant));
            PlayerActionPrimed = true;
            return;
        }
    }
    private void ConfirmAction(Combatant combatant)
    {
        if (Input.Mouse.WasButtonJustPressed(MouseButton.Left) && PlayerActionPrimed == true)
        {
            CombatManager._TurnManager.EnactPlayerAct();
            PlayerActionPrimed = false;
            PlayerAct = null;
            ActiveEntity = null;
            return;
        }          
        else if (Input.Mouse.IsButtonDown(MouseButton.Left) && PlayerActionPrimed == false && PlayerAct != null)
        {
            Vector2 end = combatant.TileLocation.GetCenter();
            Vector2 origin = Input.Mouse.Position.ToVector2();
            Vector2 normDirection = (end - origin)/(end-origin).Length();

            TileLocation tile = Helper.TraverseTiles2D(
                origin, 
                normDirection, 
                CombatManager.map.MapWidth, 
                CombatManager.map.MapHeight, 
                visit: tile =>
                {
                    // Check if this tile is in the list
                    bool isBlocked = PlayerAct.TilesActedUpon.Any(t => t.X == tile.X && t.Y == tile.Y);

                    return isBlocked; // stop traversal if blocked
                });
            combatant.Move(tile);
            return;
        }
    }
}