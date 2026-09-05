using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using POCLibrary.Graphics;
using POCLibrary.Input;
using static POCLibrary.Core;

namespace CombatPOC.Managers;

public class CombatManager
{
    private TurnManager _turnManager;
    private UIManager _UIManager;
    private Party _combatants;
    private Combatant _selectedCombatant;
    private Tilemap _tileMap;
    public CombatManager(List<Combatant> combatants, Tilemap tilemap)
    {
        _combatants = new(combatants);
        List<Combatant> playerparty = [];
        List<Combatant> enemy = [];
        List<Combatant> ally = [];
        foreach(Combatant combatant in combatants)
        {
            switch (combatant._team)
            {
                case TeamEnum.Player:
                    playerparty.Add(combatant);
                break;
                case TeamEnum.Enemy:
                    enemy.Add(combatant);
                break;
                case TeamEnum.Ally:
                    ally.Add(combatant);
                break;
            }
        }
        _turnManager = new(new(playerparty), new(enemy), new(ally));
        _UIManager = new();
        _tileMap = tilemap;
    }

    public void JumpCombatant(Combatant combatant, Vector2 newposition)
    {
        combatant.JumpToNewPosition(newposition);
    }
    public void SelectCombatant()
    {
        foreach(Combatant combatant in _combatants)
        {
            combatant.OnClick(Input.Mouse);
            if (combatant.Selected == true)
            {
                _selectedCombatant = combatant;
                _selectedCombatant?.JumpToNewPosition(new(Input.Mouse.X, Input.Mouse.Y));
            }
                
            break;
        }
    }
    public void CheckForPlayerInput()
    {
        if (_selectedCombatant != null)
        {
            if (Input.Keyboard.WasKeyJustPressed(Keys.W))
            {
                _selectedCombatant.Move(Enum.CharacterDirection.Up);
            }
            if (Input.Keyboard.WasKeyJustPressed(Keys.D))
            {
                _selectedCombatant.Move(Enum.CharacterDirection.Right);
            }
            if (Input.Keyboard.WasKeyJustPressed(Keys.S))
            {
                _selectedCombatant.Move(Enum.CharacterDirection.Down);
            }
            if (Input.Keyboard.WasKeyJustPressed(Keys.A))
            {
                _selectedCombatant.Move(Enum.CharacterDirection.Left);
            }            
        }
        if (Input.Keyboard.WasKeyJustPressed(Keys.Space) && _selectedCombatant != null)
        {
            Vector2 mousePosition = new(Input.Mouse.CurrentState.Position.X, Input.Mouse.CurrentState.Position.Y);
            _selectedCombatant.JumpToNewPosition(mousePosition);
            _selectedCombatant.ClearSelect();
            _selectedCombatant = null;
        }
    }
    public void Update(GameTime gameTime)
    {
        CheckForPlayerInput();
        SelectCombatant();
        _tileMap.Update(Input.Mouse);
        foreach(Combatant combatant in _combatants)
        {
            combatant.ClearHover();
            combatant.Update(gameTime, Input.Mouse);
        }
    }

}