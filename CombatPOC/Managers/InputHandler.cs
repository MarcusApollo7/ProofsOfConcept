using CombatPOC.Entities;
using Microsoft.Xna.Framework.Input;
using static POCLibrary.Core;
using POCLibrary.Input;
using CombatPOC.Enum;
using Microsoft.Xna.Framework;
using CombatPOC.Logic;
using System.Linq;
using CombatPOC.UI;
using CombatPOC.Classes;

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
            CheckPlayerInput();
            EndPlayerTurn();
        }
        if (Input.Mouse.WasButtonJustPressed(MouseButton.Right))
        {
            DeselectElement();
        }
        return _currentState;
    }
    public void SubscribeToSelect(CombatElement sender, SelectEventArgs e)
    {
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
            TurnCharacter(player);
            ChooseAttack(player);
            ConfirmAction(player);
            MovePlayer(player);
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
            combatant.Attacked = true;
        }          
    }
    private void MovePlayer(PlayerCombatant combatant)
    {
        if (Input.Mouse.IsButtonDown(MouseButton.Left) && SelectedAct is MoveAct moveAct && combatant.Moved == false)
        {
            Vector2 end = combatant.TileLocation.GetCenter();
            Vector2 origin = Input.Mouse.Position.ToVector2();
            Vector2 direction = end - origin;

            TileLocation tile = Helper.TraverseTiles2D(
                origin, 
                direction, 
                CombatPOC.MapColumns, 
                CombatPOC.MapRows, 
                visit: tile => moveAct.TilesActedUpon.Any(t => t.X == tile.X && t.Y == tile.Y));
            combatant.MovePhantomToNewLocation(tile);

        } 
        if (Input.Mouse.WasButtonJustReleased(MouseButton.Left) && SelectedAct is MoveAct && combatant.Moved == false)
        {
            TileLocation newPos = Helper.ConvertScreenPositionToTileLocation(combatant.Phantom.ScreenPosition);
            combatant.MoveToNewLocation(newPos);
            combatant.Moved = true;
        }
    }
    private void DeselectElement()
    {
        SelectedElement = null;
        SelectedAct = null;
        PlayerActionPrimed = false;
    }
    private void EndPlayerTurn()
    {
        if (Input.Keyboard.WasKeyJustPressed(Keys.Space) && _currentState.State == BattleStateEnum.WaitingForInput)
        {
            _currentState.State = BattleStateEnum.EnemyTeam;
            foreach(PlayerCombatant playerCombatant in _currentState.PlayerTeam)
            {
                playerCombatant.ResetTurn();
            }
        }
    }
}