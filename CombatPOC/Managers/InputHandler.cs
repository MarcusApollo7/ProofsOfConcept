using CombatPOC.Entities;
using Microsoft.Xna.Framework.Input;
using static POCLibrary.Core;
using POCLibrary.Input;
using CombatPOC.Enum;
using Microsoft.Xna.Framework;
using CombatPOC.Logic;
using System;
using System.Linq;
using CombatPOC.UI;
using CombatPOC.Classes;
using System.Diagnostics;

namespace CombatPOC.Managers;


public class InputHandler
{
    private bool PlayerActionPrimed = false;
    private BattleState _currentState;
    private Act SelectedAct {get => _currentState.SelectedAct; set => _currentState.SelectedAct = value;}
    private CombatElement SelectedElement {get => _currentState.SelectedElement; set => _currentState.SelectedElement = value;}
    public BattleState Update(BattleState state)
    {
        _currentState = state;
        if (_currentState.State == BattleStateEnum.WaitingForInput)
        {
            Random random = new();
            if (Input.Keyboard.WasKeyJustPressed(Keys.Space))
            {
                // Readd ability to jump enemy to random spot
            }
            CheckPlayerInput();
            if (SelectedElement is PlayerCombatant combatant && Input.Mouse.IsButtonDown(MouseButton.Left) && SelectedAct != null)
            {
                Vector2 end = combatant.TileLocation.GetCenter();
                Vector2 origin = Input.Mouse.Position.ToVector2();
                Vector2 normDirection = (end - origin)/(end-origin).Length();

                TileLocation tile = Helper.TraverseTiles2D(
                    origin, 
                    normDirection, 
                    state.map.MapWidth, 
                    state.map.MapHeight, 
                    visit: tile =>
                    {
                        // Check if this tile is in the list
                        bool isBlocked = SelectedAct.TilesActedUpon.Any(t => t.X == tile.X && t.Y == tile.Y);

                        return isBlocked; // stop traversal if blocked
                    });
                combatant.MoveToNewLocation(tile);
            } 
        }
        if (Input.Mouse.WasButtonJustPressed(MouseButton.Right))
        {
            SelectedElement = null;
            SelectedAct = null;
            PlayerActionPrimed = false;
        }
        return _currentState;
    }
    public void SubscribeToSelect(CombatElement sender, SelectEventArgs e)
    {
        Debug.WriteLine(e.Message);
        SelectedElement = sender;
        if (SelectedElement is BaseCombatant combatant)
        {
            SelectedAct = combatant.Move.GetFullMove(combatant, combatant.Pathfinder);
        }
    }
    private void CheckPlayerInput()
    {
        if (SelectedElement is PlayerCombatant player)
        {
            Debug.WriteLine("Checking Player Input");
            TurnCharacter(player);
            ChooseAttack(player);
            ConfirmAction(player);
        }
    }
    private void TurnCharacter(BaseCombatant combatant)
    {
        if (Input.Keyboard.WasKeyJustPressed(Keys.A))
        {
            combatant.ActorDirection = CharacterDirection.Left;
            if (SelectedAct is AttackAct attack)
            {
                attack.RotateAttack(combatant.ActorDirection);
            }
        }
        else if (Input.Keyboard.WasKeyJustPressed(Keys.W))
        {
            combatant.ActorDirection = CharacterDirection.Up;
            if (SelectedAct is AttackAct attack)
            {
                attack.RotateAttack(combatant.ActorDirection);
            }
        }
        else if (Input.Keyboard.WasKeyJustPressed(Keys.S))
        {
            combatant.ActorDirection = CharacterDirection.Down;
            if (SelectedAct is AttackAct attack)
            {
                attack.RotateAttack(combatant.ActorDirection);
            }
        }
        else if (Input.Keyboard.WasKeyJustPressed(Keys.D))
        {
            combatant.ActorDirection = CharacterDirection.Right;
            if (SelectedAct is AttackAct attack)
            {
                attack.RotateAttack(combatant.ActorDirection);
            }
        }
    }
    private void ChooseAttack(PlayerCombatant player)
    {
        if (!player.Attacked)
        {    
            if (Input.Keyboard.WasKeyJustPressed(Keys.Z))
            {
                Attack attack = player.Attacks[0];
                SelectedAct = attack.Execute(player);
                PlayerActionPrimed = true;
            }
            else if (Input.Keyboard.WasKeyJustPressed(Keys.X))
            {
                Attack attack = player.Attacks[1];
                SelectedAct = attack.Execute(player);
                PlayerActionPrimed = true;
            }
        }
    }
    private void ConfirmAction(PlayerCombatant combatant)
    {
        if (Input.Mouse.WasButtonJustPressed(MouseButton.Left) && PlayerActionPrimed == true)
        {
            _currentState.SelectedActConfirmed = true;
            PlayerActionPrimed = false;
            SelectedAct = null;
            SelectedElement = null;
            combatant.Attacked = true;
            return;
        }          
    }
}