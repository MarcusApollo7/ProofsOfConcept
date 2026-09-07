using System.Collections.Generic;
using System.Diagnostics;
using CombatPOC.Classes;
using CombatPOC.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    private List<Act> _acts;
    public CombatManager(List<Combatant> combatants, Tilemap tilemap)
    {
        _combatants = new(combatants);
        List<Combatant> playerparty = [];
        List<Combatant> enemy = [];
        List<Combatant> ally = [];
        foreach(Combatant combatant in combatants)
        {
            switch (combatant._stats._team)
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
        _acts = [];
    }

    public void JumpCombatant(Combatant combatant, Vector2 newposition)
    {
        combatant.JumpToNewPosition(newposition);
    }
    public void SelectCombatant()
    {
        foreach(Combatant combatant in _combatants)
        {
            if (combatant._sprite.Selected == true)
            {
                _selectedCombatant = combatant;           
                break;
            }
        }
    }
    public void CheckForPlayerInput()
    {
        if (_selectedCombatant != null)
        {
            /* if (Input.Keyboard.WasKeyJustPressed(Keys.A))
            {
                Act shown_action = _selectedCombatant.ShowAction(0, 0);
                if (!_acts.Contains(shown_action))
                {
                    _acts.Add(shown_action);
                }
            }   */      
        }
        if (Input.Mouse.WasButtonJustReleased(MouseButton.Left) && _selectedCombatant != null)
        {
            Vector2 mousePosition = new(Input.Mouse.CurrentState.Position.X, Input.Mouse.CurrentState.Position.Y);
            _selectedCombatant.JumpToNewPosition(mousePosition);
            _selectedCombatant._sprite.ClearSelect();
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
            combatant.Update(gameTime, Input.Mouse);
        }
    }
    public void Draw(SpriteBatch spriteBatch, Texture2D _actTexture)
    {
        _tileMap.Draw(spriteBatch);
        foreach(Combatant combatant in _combatants)
        {
            combatant.Draw(spriteBatch);
        }
        foreach(Act act in _acts)
        {
            act.Draw(spriteBatch, _actTexture);
        }
    }

}